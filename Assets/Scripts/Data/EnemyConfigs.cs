using UnityEngine;

[CreateAssetMenu(fileName = "New enemy config", menuName = "Configs/EnemyConfigs")]
public class EnemyConfigs : ScriptableObject
{
    public float MovementSpeed = 1f;
    public float AttackPower = 10f;
    public float Health = 20f;
    public int MoneyByKill = 5;
}