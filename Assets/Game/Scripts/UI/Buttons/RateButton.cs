using LazyFramework;

public class RateButton : ButtonBase
{
    public override void OnClick()
    {
        base.OnClick();
        ShowPopupRate();
    }
    private void ShowPopupRate()
    {
        DisplayService.ShowPopup(UIPopupName.PopupRate);
    }
    public override void PlaySound()
    {
        AudioService.PlaySound(AudioName.ADS_Star);
    }
}
