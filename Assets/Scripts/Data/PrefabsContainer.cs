using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prefabs container", menuName = "Containers/PrefabsContainer")]
public class PrefabsContainer : ScriptableObject
{
    public PlayerTower PlayerTower;
    public List<BaseEnemy> Enemeis;
    public DefaultBullet DefaultPlayerBullet;
    public ParticleSystem EnemyDeathFX;
    public ParticleSystem PlayerDeathFX;
    public MoneyPopup MoneyPopup;

    public BaseEnemy GetEnemyPrefabByType(EnemyType type)
    {
        foreach (var item in Enemeis)
        {
            if (item.EnemyType == type)
            {
                return item;
            }
        }

        return null;
    }
}