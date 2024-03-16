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
    protected override void Start()
    {
        base.Start();
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

    protected override void Subscribe()
    {
        Event<OnChangeSoundSetting>.Subscribe(OnChangeSoundSetting);
    }
    protected override void Unsubscribe()
    {
        Event<OnChangeSoundSetting>.Unsubscribe(OnChangeSoundSetting);
    }
}
