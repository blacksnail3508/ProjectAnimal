using LazyFramework;

public class UIPopupBonusCoin : UIPopupBase
{
    public void OnWatchAds()
    {
        //Action OnSuccess = () =>
        //{
        //    CurrencyService.AddCoin(200);
        //Hide();
        //};
        //Action OnFail = () =>
        //{

        //};

        //AdsService.ShowReward("bonus_coin",OnSuccess,OnFail);

        CurrencyService.AddCoin(200);
        Hide();
    }
}
