using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LazyFramework
{
    [CreateAssetMenu(fileName = "UIMenuLoader" , menuName = "ScriptableObject/UIMenuLoader" , order = 1)]
    public class MenuLoader : ScriptableObject
    {
        public List<UIMenuPrefab> listMenuPrefabs = new List<UIMenuPrefab>();
    }
    [Serializable]
    public struct UIMenuPrefab
    {
        [Tooltip("this popup name must match to popup name in UIName")]
        public string menuName;
        public GameObject menuPrefab;
    }
}