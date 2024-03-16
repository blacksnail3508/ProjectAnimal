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
    protected override void Start()
    {
        base.Start();
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

    protected override void Unsubscribe()
    {
        Event<OnChangeMusicSetting>.Unsubscribe(OnChangeMusicSetting);
    }
    protected override void Subscribe()
    {
        Event<OnChangeMusicSetting>.Subscribe(OnChangeMusicSetting);
    }
}
