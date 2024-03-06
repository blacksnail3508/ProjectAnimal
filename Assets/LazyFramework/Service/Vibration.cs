using LazyFramework;
using UnityEngine;
public static class Vibration
{
    public static void Vibrate()
    {
        if (PlayerPrefs.GetInt(KeyString.IsVibrationOn , 1)==0) return;

        Handheld.Vibrate();
    }
}