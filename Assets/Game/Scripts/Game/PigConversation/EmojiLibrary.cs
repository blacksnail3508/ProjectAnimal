using LazyFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EmoteLibrary" , menuName = "ScriptableObjects/EmoteLibrary")]
public class EmojiLibrary : ScriptableObject
{
    public List<EmoteData> listEmoji = new List<EmoteData>();

    private void OnValidate()
    {
        for (int i = 0;i<listEmoji.Count; i++)
        {
            listEmoji[i].index = i;
        }
    }
    public Sprite GetEmoji(int index)
    {
        if(index < listEmoji.Count)
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
            if(emoji.name == name)
            {
                return emoji.emoji;
            }
        }
        return null;
    }
}

[Serializable] public class EmoteData
{
    public string name;
    public int index;
    public Sprite emoji;
}

