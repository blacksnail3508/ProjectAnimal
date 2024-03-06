using System;
using System.Collections.Generic;
using UnityEngine;
namespace LazyFramework
{
    [CreateAssetMenu(fileName = "AudioData" , menuName = "ScriptableObject/AudioData" , order = 7)]
    public class AudioData : ScriptableObject
    {
        public List<AudioClipData> listMusic = new List<AudioClipData>();
        public List<AudioClipData> listSound = new List<AudioClipData>();
    }
    [Serializable]
    public class AudioClipData
    {
        public string name;
        public AudioClip audioClip;
        [Range(0 , 1)] public float volumn = 1;
    }
    public enum AudioType
    {
        Music = 0,
        Sound = 1,
        OneShot = 2,
    }
}
