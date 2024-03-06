using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PopupLibrary" , menuName = "ScriptableObject/PopupLibrary" , order = 1)]
public class PopupLibrary : ScriptableObject
{
    public List<PopupPrefab> listPopupPrefabs = new List<PopupPrefab>();

}
[Serializable]
public struct PopupPrefab
{
    [Tooltip("this popup name must match to popup name in UIPopupName")]
    public string popupName;
    public GameObject popupPrefab;
}

