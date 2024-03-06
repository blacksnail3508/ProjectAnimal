using LazyFramework;
public class HomeButton : ButtonBase
{
    public override void OnClick()
    {
        base.OnClick();
        ShowHomeMenu();
    }
    private void ShowHomeMenu()
    {
        DisplayService.ShowMenu(UIMenuName.MenuMain);
    }
}
