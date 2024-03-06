using System;
using UnityEngine;
[CreateAssetMenu(fileName = "GameConfig" , menuName = ("ScriptableObjects/GameConfig"))]
public class GameConfig : ScriptableObject
{
    public GateConfig GateConfig = new GateConfig();
}
[Serializable] public struct GateConfig
{
    public float AnimationTime;

}

