using System;
using UnityEngine;
[CreateAssetMenu(fileName = "GameConfig" , menuName = ("ScriptableObjects/GameConfig"))]
public class GameConfig : ScriptableObject
{
    public BulletConfig bulletConfig;
    public BuffConfig buffConfig;
    public ArcherConfig archerConfig;
}
[Serializable]
public struct BulletConfig
{
    public float bulletSpeed;
}
[Serializable]
public struct BuffConfig
{
    public int ShieldStrength;
}
[Serializable]
public struct ArcherConfig
{
    public float ArcherMoveTime;
}
