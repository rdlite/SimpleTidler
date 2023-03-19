using UnityEngine;

public class MoneyFlowController
{
    private UIRoot _uiRoot;
    private int _currentMoneyAmount;

    public MoneyFlowController(UIRoot uiRoot)
    {
        _uiRoot = uiRoot;
    }

    public void Init()
    {
        _currentMoneyAmount = 0;
        UpdateUIMoneyValue();
    }

    public void AddMoney(int amount)
    {
        _currentMoneyAmount += amount;
        UpdateUIMoneyValue();
    }

    public bool TrySpendMoney(int amount)
    {
        if (_currentMoneyAmount - amount >= 0)
        {
            _currentMoneyAmount -= amount;
            UpdateUIMoneyValue();
            return true;
        }

        return false;
    }

    private void UpdateUIMoneyValue()
    {
        _uiRoot.GetPanel<IngamePanel>().SetMoneyTextValue(_currentMoneyAmount);
        _uiRoot.GetPanel<IngamePanel>().UpdateInteractivityOfButtons(_currentMoneyAmount);
    }
}