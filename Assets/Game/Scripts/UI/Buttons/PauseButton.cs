using LazyFramework;

public class PauseButton : ButtonBase
{
    public override void OnClick()
    {
        base.OnClick();
        Pause();
    }

    private void Pause()
    {
        DisplayService.ShowPopup(UIPopupName.PopupPause);
    }
}
