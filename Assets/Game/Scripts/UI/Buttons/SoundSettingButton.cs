using LazyFramework;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingButton : ButtonBase
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
        if (AudioService.IsSoundOn)
        {
            TurnOffSound();
        }
        else
        {
            TurnOnSound();
        }
    }
    private void ChangeIcon()
    {
        if (AudioService.IsSoundOn)
        {
            settingIcon.sprite = iconOn;
        }
        else
        {
            settingIcon.sprite = iconOff;
        }
    }
    private void TurnOnSound()
    {
        AudioService.SetSound(true);
    }
    private void TurnOffSound()
    {
        AudioService.SetSound(false);
    }

    private void OnChangeSoundSetting(OnChangeSoundSetting e)
    {
        ChangeIcon();
    }

    private void Subscribe()
    {
        Event<OnChangeSoundSetting>.Subscribe(OnChangeSoundSetting);
    }
    private void Unsubscribe()
    {
        Event<OnChangeSoundSetting>.Unsubscribe(OnChangeSoundSetting);
    }
}
