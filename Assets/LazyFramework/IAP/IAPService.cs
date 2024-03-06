using System;
using UnityEngine;
using UnityEngine.Purchasing;
namespace LazyFramework
{
    public class IAPService : MonoBehaviour
    {
        public static IAPService Instance;
        public static IAPManager manager;
        [SerializeField] DefaultProduct productData;

        [Obsolete]
        private void Awake()
        {
            SaveDefautlPrice();
            Instance=this;
            manager=new IAPManager();
        }
        public void SaveProductPrice(IStoreController controller)
        {
            foreach (var product in controller.products.all)
            {
                SaveProduct(product.definition.id , product.metadata.localizedPriceString , true);
            }
        }

        private void SaveDefautlPrice()
        {
            foreach (var product in productData.defaultProductDatas)
            {
                SaveProduct(product.productId , product.price.ToString());
            }
        }
        private void SaveProduct(string productId , string price , bool isForced = false)
        {
            if (isForced)
            {
                PlayerPrefs.SetString(productId , price);
            }
            else
            {
                if (PlayerPrefs.HasKey(productId)==false)
                {
                    PlayerPrefs.SetString(productId , price);
                }
            }
        }
        public static string GetPrice(string productId)
        {
            return PlayerPrefs.GetString(productId);
        }
        public static void Purchase(string id , Action onsuccess , Action onFailure)
        {
            manager.Purchase(id , onsuccess , onFailure);
        }
    }

}
