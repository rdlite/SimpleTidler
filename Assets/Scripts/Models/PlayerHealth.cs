using System;
using UnityEngine;

public class PlayerHealth
{
    public event Action OnPlayerKilled;

    private UIRoot _uiRoot;
    private Configs _configs;
    private float _currentHealth, _maxHealth;
    private bool _isKilled;

    public PlayerHealth(UIRoot uiRoot, Configs configs)
    {
        _uiRoot = uiRoot;
        _configs = configs;
        _currentHealth = _maxHealth = configs.PlayerDefaultHealth;
        _uiRoot.GetPanel<IngamePanel>().SetPlayerHealthSliderValue(_currentHealth / _maxHealth);
    }

    public void Tick()
    {
        if (Time.time % 1 == 0)
        {
            _currentHealth += _configs.PlayerHealPower;
            _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);

            _uiRoot.GetPanel<IngamePanel>().SetPlayerHealthSliderValue(_currentHealth / _maxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        if (_isKilled)
        {
            return;
        }

        _currentHealth -= damage;
        _uiRoot.GetPanel<IngamePanel>().SetPlayerHealthSliderValue(_currentHealth / _maxHealth);

        if (_currentHealth <= 0f)
        {
            Kill();
        }
    }

    private void Kill() 
    {
        _isKilled = true;
        OnPlayerKilled?.Invoke();
    }
}