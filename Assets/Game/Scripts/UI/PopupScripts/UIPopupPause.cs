using LazyFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupPause : UIPopupBase
{

    protected override void OnShow()
    {
        base.OnShow();
        //Time.timeScale = 0;
    }
    public void Resume()
    {
        Hide();
    }
    public override void Hide(float? fadeTime = 0.25F)
    {
        base.Hide(fadeTime);
        //Time.timeScale = 1;
    }
    public void BackToMenu()
    {
        Hide();
        DisplayService.ShowMenu(UIMenuName.MenuMain);
        GameServices.EndLevel();
    }

}
