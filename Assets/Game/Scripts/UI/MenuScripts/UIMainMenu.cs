using LazyFramework;
using UnityEngine;
public class UIMainMenu : UIMenuBase
{
    [SerializeField] LevelAsset levelAsset;

    [SerializeField] GameObject adsButton;
    [SerializeField] GameObject rateButton;
    protected override void OnEnable()
    {
        base.OnEnable();
        AudioService.PlayMusic(AudioName.BG1);
        Reload();
    }
    public void OnPlayBtn()
    {
        if (PlayerService.Level>=levelAsset.listLevel.Count)
        {
            PlayerService.PlayLevel(levelAsset.listLevel.Count-1);
            DisplayService.ShowMenu(UIMenuName.MenuGameplay);
            return;
        }
        PlayerService.PlayLevel(PlayerService.Level);
        DisplayService.ShowMenu(UIMenuName.MenuGameplay);
    }
    public void OnSelectBtn()
    {
        DisplayService.ShowPopup(UIPopupName.PopupSelect);
        AudioService.PlaySound(AudioName.Select);
    }
    private void Reload()
    {
        if (PlayerService.IsRated==1)
        {
            rateButton.SetActive(false);
        }
        else
        {
            rateButton.SetActive(true);
        }

        if (AdsService.IsRemoveAds==true)
        {
            adsButton.SetActive(false);
        }
        else
        {
            adsButton.SetActive(true);
        }
    }
    private void OnReloadUI(OnReloadUI e)
    {
        Reload();
    }
    public override void Subscribe()
    {
        Event<OnReloadUI>.Subscribe(OnReloadUI);
    }
    public override void Unsubscribe()
    {
        Event<OnReloadUI>.Unsubscribe(OnReloadUI);
    }
}
