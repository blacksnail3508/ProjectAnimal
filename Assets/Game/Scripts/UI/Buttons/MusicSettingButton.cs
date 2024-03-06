using LazyFramework;
using UnityEngine;
using UnityEngine.UI;

public class MusicSettingButton : ButtonBase
{
    [SerializeField] Image settingIcon;
    [SerializeField] Sprite iconOn;
    [SerializeField] Sprite iconOff;
    private void Awake()
    {
        Subscribe();
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }
    private void Start()
    {
        ChangeIcon();
    }
    public override void OnClick()
    {
        base.OnClick();
        if (AudioService.IsMusicOn)
        {
            TurnOffMusic();
        }
        else
        {
            TurnOnMusic();
        }
    }
    private void ChangeIcon()
    {
        if (AudioService.IsMusicOn)
        {
            settingIcon.sprite=iconOn;
        }
        else
        {
            settingIcon.sprite=iconOff;
        }
    }
    private void TurnOnMusic()
    {
        AudioService.SetMusic(true);
    }
    private void TurnOffMusic()
    {
        AudioService.SetMusic(false);
    }

    private void OnChangeMusicSetting(OnChangeMusicSetting e)
    {
        ChangeIcon();
    }

    private void Subscribe()
    {
        Event<OnChangeMusicSetting>.Subscribe(OnChangeMusicSetting);
    }
    private void Unsubscribe()
    {
        Event<OnChangeMusicSetting>.Unsubscribe(OnChangeMusicSetting);
    }
}
