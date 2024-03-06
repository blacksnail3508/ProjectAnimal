using AdsAnalytics;
using LazyFramework;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    [SerializeField] string YOUR_APP_KEY = "1d3b5c495";
    private Action onSuccess;
    private Action onFail;
    private bool isBanner = false;
    private bool isReadyToShowInter = true;
    private int delayInter = 15;
    private void Awake()
    {
        IronSource.Agent.init(YOUR_APP_KEY , IronSourceAdUnits.REWARDED_VIDEO , IronSourceAdUnits.INTERSTITIAL , IronSourceAdUnits.BANNER);
        IronSource.Agent.validateIntegration();

        if (TimeUtils.CalculateDayPastFromLastLogin() > 0)
        {
            AdsService.IsShowRemoveAdsPopup = true;
        }
        TimeUtils.SetLoginDayAsToday();

        if (PlayerPrefs.HasKey("GDPR"))
        {
            SetConsent();
        }

    }
    private void Start()
    {
        SubscribeGameEvent();
        //subscribe event
        SubscribeRewardEvent();
        SubscribeBannerEvent();
        SubscribeInterEvent();

        //load
        LoadBannerAds();
        LoadInterAds();
        LoadRewardAds();

        ShowBanner();
    }
    private void OnDestroy()
    {
        UnsubscribeRewardEvent();
        UnsubscribeBannerEvent();
        UnSubscribeInterEvent();
        UnsubscribeGameEvent();
    }
    public void SetConsent()
    {
        if (PlayerPrefs.GetInt("GDPR")==1)
            IronSource.Agent.setConsent(true);
        else
            IronSource.Agent.setConsent(false);
    }
    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
    private void Reload()
    {
        if(AdsService.IsRemoveAds == true)
        {
            HideBanner();
        }
    }
    private void OnReloadUI(OnReloadUI e)
    {
        Invoke("Reload" , 0.35f);
    }
    private void InvokeSuccessCallback()
    {
        onSuccess?.Invoke();
        onSuccess=null; //clear callback
    }
    private void InvokeFailedCallback()
    {
        onFail?.Invoke();
        onFail=null; //clear callback
    }
    private void OnShowAdsInter(OnShowAdsInter e)
    {
        if (isReadyToShowInter==false) return;
        if (PlayerService.Level <=1 ) return;
        ShowInterAds(e.onSuccess,e.onFail,e.placement);

    }
    private async void DelayShowInterAds()
    {
        isReadyToShowInter=false;
        await Task.Delay(delayInter*1000);
        isReadyToShowInter=true;
    }
    private void OnShowAdsReward(OnShowAdsReward e)
    {
        ShowRewardAds(e.onSuccess , e.onFail);
    }
    private void OnRemoveAds(OnRemoveAds e)
    {
        AdsService.IsRemoveAds = true;
        HideBanner();
        DestroyBanner();
    }
    private void OnShowAdsBanner(OnShowAdsBanner e)
    {
        if (isBanner==false)
        {
            LoadBannerAds();
        }

        ShowBanner();
    }
    private void OnHideAdsBanner(OnHideAdsBanner e)
    {
        HideBanner();
    }
    private void SubscribeGameEvent()
    {
        Event<OnShowAdsInter>.Subscribe(OnShowAdsInter);
        Event<OnShowAdsReward>.Subscribe(OnShowAdsReward);
        //Event<OnGameCount>.Subscribe(OnGameCount);
        Event<OnRemoveAds>.Subscribe(OnRemoveAds);

        Event<OnShowAdsBanner>.Subscribe(OnShowAdsBanner);
        Event<OnHideAdsBanner>.Subscribe(OnHideAdsBanner);
        Event<OnReloadUI>.Subscribe(OnReloadUI);

    }
    private void UnsubscribeGameEvent()
    {
        Event<OnShowAdsInter>.Unsubscribe(OnShowAdsInter);
        Event<OnShowAdsReward>.Unsubscribe(OnShowAdsReward);
        //Event<OnGameCount>.Unsubscribe(OnGameCount);
        Event<OnRemoveAds>.Unsubscribe(OnRemoveAds);
        Event<OnShowAdsBanner>.Unsubscribe(OnShowAdsBanner);
        Event<OnHideAdsBanner>.Unsubscribe(OnHideAdsBanner);
        Event<OnReloadUI>.Unsubscribe(OnReloadUI);
    }

    #region GamePlayInter

    #endregion

    #region Banner Ads
    private void LoadBannerAds()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER , IronSourceBannerPosition.BOTTOM);
    }
    private void ShowBanner()
    {
        if (AdsService.IsRemoveAds == true)
        {
            Bug.Log("Not show banner, user has bought remove ads");
            HideBanner();
        }
        else
        {
            Bug.Log("Show banner");
            IronSource.Agent.displayBanner();
        }
    }
    private void HideBanner()
    {
        Bug.Log("Hide banner");
        IronSource.Agent.hideBanner();

    }
    private void DestroyBanner()
    {
        IronSource.Agent.destroyBanner();
    }
    private void SubscribeBannerEvent()
    {
        //Add AdInfo Banner Events
        IronSourceBannerEvents.onAdLoadedEvent+=BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent+=BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent+=BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent+=BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent+=BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent+=BannerOnAdLeftApplicationEvent;
    }
    private void UnsubscribeBannerEvent()
    {
        //Add AdInfo Banner Events
        IronSourceBannerEvents.onAdLoadedEvent-=BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent-=BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent-=BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent-=BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent-=BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent-=BannerOnAdLeftApplicationEvent;
    }
    /************* Banner AdInfo Delegates *************/
    //Invoked once the banner has loaded
    void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo)
    {
        isBanner = true;
    }
    //Invoked when the banner loading process has failed.
    void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError)
    {
        isBanner = false;
    }
    // Invoked when end user clicks on the banner ad
    void BannerOnAdClickedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Notifies the presentation of a full screen content following user click
    void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Notifies the presented screen has been dismissed
    void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Invoked when the user leaves the app
    void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo)
    {
    }
    #endregion
    #region Inter Ads
    private void LoadInterAds()
    {
        IronSource.Agent.loadInterstitial();
    }
    private void ShowInterAds(Action onSuccess = null, Action onFail = null,string placement = "")
    {
        if(AdsService.IsRemoveAds == true)
        {
            Bug.Log("Removed inter ads");
            onSuccess?.Invoke();
            return;
        }

        AnalyticsManager.LogEventInterstitialShow(PlayerService.Level , placement);
        AnalyticsManager.AppflyerInterstitialShow();
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial(placement);

            this.onSuccess=onSuccess;
            this.onFail=onFail;
        }
        else
        {
            DisplayService.ShowMessage("No inter ads available");
            onFail?.Invoke();
        }
    }
    private void SubscribeInterEvent()
    {
        //Add AdInfo Interstitial Events
        IronSourceInterstitialEvents.onAdReadyEvent+=InterstitialOnAdReadyEvent;
        IronSourceInterstitialEvents.onAdLoadFailedEvent+=InterstitialOnAdLoadFailed;
        IronSourceInterstitialEvents.onAdOpenedEvent+=InterstitialOnAdOpenedEvent;
        IronSourceInterstitialEvents.onAdClickedEvent+=InterstitialOnAdClickedEvent;
        IronSourceInterstitialEvents.onAdShowSucceededEvent+=InterstitialOnAdShowSucceededEvent;
        IronSourceInterstitialEvents.onAdShowFailedEvent+=InterstitialOnAdShowFailedEvent;
        IronSourceInterstitialEvents.onAdClosedEvent+=InterstitialOnAdClosedEvent;
    }
    private void UnSubscribeInterEvent()
    {
        //Add AdInfo Interstitial Events
        IronSourceInterstitialEvents.onAdReadyEvent-=InterstitialOnAdReadyEvent;
        IronSourceInterstitialEvents.onAdLoadFailedEvent-=InterstitialOnAdLoadFailed;
        IronSourceInterstitialEvents.onAdOpenedEvent-=InterstitialOnAdOpenedEvent;
        IronSourceInterstitialEvents.onAdClickedEvent-=InterstitialOnAdClickedEvent;
        IronSourceInterstitialEvents.onAdShowSucceededEvent-=InterstitialOnAdShowSucceededEvent;
        IronSourceInterstitialEvents.onAdShowFailedEvent-=InterstitialOnAdShowFailedEvent;
        IronSourceInterstitialEvents.onAdClosedEvent-=InterstitialOnAdClosedEvent;
    }
    /************* Interstitial AdInfo Delegates *************/
    // Invoked when the interstitial ad was loaded succesfully.
    void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
    {
    }
    // Invoked when the initialization process has failed.
    void InterstitialOnAdLoadFailed(IronSourceError ironSourceError)
    {
    }
    // Invoked when the Interstitial Ad Unit has opened. This is the impression indication.
    void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        Time.timeScale = 0;

    }
    // Invoked when end user clicked on the interstitial ad
    void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo)
    {
    }
    // Invoked when the ad failed to show.
    void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError , IronSourceAdInfo adInfo)
    {
        InvokeFailedCallback();
    }
    // Invoked when the interstitial ad closed and the user went back to the application screen.
    void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Time.timeScale=1;
    }
    // Invoked before the interstitial ad was opened, and before the InterstitialOnAdOpenedEvent is reported.
    // This callback is not supported by all networks, and we recommend using it only if
    // it's supported by all networks you included in your build.
    void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo)
    {
        InvokeSuccessCallback();
        DelayShowInterAds();
        if(AdsService.IsShowRemoveAdsPopup == true) DisplayService.ShowPopup(UIPopupName.PopupRemoveAds);
        LoadInterAds();
    }
    #endregion
    #region Reward Ads
    private void LoadRewardAds()
    {
        IronSource.Agent.loadRewardedVideo();
    }
    private void ShowRewardAds(Action onSuccess = null , Action onFail = null)
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
            this.onSuccess =onSuccess;
            this.onFail = onFail;
        }
        else
        {
            DisplayService.ShowMessage("No ads available");
            onFail?.Invoke();
        }
    }
    private void SubscribeRewardEvent()
    {
        //Add AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent+=RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent+=RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent+=RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent+=RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent+=RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent+=RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent+=RewardedVideoOnAdClickedEvent;
    }
    private void UnsubscribeRewardEvent()
    {
        //Add AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent-=RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent-=RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent-=RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent-=RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent-=RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent-=RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent-=RewardedVideoOnAdClickedEvent;
    }

    /************* RewardedVideo AdInfo Delegates *************/
    // Indicates that there’s an available ad.
    // The adInfo object includes information about the ad that was loaded successfully
    // This replaces the RewardedVideoAvailabilityChangedEvent(true) event
    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
    }
    // Indicates that no ads are available to be displayed
    // This replaces the RewardedVideoAvailabilityChangedEvent(false) event
    void RewardedVideoOnAdUnavailable()
    {
    }
    // The Rewarded Video ad view has opened. Your activity will loose focus.
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        Time.timeScale = 0;
    }
    // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Time.timeScale=1;
    }
    // The user completed to watch the video, and should be rewarded.
    // The placement parameter will include the reward data.
    // When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement , IronSourceAdInfo adInfo)
    {
        InvokeSuccessCallback();
        LoadRewardAds();
    }
    // The rewarded video ad was failed to show.
    void RewardedVideoOnAdShowFailedEvent(IronSourceError error , IronSourceAdInfo adInfo)
    {
        DisplayService.ShowMessage("Ads not completed");
        InvokeFailedCallback();
    }
    // Invoked when the video ad was clicked.
    // This callback is not supported by all networks, and we recommend using it only if
    // it’s supported by all networks you included in your build.
    void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement , IronSourceAdInfo adInfo)
    {
    }
    #endregion
}
