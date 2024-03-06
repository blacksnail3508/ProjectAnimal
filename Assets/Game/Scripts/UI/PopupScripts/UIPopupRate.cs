using LazyFramework;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupRate : UIPopupBase
{
    string url = "https://play.google.com/store/apps/details?id=com.archer.shoot.cone";
    [SerializeField] Image star1;
    [SerializeField] Image star2;
    [SerializeField] Image star3;
    [SerializeField] Image star4;
    [SerializeField] Image star5;
    public void Rate1Star()
    {
        star1.gameObject.SetActive(true);
        star2.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);
        star4.gameObject.SetActive(false);
        star5.gameObject.SetActive(false);
        Application.OpenURL(url);
        PlayerService.IsRated=1;
        DisplayService.ReloadUI();
    }
    public void Rate2Star()
    {
        star1.gameObject.SetActive(true);
        star2.gameObject.SetActive(true);
        star3.gameObject.SetActive(false);
        star4.gameObject.SetActive(false);
        star5.gameObject.SetActive(false);
        Application.OpenURL(url);
        PlayerService.IsRated=1;
        DisplayService.ReloadUI();
    }
    public void Rate3Star()
    {
        star1.gameObject.SetActive(true);
        star2.gameObject.SetActive(true);
        star3.gameObject.SetActive(true);
        star4.gameObject.SetActive(false);
        star5.gameObject.SetActive(false);
        Application.OpenURL(url);
        PlayerService.IsRated=1;
        DisplayService.ReloadUI();
    }
    public void Rate4Star()
    {
        star1.gameObject.SetActive(true);
        star2.gameObject.SetActive(true);
        star3.gameObject.SetActive(true);
        star4.gameObject.SetActive(true);
        star5.gameObject.SetActive(false);
        Application.OpenURL(url);
        PlayerService.IsRated=1;
        DisplayService.ReloadUI();
    }
    public void Rate5Star()
    {
        star1.gameObject.SetActive(true);
        star2.gameObject.SetActive(true);
        star3.gameObject.SetActive(true);
        star4.gameObject.SetActive(true);
        star5.gameObject.SetActive(true);
        Application.OpenURL(url);
        PlayerService.IsRated=1;
        DisplayService.ReloadUI();
    }

}
