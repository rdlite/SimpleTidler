using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradesController
{
    private Dictionary<UpgradeType, int> _upgradesMap;

    private MoneyFlowController _moneyFlowController;
    private Configs _configs;
    private UIRoot _uiRoot;

    public UpgradesController(
        MoneyFlowController moneyFlowController, UIRoot uiRoot, Configs configs)
    {
        _configs = configs;
        _uiRoot = uiRoot;

        uiRoot.GetPanel<IngamePanel>().OnUpgradeButtonPressed += TryUpgrade;
        _moneyFlowController = moneyFlowController;

        _upgradesMap = new Dictionary<UpgradeType, int>();

        for (int i = 0; i < configs.Upgrades.Count; i++)
        {
            _upgradesMap.Add(configs.Upgrades[i].Type, 1);
            _uiRoot.GetPanel<IngamePanel>().UpdateButtonCost(GetCurrentUpgradeCost(configs.Upgrades[i].Type), configs.Upgrades[i].Type);
            _uiRoot.GetPanel<IngamePanel>().UpdateButtonLevel(_upgradesMap[configs.Upgrades[i].Type], configs.Upgrades[i].Type);
            _uiRoot.GetPanel<IngamePanel>().MarkButtonBought(configs.Upgrades[i].Type, false);
        }
    }

    public void TryUpgrade(UpgradeType upgrade)
    {
        if (IsUpgradeMax(upgrade))
        {
            return;
        }

        if (_moneyFlowController.TrySpendMoney(GetCurrentUpgradeCost(upgrade)))
        {
            _upgradesMap[upgrade]++;

            if (_upgradesMap[upgrade] == _configs.Upgrades.First(upgradeData => upgradeData.Type == upgrade).MaxLevel)
            {
                _uiRoot.GetPanel<IngamePanel>().MarkButtonBought(upgrade, true);
            }

            _uiRoot.GetPanel<IngamePanel>().UpdateButtonCost(GetCurrentUpgradeCost(upgrade), upgrade);
            _uiRoot.GetPanel<IngamePanel>().UpdateButtonLevel(_upgradesMap[upgrade], upgrade);
        }
    }

    public int GetCurrentUpgradeCost(UpgradeType type)
    {
        UpgradeData itemToUpgrade = _configs.Upgrades.First((upgradeData) => upgradeData.Type == type);
        int cost = itemToUpgrade.Cost + itemToUpgrade.CostStep * (_upgradesMap[type] - 1);
        return cost;
    }

    public float GetUpgradeValue(UpgradeType type)
    {
        UpgradeData data = _configs.Upgrades.First(upgrade => upgrade.Type == type);
        return (_upgradesMap[type] - 1) * data.UpgradeValueStep;
    }

    private bool IsUpgradeMax(UpgradeType upgrade)
    {
        return _upgradesMap[upgrade] == _configs.Upgrades.First(upgradeData => upgradeData.Type == upgrade).MaxLevel;
    }
}