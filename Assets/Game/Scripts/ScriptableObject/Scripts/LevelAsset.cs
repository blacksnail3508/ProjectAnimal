using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelAsset", menuName = "ScriptableObjects/LevelAsset")]
public class LevelAsset : ScriptableObject
{
    public List<Level> listLevel = new List<Level>();
}

[Serializable]public struct Level
{
    public int sizeX;
    public int sizeY;

}
