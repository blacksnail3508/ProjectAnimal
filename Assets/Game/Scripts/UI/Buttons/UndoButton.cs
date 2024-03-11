using LazyFramework;

public class UndoButton : ButtonBase
{
    public override void OnClick()
    {
        base.OnClick();
        Undo();
    }

    private void Undo()
    {
        if(GameServices.Undo() == false)
        {
            //sound negative
            //AudioService.PlaySound();
        }
        else
        {
            //sound positive
        }
    }
}
