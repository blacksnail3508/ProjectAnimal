using System;
using UnityEngine;
[CreateAssetMenu(fileName = "GameConfig" , menuName = ("ScriptableObjects/GameConfig"))]
public class GameConfig : ScriptableObject
{
    public GateConfig Cannon = new GateConfig();
    public AnimalConfig Animal = new AnimalConfig();
    public EffectConfig Effect = new EffectConfig();
    public EconomyConfig Economy = new EconomyConfig();
}
[Serializable] public struct GateConfig
{
    public float AnimationTime;

}
[Serializable] public struct AnimalConfig
{
    public float animationTime;
    public float backwardScale;
}
[Serializable] public struct EffectConfig
{
    public float combatTime;
    public float emojiTime;
    [Range(0 , 1)] public float celebrateEmojiRate;
    [Range(0,1)]public float loseEmojiRate;

    public float coinChangeFadeDelay;
    public float coinChangeFadeTime;
}
[Serializable] public struct EconomyConfig
{
    public int HintCost;
    public int UndoCost;
    public int UFOCost;
}

