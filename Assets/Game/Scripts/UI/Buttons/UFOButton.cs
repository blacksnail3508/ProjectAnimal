using LazyFramework;
using System;
using TMPro;
using UnityEngine;

public class UFOButton : ButtonBase
{

    [SerializeField] GameConfig gameConfig;
    [SerializeField] TMP_Text priceText;
    private void Awake()
    {
        priceText.text=$"{gameConfig.Economy.UFOCost}";
    }
    public override void OnClick()
    {
        base.OnClick();

        if (!GameServices.UndoEnable) return;
        StartUfo();
    }

    private void StartUfo()
    {
        Action onSuccess = () =>
        {
            GameServices.StartUfoMode();
            DisplayService.ShowPopup(UIPopupName.PopupUfo);
        };
        Action onFail = () =>
        {
            DisplayService.ShowPopup(UIPopupName.PopupBonusCoin);
        };

        CurrencyService.Spend(gameConfig.Economy.UFOCost, onSuccess, onFail);
    }
}
