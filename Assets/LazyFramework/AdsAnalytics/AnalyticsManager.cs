using AppsFlyerSDK;
using Firebase.Analytics;
using System.Collections.Generic;
using UnityEngine;
namespace AdsAnalytics
{
    public class AnalyticsManager : Singleton<AnalyticsManager>
    {
        #region FIREBASE
        public static void SetFirebaseUserProperties(string name , string property)
        {
            if (!GameServices.Instance.FirebaseReady)
                return;

#if FIREBASE
        FirebaseAnalytics.SetUserProperty(name, property);
#endif
        }
        public static void LogEvent(string eventName , params Parameter[] parameters)
        {
            if (!GameServices.Instance.FirebaseReady)
                return;

#if FIREBASE
#if UNITY_EDITOR
        Debug.Log("<color=yellow>Firebase: " + eventName + "</color>");
#endif
        FirebaseAnalytics.LogEvent(eventName, parameters);
#endif
        }
        public static void LogEvent(string eventName , int paramValue)
        {
            if (!GameServices.Instance.FirebaseReady)
                return;

#if FIREBASE
#if UNITY_EDITOR
        Debug.Log("<color=yellow>Firebase: " + eventName + "</color>");
#endif
        FirebaseAnalytics.LogEvent(eventName, paramValue);
#endif
        }
        public static void LogEventLevelStart(int level)
        {
            LogEvent(AnalyticsEvent.LEVEL_START , level);
        }
        public static void LogEventLevelWin(int paramLevel , float paramTime)
        {
            LogEvent(AnalyticsEvent.LEVEL_WIN , new Parameter[] {
            new Parameter(AnalyticParamKey.LEVEL,paramLevel),
            new Parameter(AnalyticParamKey.TIME,paramTime)
        });
            SetPropertiesLevelReach(paramLevel);
        }
        public static void LogEventLevelLose(int paramLevel , float paramTime)
        {
            LogEvent(AnalyticsEvent.LEVEL_LOSE , new Parameter[] {
            new Parameter(AnalyticParamKey.LEVEL,paramLevel),
            new Parameter(AnalyticParamKey.TIME,paramTime)
        });
        }
        public static void LogEventRewardedVideoShow(int paramLevel , string paramPlacement)
        {
            LogEvent(AnalyticsEvent.REWARDED_VIDEO_SHOW , new Parameter[] {
            new Parameter(AnalyticParamKey.LEVEL,paramLevel),
            new Parameter(AnalyticParamKey.PLACEMENT,paramPlacement)
        });
        }
        public static void LogEventInterstitialShow(int paramLevel , string paramPlacement)
        {
            LogEvent(AnalyticsEvent.INTERSTITIAL_SHOW , new Parameter[] {
            new Parameter(AnalyticParamKey.LEVEL,paramLevel),
            new Parameter(AnalyticParamKey.PLACEMENT,paramPlacement)
        });
        }
        public static void LogInAppPurchase(int paramLevel , string paramPackage)
        {
            LogEvent(AnalyticsEvent.INAPPPURCHASE , new Parameter[] {
            new Parameter(AnalyticParamKey.LEVEL,paramLevel),
            new Parameter(AnalyticParamKey.PRODUCT,paramPackage)
        });
        }
        public static void SetPropertiesDayPlaying(int param)
        {
            SetFirebaseUserProperties(UserProperties.DAYS_PLAYING , param.ToString());
        }
        public static void SetPropertiesLevelReach(int param)
        {
            SetFirebaseUserProperties(UserProperties.LEVEL_REACH , param.ToString());
        }
        #endregion



        #region APPFLYER

        public static void AppflyerInterstitialShow()
        {
            AppsFlyer.sendEvent("Interstitial_show" , new Dictionary<string , string>() { { "Interstitial_show" , "Interstitial_show" } });
#if UNITY_EDITOR
            Debug.Log("ShowInter");
#endif
        }
        public static void AppflyerRewardedVideoShow()
        {
            AppsFlyer.sendEvent("rewarded_video_show" , new Dictionary<string , string>() { { "rewarded_video_show" , "rewarded_video_show" } });
#if UNITY_EDITOR
            Debug.Log("ShowRewarded");
#endif
        }
        #endregion
    }
}

