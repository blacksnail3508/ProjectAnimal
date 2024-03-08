
using System.Collections.Generic;
using UnityEngine;
namespace LazyFramework
{
    public static class PlayerService
    {
        public static int Level { get => PlayerPrefs.GetInt(KeyString.Level , 0); private set => PlayerPrefs.SetInt(KeyString.Level , value); }
        public static int CurrentLevel;
        public static string LastLoginDay { get ; set; }
        public static int IsRated { get => PlayerPrefs.GetInt(KeyString.IsRated , 0); set => PlayerPrefs.SetInt(KeyString.IsRated , value); }
        public static void ReplayLevel()
        {
            PlayLevel(CurrentLevel);
        }
        public static void NextLevel()
        {
            PlayLevel(CurrentLevel + 1);
        }
        public static void PreviousLevel()
        {
            if (CurrentLevel==0)
            {
                Bug.Log("No previous level");
                return;
            }
            else
            {
                PlayLevel(CurrentLevel-1);
            }
        }
        public static void PlayLevel(int level)
        {
            CurrentLevel = level;
            //play
            Event<OnPlayLevel>.Post(new OnPlayLevel(level));
        }
        public static void UpdateLevel()
        {
            Level = Mathf.Max(CurrentLevel+1, Level);
        }
    }
}
