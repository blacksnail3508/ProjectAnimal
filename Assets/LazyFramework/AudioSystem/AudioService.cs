using UnityEngine;
namespace LazyFramework
{
    public static class AudioService
    {
        public static bool IsMusicOn { get => PlayerPrefs.GetInt(KeyString.IsMusicOn , 1) == 0 ? false : true;}
        public static void SetMusic(bool isOn)
        {
            PlayerPrefs.SetInt(KeyString.IsMusicOn , isOn == false ? 0 : 1);

            Event<OnChangeMusicSetting>.Post(new OnChangeMusicSetting(isOn));
        }
        public static bool IsSoundOn { get => PlayerPrefs.GetInt(KeyString.IsSoundOn , 1) == 0 ? false : true;}
        public static void SetSound(bool isOn)
        {
            PlayerPrefs.SetInt(KeyString.IsSoundOn , isOn==false ? 0 : 1);

            Event<OnChangeSoundSetting>.Post(new OnChangeSoundSetting(isOn));
        }
        public static bool IsVibrationOn { get => PlayerPrefs.GetInt(KeyString.IsVibrationOn , 1) == 0 ? false : true;
            set => PlayerPrefs.GetInt(KeyString.IsVibrationOn , value == false ? 0 : 1); }
        public static void SetVibration(bool isOn)
        {
            PlayerPrefs.SetInt(KeyString.IsVibrationOn , isOn == false ? 0 : 1);

            Event<OnChangeVibrationSetting>.Post(new OnChangeVibrationSetting(isOn));
        }
        //play
        public static void PlaySound(string name , bool? isLoop = false)
        {
            Event<OnPlaySound>.Post(new OnPlaySound(name , isLoop));
        }
        public static void StopSound(string name)
        {
            Event<OnStopSound>.Post(new OnStopSound(name));
        }
        public static void PlayMusic(string name)
        {
            Event<OnPlayMusic>.Post(new OnPlayMusic(name));
        }
    }
}
