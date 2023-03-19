using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public event Action<UpgradeType> OnButtonPressed;
    public UpgradeType UpgradeType;

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _maxTitle;
    [SerializeField] private Button _currentButton;

    private bool _isMaxed;

    public void Init(UpgradeData upgradeData)
    {
        UpdateLevel(1);
        UpdateCost((int)upgradeData.Cost);
        _currentButton.onClick.AddListener(() => 
        {
            OnButtonPressed?.Invoke(UpgradeType);
        });
    }

    public void UpdateCost(int newCost)
    {
        _costText.text = $"{newCost}$";
    }

    public void UpdateLevel(int level)
    {
        _levelText.text = level.ToString();
    }

    public void SetInteractable(bool value)
    {
        if (_isMaxed) value = true;

        _canvasGroup.interactable = value;
        _canvasGroup.alpha = value ? 1f : .5f;
    }

    public void MarkBought(bool value)
    {
        _costText.gameObject.SetActive(!value);
        _maxTitle.gameObject.SetActive(value);
        _isMaxed = value;
        if (value)
        {
            SetInteractable(true);
        }
    }
}