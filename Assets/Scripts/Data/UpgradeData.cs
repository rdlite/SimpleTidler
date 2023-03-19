using UnityEngine;

[CreateAssetMenu(fileName = "New upgrade data", menuName = "Configs/Upgrade data")]
public class UpgradeData : ScriptableObject
{
    public UpgradeType Type;
    public int Cost = 20;
    public int CostStep = 5;
    public int MaxLevel = 10;
    public float UpgradeValueStep;
}

public enum UpgradeType
{
    Damage, ShootSpeed, Radius
}