using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LazyFramework;
public class WinButton : ButtonBase
{
    [SerializeField] LevelAsset levelLibrary;
    public override void OnClick()
    {
        base.OnClick();

        PlayerService.UpdateLevel();
        GameServices.AnimalCelebrate();
        //all animal safe
        if (PlayerService.IsLevelUp==true)
        {
            CurrencyService.AddCoin(levelLibrary.listLevel[PlayerService.CurrentLevel].CoinReward);
        }
        else
        {
            CurrencyService.AddCoin(5);
        }

        ShowPopupWin();
    }
    private void ShowPopupWin()
    {
        DisplayService.ShowPopup(UIPopupName.PopupWin);
    }
}
