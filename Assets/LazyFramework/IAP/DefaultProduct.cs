using System;
using System.Collections.Generic;
using UnityEngine;
namespace LazyFramework
{
    [CreateAssetMenu(fileName = "DefaultProduct" , menuName = "ScriptableObject/DefaultProduct")]
    public class DefaultProduct : ScriptableObject
    {
        public List<DefaultProductData> defaultProductDatas = new List<DefaultProductData>();
    }
    [Serializable]
    public struct DefaultProductData
    {
        public string productId;
        public string price;
    }
}

