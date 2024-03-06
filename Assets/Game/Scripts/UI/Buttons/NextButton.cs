using LazyFramework;

public class NextButton : ButtonBase
{
    public override void OnClick()
    {
        base.OnClick();
        Next();
    }

    private void Next()
    {
        PlayerService.NextLevel();
        DisplayService.ShowMenu(UIMenuName.MenuGameplay);
        DisplayService.HideAllPopup();
    }
}
