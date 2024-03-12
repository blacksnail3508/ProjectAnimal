using LazyFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpriteLibrary" , menuName = "ScriptableObjects/SpriteLibrary")]
public class SpriteLibrary : ScriptableObject
{
    public List<SpriteAsset> SkateBoardAsset = new List<SpriteAsset>();
    public List<SpriteAsset> SkateDecorAsset = new List<SpriteAsset>();

    private void OnValidate()
    {
        for (int i = 0; i<SkateBoardAsset.Count; i++)
        {
            SkateBoardAsset[i].name = $"Board {i}";
        }

        for (int i = 0; i<SkateDecorAsset.Count; i++)
        {
            SkateDecorAsset[i].name = $"Decor {i}";
        }
    }

    public Sprite GetDecor(int index)
    {
        if(index >= SkateBoardAsset.Count)
        {
            Bug.Log("index out of Asset count");
            return null;
        }

        return SkateDecorAsset[index].texture;
    }

    public Sprite GetBoard(int index)
    {
        if (index>=SkateBoardAsset.Count)
        {
            Bug.Log("index out of Asset count");
            return null;
        }

        return SkateBoardAsset[index].texture;
    }
}

[Serializable]
public class SpriteAsset
{
    public string name;
    public Sprite texture;
}
