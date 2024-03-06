using System;
using UnityEngine;

namespace LazyFramework
{
    public static class AdsService
    {
        public static void ShowInter(string placement,Action onSuccess = null , Action onFailed = null)
        {
            Event<OnShowAdsInter>.Post(new OnShowAdsInter(placement,onSuccess , onFailed));
        }
        public static void ShowReward(string placement , Action onSuccess = null , Action onFailed = null)
        {
            Event<OnShowAdsReward>.Post(new OnShowAdsReward(placement,onSuccess , onFailed));
        }
        public static void ShowBanner()
        {
            Event<OnShowAdsBanner>.Post(new OnShowAdsBanner());
        }
        public static void HideBanner()
        {
            Event<OnHideAdsBanner>.Post(new OnHideAdsBanner());
        }
        public static bool IsRemoveAds { get => PlayerPrefs.GetInt(KeyString.IsRemoveAds , 0)==0 ? false : true; set => PlayerPrefs.SetInt(KeyString.IsRemoveAds , value==true ? 1 : 0); }
        public static bool IsShowRemoveAdsPopup { get => PlayerPrefs.GetInt(KeyString.IsShowRemoveAdsPopup , 0)==1; set => PlayerPrefs.SetInt(KeyString.IsShowRemoveAdsPopup , value==true ? 1 : 0); }
    }
    public static class AdsConfig
    {

    }
}

