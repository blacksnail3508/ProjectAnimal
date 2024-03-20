using LazyFramework;
using System;
using UnityEngine.Purchasing.Extension;

public class UndoButton : ButtonBase
{
    public override void OnClick()
    {
        base.OnClick();
        Undo();
    }

    private void Undo()
    {
        //GameServices.Undo();

        if (!GameServices.UndoEnable) return;

        if (GameServices.History.IsUndoable())
        {
            //success on spend coin
            Action OnSuccess = () =>
            {
                //undo move
                var lastMove = GameServices.History.Pop();
                lastMove.Undo();
                AudioService.PlaySound(AudioName.Undo);
            };

            //failed on spend coin
            Action OnFailed = () =>
            {
                DisplayService.ShowPopup(UIPopupName.PopupBonusCoin);
            };

            CurrencyService.Spend(5 , OnSuccess , OnFailed);
        }
    }
}
