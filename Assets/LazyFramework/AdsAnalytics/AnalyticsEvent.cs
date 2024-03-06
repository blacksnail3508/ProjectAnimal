using UnityEngine;

public class AnalyticsEvent : MonoBehaviour
{
    public static string LEVEL_START = "level_start";
    public static string LEVEL_WIN = "level_win";
    public static string LEVEL_LOSE = "level_lose";
    public static string REWARDED_VIDEO_SHOW = "rewarded_video_show";
    public static string INTERSTITIAL_SHOW = "interstitial_show";
    public static string INAPPPURCHASE = "in_app_purchase";
}
public static class UserProperties
{
    public static string LEVEL_REACH = "level_reach";
    public static string DAYS_PLAYING = "days_playing";
}
public static class AnalyticParamKey
{
    public static string TIME = "time";
    public static string LEVEL = "level";
    public static string PLACEMENT = "placement";
    public static string PRODUCT = "package";
}
