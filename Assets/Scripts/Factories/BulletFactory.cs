using UnityEngine;

public class BulletFactory
{
    private UpgradesController _upgradesController;
    private PrefabsContainer _prefabsContainer;

    public BulletFactory(Configs configs, UpgradesController upgradesController)
    {
        _upgradesController = upgradesController;
        _prefabsContainer = configs.PrefabsContainer;
    }

    public DefaultBullet SpawnBullet(Transform target, Vector3 startPos)
    {
        DefaultBullet newBullet = Object.Instantiate(_prefabsContainer.DefaultPlayerBullet);
        newBullet.transform.position = startPos;
        newBullet.Init(target, _upgradesController);

        return newBullet;
    }
}