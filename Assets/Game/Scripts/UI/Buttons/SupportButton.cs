using LazyFramework;

public class SupportButton : ButtonBase
{
    public override void OnClick()
    {
        base.OnClick();
        BackToMenu();
    }

    private void BackToMenu()
    {
        DisplayService.HideAllMenu();
        DisplayService.HideAllPopup();
        GameServices.EndLevel();
        DisplayService.ShowMenu(UIMenuName.MenuMain);
    }
}
