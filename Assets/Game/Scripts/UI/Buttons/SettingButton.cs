using LazyFramework;

public class SettingButton : ButtonBase
{
    public override void OnClick()
    {
        base.OnClick();
        ShowSetting();
    }
    private void ShowSetting()
    {
        DisplayService.ShowPopup(UIPopupName.PopupSetting);
    }
}
