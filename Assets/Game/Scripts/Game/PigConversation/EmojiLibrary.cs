using LazyFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EmoteLibrary", menuName = "ScriptableObjects/EmoteLibrary")]
public class EmojiLibrary : ScriptableObject
{
    public List<EmoteData> listEmoji = new List<EmoteData>();
    private List<int> listPositive = new List<int>();
    private List<int> listNegative = new List<int>();
    private void OnValidate()
    {
        for (int i = 0; i < listEmoji.Count; i++)
        {
            listEmoji[i].index = i;
        }
    }
    public Sprite GetEmoji(int index)
    {
        if (index < listEmoji.Count)
        {
            return listEmoji[index].emoji;

        }
        else
        {
            Bug.Log("Emoji index is out of list");
            return null;
        }

    }
    public Sprite GetEmoji(string name)
    {
        foreach (var emoji in listEmoji)
        {
            if (emoji.name == name)
            {
                return emoji.emoji;
            }
        }
        return null;
    }

    public List<int> GetPositiveEmoji()
    {
        if(listPositive.Count == 0)
        {
            for (int i = 0; i < listEmoji.Count; i++)
            {
                if (listEmoji[i].type == EmojiType.Positive)
                {
                    listPositive.Add(i);
                }
            }
        }
        
        return listPositive;
    }
    public List<int> GetNegativeEmoji()
    {
        if(listNegative.Count == 0)
        {
            for (int i = 0; i < listEmoji.Count; i++)
            {
                if (listEmoji[i].type == EmojiType.Negative)
                {
                    listNegative.Add(i);
                }
            }
        }
        return listNegative;
    }
}

[Serializable]
public class EmoteData
{
    public string name;
    public int index;
    public Sprite emoji;
    public EmojiType type;
}

