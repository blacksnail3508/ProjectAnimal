using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelAsset", menuName = "ScriptableObjects/LevelAsset")]
public class LevelAsset : ScriptableObject
{
    public List<Level> listLevel = new List<Level>();

    private void OnValidate()
    {
        for (int i = 0; i < listLevel.Count; i++)
        {
            listLevel[i].name=$"Level {i}";
        }
    }
}

[Serializable]public class Level
{
    public string name;
    public int sizeX;
    public int sizeY;
    public List<CannonData> listCannon = new List<CannonData>();
}

[Serializable]public struct CannonData
{
    public int posX;
    public int posY;

    public int animalCount;
}
