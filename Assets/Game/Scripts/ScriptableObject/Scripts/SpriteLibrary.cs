using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpriteLibrary" , menuName = "ScriptableObjects/SpriteLibrary")]
public class SpriteLibrary : ScriptableObject
{

}

[Serializable] public struct SpriteAsset
{
    public string name;
    public Sprite texture;
}
