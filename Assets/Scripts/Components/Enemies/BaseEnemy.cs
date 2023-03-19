using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(EnemyHealthController))]
public class BaseEnemy : MonoBehaviour
{
    [field: SerializeField] public EnemyType EnemyType { get; private set; }

    [SerializeField] private EnemyConfigs _enemyConfigs;

    private EnemyHealthController _enemyHealth;
    private MoneyFlowController _moneyFlowController;
    private Configs _configs;
    private PlayerTower _target;
    private Vector3 _movementDir;

    public void Init(
        PlayerTower target, Configs configs, MoneyFlowController moneyFlowController)
    {
        _moneyFlowController = moneyFlowController;
        _configs = configs;
        _target = target;
        transform.DOScale(transform.localScale, .4f).From(Vector3.zero).SetEase(Ease.OutBack);
        _movementDir = (_target.transform.position - transform.position).normalized;
        _enemyHealth = GetComponent<EnemyHealthController>();
        _enemyHealth.Init(_enemyConfigs.Health);
    }

    public void UpdateTick()
    {
        transform.position += _movementDir * _enemyConfigs.MovementSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, _target.transform.position) < .2f)
        {
            _target.TakeDamage(_enemyConfigs.AttackPower);
            Instantiate(_configs.PrefabsContainer.EnemyDeathFX, transform.position, _configs.PrefabsContainer.EnemyDeathFX.transform.rotation);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageValue)
    {
        if (_enemyHealth.TakeDamage(damageValue))
        {
            Kill();
        }
    }

    public void Kill()
    {
        _moneyFlowController.AddMoney(_enemyConfigs.MoneyByKill);
        MoneyPopup moneyPopup = Instantiate(_configs.PrefabsContainer.MoneyPopup, transform.position, _configs.PrefabsContainer.MoneyPopup.transform.rotation);
        moneyPopup.Init(_enemyConfigs.MoneyByKill);
        Instantiate(_configs.PrefabsContainer.EnemyDeathFX, transform.position, _configs.PrefabsContainer.EnemyDeathFX.transform.rotation);
        Destroy(gameObject);
    }
}

public enum EnemyType
{
    StrongSlow, FastWeak
}