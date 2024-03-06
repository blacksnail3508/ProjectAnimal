using LazyFramework;

public class RemoveAdsButton : ButtonBase
{
    public override void OnClick()
    {
        base.OnClick();
        ShowPopupRemoveAds();
    }
    private void ShowPopupRemoveAds()
    {
        DisplayService.ShowPopup(UIPopupName.PopupRemoveAds);
    }
    public override void PlaySound()
    {
        AudioService.PlaySound(AudioName.ADS_Star);
    }
}
