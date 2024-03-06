using UnityEngine;
namespace LazyFramework
{
    public static class Bug
    {
        public static void LogAds()
        {
#if UNITY_EDITOR
            Debug.Log("Watch ads!");
#endif
        }
        public static void Log(string message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }
        public static void LogWarning(string message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(message);
#endif
        }
        public static void LogError(string message)
        {
#if UNITY_EDITOR
            Debug.LogError(message);
#endif
        }
        public static void LogIap()
        {
#if UNITY_EDITOR
            Debug.LogWarning("IAP Purchase success");
#endif
        }
    }
}


