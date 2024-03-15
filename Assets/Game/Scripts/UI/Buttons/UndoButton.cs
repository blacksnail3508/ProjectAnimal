using LazyFramework;
using System;

public class UndoButton : ButtonBase
{
    public override void OnClick()
    {
        base.OnClick();
        Undo();
    }

    private void Undo()
    {
        GameServices.Undo();
    }
}
