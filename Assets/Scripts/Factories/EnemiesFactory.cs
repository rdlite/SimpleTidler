using UnityEngine;

public class EnemiesFactory
{
    private GlobalStateMachine _globalSM;
    private PrefabsContainer _prefabsContainer;
    private Configs _configs;

    public EnemiesFactory(
        PrefabsContainer prefabsContainer, GlobalStateMachine globalSM, Configs configs)
    {
        _globalSM = globalSM;
        _prefabsContainer = prefabsContainer;
        _configs = configs;
    }

    public BaseEnemy Spawn(Vector3 position, EnemyType type)
    {
        BaseEnemy newEnemy = Object.Instantiate(_prefabsContainer.GetEnemyPrefabByType(type));
        newEnemy.transform.position = position;
        newEnemy.Init(_globalSM.StatesData.PlayerTower, _configs, _globalSM.StatesData.MoneyFlowController);
        return newEnemy;
    }
}