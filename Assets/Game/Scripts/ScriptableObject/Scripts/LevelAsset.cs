using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelAsset" , menuName = "ScriptableObjects/LevelAsset")]
public class LevelAsset : ScriptableObject
{
    public List<Level> listLevel = new List<Level>();

    private void OnValidate()
    {
        for (int i = 0; i<listLevel.Count; i++)
        {
            listLevel[i].name=$"Level {i+1}";
        }

        listLevel[0].CoinReward = 50;
        for(int i = 1;i<listLevel.Count; i++)
        {
            listLevel[i].CoinReward=5;
            if(i % 10 == 9)
            {
                listLevel[i].CoinReward = 20;
            }
        }
    }
}

[Serializable]
public class Level
{
    public string name;

    public int CoinReward;

    public int sizeX;
    public int sizeY;
    public List<CannonData> listCannon = new List<CannonData>();
    public List<ObstacleData> listObstacle = new List<ObstacleData>();
}

[Serializable]
public struct CannonData
{
    public int posX;
    public int posY;
    public int animalCount;
}

[Serializable]
public struct ObstacleData
{
    public int posX;
    public int posY;
    public ObjectType type;
}
