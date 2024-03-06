
using System.Collections.Generic;
using UnityEngine;
namespace LazyFramework
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Audio source")]
        [Tooltip("Scriptable object that store clips")][SerializeField] AudioData audioData;
        [Tooltip("an audio source")][SerializeField] AudioSource musicSource;
        [Tooltip("Just make a game object to store sounce effect")][SerializeField] GameObject soundRoot;

        [Header("Dictionary")]
        Dictionary<string , AudioSource> soundDic = new Dictionary<string , AudioSource>();
        Dictionary<string , AudioClip> musicDic = new Dictionary<string , AudioClip>();
        private void Awake()
        {
            musicSource.loop=true;

            foreach (var item in audioData.listMusic)
            {
                musicDic.Add(item.name , item.audioClip);
            }

            foreach (var audio in audioData.listSound)
            {
                var audioSource = soundRoot.AddComponent<AudioSource>();
                audioSource.clip=audio.audioClip;
                audioSource.loop=false;
                soundDic.Add(audio.name , audioSource);
            }

            //LoadDataVolume();
            SubscribeEvent();
        }

        //private void LoadDataVolume()
        //{
        //    if (!PlayerPrefs.HasKey( SOUND_VOLUME))
        //    {
        //        PlayerPrefs.SetInt( SOUND_VOLUME , 100);
        //    }
        //    if (!PlayerPrefs.HasKey( MUSIC_VOLUME))
        //    {
        //        PlayerPrefs.SetInt( MUSIC_VOLUME , 100);
        //    }

        //    var volumeSound = PlayerPrefs.GetInt( SOUND_VOLUME);
        //    var volumeMusic = PlayerPrefs.GetInt( MUSIC_VOLUME);

        //    musicSource.volume=volumeMusic/100f;

        //    foreach (var item in soundDic)
        //    {
        //        item.Value.volume=volumeSound/100f;
        //    }
        //}
        #region Event
        private void OnPlaySound(OnPlaySound e)
        {
            if (AudioService.IsSoundOn)
            {
                soundDic[e.name].Play();
                soundDic[e.name].loop=e.isLoop;
            }
        }
        private void OnStopSound(OnStopSound e)
        {
            soundDic[e.name].Stop();
        }
        private void OnPlayMusic(OnPlayMusic e)
        {
            if (musicSource.clip == musicDic[e.name] && musicSource.isPlaying)
            {
                //do nothing
            }
            else
            {
                musicSource.clip = musicDic[e.name];
                if (AudioService.IsMusicOn)
                {
                    musicSource.Play();
                }
            }
        }

        private void OnChangeSoundSetting(OnChangeSoundSetting e)
        {
            //mute
            if (e.isSoundOn == false)
            {
                foreach (var sound in soundDic)
                {
                    sound.Value.Stop();
                }
            }
        }
        private void OnChangeMusicSetting(OnChangeMusicSetting e)
        {
            if (e.isMusicOn == false)
            {
                musicSource.Stop();
            }
            else
            {
                if (!musicSource.isPlaying)
                {
                    musicSource.Play();
                }
            }
        }
        #endregion

        private void OnDestroy()
        {
            UnsubscribeEvent();
        }
        private void SubscribeEvent()
        {
            Event<OnPlayMusic>.Subscribe(OnPlayMusic);
            Event<OnPlaySound>.Subscribe(OnPlaySound);
            Event<OnStopSound>.Subscribe(OnStopSound);
            //Event<OnPlayOneshot>.Subscribe(OnPlayOneshot);
            Event<OnChangeMusicSetting>.Subscribe(OnChangeMusicSetting);
            Event<OnChangeSoundSetting>.Subscribe(OnChangeSoundSetting);
        }
        private void UnsubscribeEvent()
        {
            Event<OnPlayMusic>.Unsubscribe(OnPlayMusic);
            Event<OnPlaySound>.Unsubscribe(OnPlaySound);
            Event<OnStopSound>.Unsubscribe(OnStopSound);
            //Event<OnPlayOneshot>.Unsubscribe(OnPlayOneshot);
            Event<OnChangeMusicSetting>.Unsubscribe(OnChangeMusicSetting);
            Event<OnChangeSoundSetting>.Unsubscribe(OnChangeSoundSetting);
        }
    }
}
