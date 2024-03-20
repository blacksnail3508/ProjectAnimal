using LazyFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupHint : UIPopupBase
{
    [SerializeField] LevelAsset levelLibrary;
    [SerializeField] Image hintImage;
    [SerializeField] TMP_Text levelText;
    protected override void OnShow()
    {
        base.OnShow();
        levelText.text=$"Level {PlayerService.CurrentLevel + 1 }";
        hintImage.sprite=levelLibrary.GetHint(PlayerService.CurrentLevel);
    }
}
