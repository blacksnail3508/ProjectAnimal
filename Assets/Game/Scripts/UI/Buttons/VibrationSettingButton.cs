using LazyFramework;
using UnityEngine;
using UnityEngine.UI;

public class VibrationSettingButton : ButtonBase
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
        if (AudioService.IsVibrationOn)
        {
            TurnOffVibration();
        }
        else
        {
            TurnOnVibration();
        }
    }
    private void ChangeIcon()
    {
        if (AudioService.IsVibrationOn)
        {
            settingIcon.sprite=iconOn;
        }
        else
        {
            settingIcon.sprite=iconOff;
        }
    }
    private void TurnOnVibration()
    {
        AudioService.SetVibration(true);
    }
    private void TurnOffVibration()
    {
        AudioService.SetVibration(false);
    }

    private void OnChangeVibrationSetting(OnChangeVibrationSetting e)
    {
        ChangeIcon();
    }

    private void Subscribe()
    {
        Event<OnChangeVibrationSetting>.Subscribe(OnChangeVibrationSetting);
    }
    private void Unsubscribe()
    {
        Event<OnChangeVibrationSetting>.Unsubscribe(OnChangeVibrationSetting);
    }
}
