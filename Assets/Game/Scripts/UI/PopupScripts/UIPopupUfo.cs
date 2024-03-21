using DG.Tweening;
using LazyFramework;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupUfo : UIPopupBase
{
    [SerializeField] GameConfig config;
    [SerializeField] Image bg;

    Color whiteTransparent = new Color(1,1,1,0.2f);
    Color redTransparent = new Color(1,0,0,0.2f);
    protected override void OnShow()
    {
        base.OnShow();

        bg.DOKill();

        bg.color = whiteTransparent;
        bg.DOColor(redTransparent , 0.75f).SetLoops(-1 , LoopType.Yoyo).SetEase(Ease.Linear);
    }
    public void StopUfoMode()
    {
        //stop
        GameServices.StopUfoMode();

        // refund
        CurrencyService.AddCoin(config.Economy.UFOCost);
    }
}
