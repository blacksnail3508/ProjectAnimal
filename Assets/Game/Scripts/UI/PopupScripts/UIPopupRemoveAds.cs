using LazyFramework;
using System;
using TMPro;
using UnityEngine;

public class UIPopupRemoveAds : UIPopupBase
{
    [SerializeField] TMP_Text priceText;

    private void Start()
    {
        priceText.text = IAPService.GetPrice(IAPProduct.ads);
    }
    public void PurchaseRemoveAds()
    {
        Action OnSuccess = () =>
        {
            DisplayService.ShowPopup(UIPopupName.PopupPurchaseSuccess);
            AdsService.IsRemoveAds = true;
            AdsService.HideBanner();
            Hide();
            DisplayService.ReloadUI();
        };

        Action OnFailure = () =>
        {
            DisplayService.ShowPopup(UIPopupName.PopupPurchaseFail);
            Hide();
        };

        IAPService.Purchase(IAPProduct.ads,OnSuccess,OnFailure);
    }
}
