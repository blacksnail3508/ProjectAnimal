using LazyFramework;
public class SkinButton : ButtonBase
{
    public override void OnClick()
    {
        base.OnClick();
        ShowMenuSkin();
    }

    private void ShowMenuSkin()
    {
        DisplayService.ShowMenu(UIMenuName.SkinMenu);
        GameServices.EndLevel();
    }
    public override void PlaySound()
    {
        AudioService.PlaySound(AudioName.Select);
    }
}
