using LazyFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupLose : UIPopupBase
{
    [SerializeField] LevelAsset levelAsset;
    [SerializeField] Button skip;
    protected override void OnShow()
    {
        base.OnShow();
        AudioService.PlaySound(AudioName.Lose);

        if (PlayerService.CurrentLevel >= levelAsset.listLevel.Count-1)
        {
            Bug.Log("no skipable");
            skip.interactable=false;
        }
        else
        {
            skip.interactable=true;
        }

    }
    public void OnReplay()
    {
        AudioService.PlaySound(AudioName.Button);

        PlayerService.ReplayLevel();
        DisplayService.ShowMenu(UIMenuName.MenuGameplay);
        Hide();

    }
    public void OnNext()
    {
        AudioService.PlaySound(AudioName.Button);

        Action onSuccess = () =>
        {
            PlayerService.Level=Mathf.Max(PlayerService.CurrentLevel+1 , PlayerService.Level);
            PlayerService.NextLevel();
            DisplayService.ShowMenu(UIMenuName.MenuGameplay);
            Hide();
        };
        Action onFailed = () =>
        {

        };

        AdsService.ShowReward("skip" , onSuccess , onFailed);
    }
}
