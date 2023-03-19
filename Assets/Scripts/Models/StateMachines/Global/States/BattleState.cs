using System.Collections.Generic;
using UnityEngine;

public class BattleState : IState, IUpdateState
{
    private List<BaseEnemy> _spawnedEnemies;
    private UIRoot _uiRoot;
    private Configs _configs;
    private GlobalStateMachine _globalSM;
    private EnemiesFactory _enemiesFactory;
    private float _spawnTimer = 0f;
    private float _timeFromSpawn = 0f;
    private int _enemyTypesAmount;

    public BattleState(
        GlobalStateMachine globalSM, Configs configs, UIRoot uiRoot)
    {
        _uiRoot = uiRoot;
        _configs = configs;
        _globalSM = globalSM;
        _enemiesFactory = new EnemiesFactory(
            configs.PrefabsContainer, globalSM, configs);
        _enemyTypesAmount = System.Enum.GetNames(typeof(EnemyType)).Length;
    }

    public void Enter()
    {
        _uiRoot.EnablePanel<IngamePanel>();
        _spawnTimer = 0f;
        _timeFromSpawn = 0f;
        _spawnedEnemies = new List<BaseEnemy>();
    }

    public void Update()
    {
        _spawnTimer += Time.deltaTime;
        _timeFromSpawn += Time.deltaTime;

        float currentTimeToSpawn = CalculateCurrentSpawnTime();

        if (_spawnTimer >= currentTimeToSpawn)
        {
            _spawnTimer = 0f;
            SpawnEnemy();
        }

        for (int i = 0; i < _spawnedEnemies.Count; i++)
        {
            if (_spawnedEnemies[i] == null)
            {
                _spawnedEnemies.RemoveAt(i);
                
                i--;

                if (i < 0)
                {
                    i = 0;
                }

                if (_spawnedEnemies.Count == 0)
                {
                    break;
                }
            }

            _spawnedEnemies[i].UpdateTick();
        }

        _globalSM.StatesData.PlayerTower.UpdateTick();
    }

    public void Exit() { }

    private void SpawnEnemy()
    {
        EnemyType enemyToSpawn = (EnemyType)(Random.Range(0, _enemyTypesAmount));
        Vector3 randomPoint = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)) * Vector3.up * _configs.SpawnDistance;
        BaseEnemy enemy = _enemiesFactory.Spawn(_globalSM.StatesData.PlayerTower.transform.position + randomPoint, enemyToSpawn);
        _spawnedEnemies.Add(enemy);
    }

    private float CalculateCurrentSpawnTime()
    {
        return Mathf.Lerp(_configs.DefaultSpawnRate, _configs.MinSpawnRate, Mathf.InverseLerp(0f, _configs.TimeToMinSpawnRate, _timeFromSpawn));
    }
}
