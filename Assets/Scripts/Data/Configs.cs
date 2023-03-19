using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Configs", menuName = "Configs/Main configs")]
public class Configs : ScriptableObject
{
    [Header("Player configs")]
    public float PlayerTowerDefaultRadius = 1f;
    public float PlayerDefaultShootSpeed = 1f;
    public float PlayerDefaultHealth = 100f;
    public float PlayerHealPower = 5f;
    [Header("Enemies configs")]
    public float DefaultSpawnRate = 2f;
    public float MinSpawnRate = .5f;
    public float TimeToMinSpawnRate = 180f;
    public float SpawnDistance = 3f;
    [Header("Containers")]
    public PrefabsContainer PrefabsContainer;
    public List<UpgradeData> Upgrades;
}