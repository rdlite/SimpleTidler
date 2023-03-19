using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private List<TriggerCatcher> _triggers;
    [SerializeField] private Transform _radius;

    private List<BaseEnemy> _enemiesInShootRange;
    private GlobalStateMachine _globalSM;
    private UpgradesController _upgradesController;
    private BulletFactory _bulletFactory;
    private Configs _configs;
    private BaseEnemy _nearestEnemy;
    private PlayerHealth _healthController;
    private float _shootingSpeed;
    private float _currentScaleTarget;

    public void Init(
        Configs configs, BulletFactory bulletFactory, UpgradesController upgradesController,
        UIRoot uiRoot, GlobalStateMachine globalSM)
    {
        _globalSM = globalSM;
        _upgradesController = upgradesController;
        _bulletFactory = bulletFactory;
        _configs = configs;

        foreach (var item in _triggers)
        {
            item.OnEnemyCatched += CatchEnemy;
        }

        _enemiesInShootRange = new List<BaseEnemy>();
        _shootingSpeed = _configs.PlayerDefaultShootSpeed;

        _radius.transform.localScale = Vector3.zero;
        _currentScaleTarget = configs.PlayerTowerDefaultRadius;

        _healthController = new PlayerHealth(uiRoot, configs);
        _healthController.OnPlayerKilled += Kill;
    }

    public void UpdateTick()
    {
        _shootingSpeed += Time.deltaTime * (1f + _upgradesController.GetUpgradeValue(UpgradeType.ShootSpeed));

        if (_enemiesInShootRange.Count != 0)
        {
            for (int i = 0; i < _enemiesInShootRange.Count; i++)
            {
                if (_enemiesInShootRange[i] == null)
                {
                    _enemiesInShootRange.RemoveAt(i);

                    i--;

                    if (i < 0)
                    {
                        i = 0;
                    }

                    if (_enemiesInShootRange.Count == 0)
                    {
                        break;
                    }
                }
            }

            if (_shootingSpeed >= _configs.PlayerDefaultShootSpeed)
            {
                _shootingSpeed = 0f;
                ShootToNearest();
            }
        }

        _radius.transform.localScale =
            Vector3.Lerp(_radius.transform.localScale, Vector3.one * (_currentScaleTarget + _upgradesController.GetUpgradeValue(UpgradeType.Radius)), 7f * Time.deltaTime);

        _healthController.Tick();
    }

    public void TakeDamage(float damage)
    {
        _healthController.TakeDamage(damage);
    }

    public void Kill()
    {
        _globalSM.Enter<LoseState>();
    }

    public void DeactivateRadiusVisual()
    {
        _radius.gameObject.SetActive(false);
    }

    private BaseEnemy GetNearestEnemy()
    {
        BaseEnemy nearest = null;
        float nearestDist = 10000f;

        for (int i = 0; i < _enemiesInShootRange.Count; i++)
        {
            if (_enemiesInShootRange[i] != null)
            {
                float distance = Vector3.Distance(transform.position, _enemiesInShootRange[i].transform.position);

                if (distance <= nearestDist)
                {
                    nearestDist = distance;
                    nearest = _enemiesInShootRange[i];
                }
            }
        }

        return nearest;
    }

    private void ShootToNearest()
    {
        _nearestEnemy = GetNearestEnemy();

        if (_nearestEnemy != null)
        {
            _bulletFactory.SpawnBullet(_nearestEnemy.transform, transform.position);
        }
    }

    private void CatchEnemy(BaseEnemy enemy)
    {
        _enemiesInShootRange.Add(enemy);
    }
}