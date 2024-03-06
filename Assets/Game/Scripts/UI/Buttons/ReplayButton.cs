using LazyFramework;
public class ReplayButton : ButtonBase
{
    public override void OnClick()
    {
        base.OnClick();
        Replay();
    }
    private void Replay()
    {
        PlayerService.ReplayLevel();
        DisplayService.HideAllPopup();
        DisplayService.ShowMenu(UIMenuName.MenuGameplay);
    }
}
