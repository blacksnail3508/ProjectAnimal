using LazyFramework;
using System;
using TMPro;
using UnityEngine;

public class HintButton : ButtonBase
{
    [SerializeField] TMP_Text priceText;
    [SerializeField] GameConfig gameConfig;

    private void Awake()
    {
        priceText.text = gameConfig.Economy.HintCost.ToString();
    }

    public override void OnClick()
    {
        base.OnClick();

        if (!GameServices.UndoEnable) return;
        ShowHint();
    }

    private void ShowHint()
    {
        Action onSuccess = () => {
            DisplayService.ShowPopup(UIPopupName.PopupHint);
        };

        Action onFail = () => {
            DisplayService.ShowPopup(UIPopupName.PopupBonusCoin);
        };

        CurrencyService.Spend(gameConfig.Economy.HintCost , onSuccess , onFail);
    }
}
