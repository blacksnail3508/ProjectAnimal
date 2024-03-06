using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
namespace LazyFramework
{

    public class IAPManager : IDetailedStoreListener
    {
        private IStoreController controller;
        private IExtensionProvider extensions;
        private Action onSuccess;
        private Action onFailure;


        public IAPManager()
        {
            var purchasingModule = StandardPurchasingModule.Instance();
#if ENABLE_TEST_IAP
			purchasingModule.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
			purchasingModule.useFakeStoreAlways = true;
#else
            purchasingModule.useFakeStoreAlways=false;
#endif
            var builder = ConfigurationBuilder.Instance(purchasingModule);

            ////add product
            builder.AddProduct(IAPProduct.ads , ProductType.Consumable);

            UnityPurchasing.Initialize(this , builder);
        }

        /// <summary>
        /// Called when a purchase completes.
        ///
        /// May be called at any time after OnInitialized().
        /// </summary>
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            onSuccess?.Invoke();
            DisplayService.ShowPopup(UIPopupName.PopupPurchaseSuccess);
            return PurchaseProcessingResult.Complete;
        }

        /// <summary>
        /// Called when a purchase fails.
        /// </summary>
        public void OnPurchaseFailed(Product i , PurchaseFailureReason p)
        {
            onFailure?.Invoke();
        }

        public void OnInitializeFailed(InitializationFailureReason error , string message)
        {
            Debug.Log($"IAP manager initial failed, reason {error}");
            DisplayService.ShowMessage($"IAP manager initial failed, reason {error}");
        }

        public void OnInitialized(IStoreController controller , IExtensionProvider extensions)
        {
            this.controller=controller;
            this.extensions=extensions;

            var additional = new HashSet<ProductDefinition>()
        {
            new ProductDefinition(IAPProduct.ads           ,ProductType.Consumable),
        };

            Action onSuccess = () =>
            {
                Debug.Log("Fetched successfully!");
                DisplayService.ShowMessage("Fetched successfully!");
                IAPService.Instance.SaveProductPrice(controller);
            };

            Action<InitializationFailureReason , string> onFailure = (i , _) =>
            {
                Bug.Log("Fetching failed for the specified reason: "+i);
                DisplayService.ShowMessage("Fetching failed for the specified reason: "+i);
            };

            controller.FetchAdditionalProducts(additional , onSuccess , onFailure);
        }

        public void Purchase(string id , Action onSuccess , Action onFailure)
        {
            this.onSuccess=onSuccess;
            this.onFailure=onFailure;

            controller.InitiatePurchase(id);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {

        }

        public void OnPurchaseFailed(Product product , PurchaseFailureDescription failureDescription)
        {
            DisplayService.ShowPopup(UIPopupName.PopupPurchaseFail);
            onFailure?.Invoke();
        }
    }

}


