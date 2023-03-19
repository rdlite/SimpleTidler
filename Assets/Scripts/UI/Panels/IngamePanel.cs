using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngamePanel : UIPanel
{
    public event Action<UpgradeType> OnUpgradeButtonPressed;

    [SerializeField] private List<UpgradeButton> _upgrades;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Image _healthFill;

    private Vector3 _defaultMoneyTextScale;
    private float _targetHealthPercentage;

    protected override void LocalInit()
    {
        _defaultMoneyTextScale = _moneyText.transform.localScale;

        for (int i = 0; i < _upgrades.Count; i++)
        {
            UpgradeData data = null;

            foreach (var item in _configs.Upgrades)
            {
                if (item.Type == _upgrades[i].UpgradeType)
                {
                    data = item;
                    break;
                }
            }

            _upgrades[i].Init(data);
            _upgrades[i].OnButtonPressed += RaiseOnUpgradeButtonEvent;
        }

        _targetHealthPercentage = 1f;
    }

    private void Update()
    {
        _healthFill.fillAmount = Mathf.Lerp(_healthFill.fillAmount, _targetHealthPercentage, 5f * Time.deltaTime);
    }

    public void SetMoneyTextValue(int value)
    {
        _moneyText.transform.DOScale(_defaultMoneyTextScale, .1f).From(_defaultMoneyTextScale * 1.1f);
        _moneyText.text = $"{value}$";
    }

    public void RaiseOnUpgradeButtonEvent(UpgradeType type)
    {
        OnUpgradeButtonPressed?.Invoke(type);
    }

    public void UpdateButtonCost(int newCost, UpgradeType type)
    {
        _upgrades.First((upgrade) => upgrade.UpgradeType == type).UpdateCost(newCost);
    }

    public void UpdateButtonLevel(int newLevel, UpgradeType type)
    {
        _upgrades.First((upgrade) => upgrade.UpgradeType == type).UpdateLevel(newLevel);
    }

    public void MarkButtonBought(UpgradeType type, bool value)
    {
        _upgrades.First((upgrade) => upgrade.UpgradeType == type).MarkBought(value);
    }

    public void UpdateInteractivityOfButtons(int currentMoneyAmount)
    {
        foreach (var item in _upgrades)
        {
            item.SetInteractable(currentMoneyAmount >= _globalSM.StatesData.UpgradesController.GetCurrentUpgradeCost(item.UpgradeType));
        }
    }

    public void SetPlayerHealthSliderValue(float percentage)
    {
        _targetHealthPercentage = percentage;
    }
}