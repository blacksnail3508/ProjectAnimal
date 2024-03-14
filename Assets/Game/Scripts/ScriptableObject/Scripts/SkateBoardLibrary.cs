using LazyFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkateBoardLibrary" , menuName = "ScriptableObjects/SkateBoardLibrary")]
public class SkateBoardLibrary : ScriptableObject
{
    public List<SkateBoardData> listSkateBoard = new List<SkateBoardData>();

    private void OnValidate()
    {
        for (int i = 0; i<listSkateBoard.Count; i++)
        {
            listSkateBoard[i].name = $"Skate board {i}";
            listSkateBoard[i].unlockLevel=i*5;
        }

    }

    public Sprite GetBoard(int index)
    {
        if(index >=listSkateBoard.Count)
        {
            Bug.Log("index out of Asset count");
            return null;
        }

        return listSkateBoard[index].board;
    }

    public int GetUnlockLevel(int index)
    {
        if (index>=listSkateBoard.Count)
        {
            Bug.Log("index out of Asset count");
            return 0;
        }

        return listSkateBoard[index].unlockLevel;
    }
}

[Serializable]
public class SkateBoardData
{
    public string name;
    public Sprite board;
    public int unlockLevel;
}
