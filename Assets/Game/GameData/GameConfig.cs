using System;
using UnityEngine;
[CreateAssetMenu(fileName = "GameConfig" , menuName = ("ScriptableObjects/GameConfig"))]
public class GameConfig : ScriptableObject
{
    public GateConfig GateConfig = new GateConfig();
    public AnimalConfig AnimalConfig = new AnimalConfig();
}
[Serializable] public struct GateConfig
{
    public float AnimationTime;

}
[Serializable] public struct AnimalConfig
{
    public float tilePerSec;
}

