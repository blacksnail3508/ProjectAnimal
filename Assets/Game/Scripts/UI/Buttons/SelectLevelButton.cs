using LazyFramework;

public class SelectLevelButton : ButtonBase
{
    public override void OnClick()
    {
        base.OnClick();
        ShowPopupSelect();
    }
    private void ShowPopupSelect()
    {

        DisplayService.ShowPopup(UIPopupName.PopupSelect);
        DisplayService.HideAllMenu();
    }
    public override void PlaySound()
    {
        AudioService.PlaySound(AudioName.Select);
    }
}
