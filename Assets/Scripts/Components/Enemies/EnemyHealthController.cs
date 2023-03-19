using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private GameObject _healthbar;
    [SerializeField] private Image _healthSlider;

    private float _currentHealth;
    private float _maxHealth;

    public void Init(float health)
    {
        _currentHealth = _maxHealth = health;
    }

    public bool TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth > 0f)
        {
            _healthbar.gameObject.SetActive(true);
            _healthSlider.fillAmount = _currentHealth / _maxHealth;
            return false;
        }
        else
        {
            return true;
        }
    }
}