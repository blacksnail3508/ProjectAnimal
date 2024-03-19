using DG.Tweening;
using LazyFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupWin : UIPopupBase
{
    [Header("Data")]
    [SerializeField] LevelAsset levelAsset;
    [Header("Quotes")]
    [SerializeField] Image awesome;
    [SerializeField] Image good;
    [SerializeField] Image nice;
    [SerializeField] Image passed;
    [Header("text")]
    [SerializeField] TMP_Text levelText;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject homeButton;

    [SerializeField] UnlockSkateSlider skateUnlocker;
    [SerializeField] Image clickBlock;
    private void Start()
    {
        DisableCommon();
        director.stopped += delegate
        {
            //int best = levelAsset.listLevel[PlayerService.CurrentLevel].dificulty;

            //if (GameServices.PlayerMove<=best)
            //{
            //    //show Awesome
            //    QuoteAnimation(awesome);
            //}
            //else if (GameServices.PlayerMove>best&&GameServices.PlayerMove<=best+2)
            //{
            //    //show good
            //    QuoteAnimation(good);

            //}
            //else if (GameServices.PlayerMove>best+2&&GameServices.PlayerMove<best+5)
            //{
            //    //show nice
            //    QuoteAnimation(nice);

            //}
            //else
            //{
            //    //show atleast you passed
            //    QuoteAnimation(passed);
            //}

            //show Awesome
            QuoteAnimation(awesome);
        };
    }
    public void QuoteAnimation(Image quote)
    {
        quote.gameObject.SetActive(true);
        quote.DOKill();

        quote.transform.localScale=new Vector3(1 , 0 , 1);
        quote.transform.DOScaleY(1 , 0.5f);

        quote.transform.DOScaleY(0 , 0.5f).SetDelay(1.5f).OnComplete(() =>
        {
            quote.gameObject.SetActive(false);
            clickBlock.gameObject.SetActive(false);
        });
    }
    public void OnReplay()
    {
        PlayerService.ReplayLevel();
        DisplayService.ShowMenu(UIMenuName.MenuGameplay);
        Hide();
    }
    public void OnPrevious()
    {
        PlayerService.PreviousLevel();
        DisplayService.ShowMenu(UIMenuName.MenuGameplay);
        Hide();
    }
    protected override void OnShow()
    {
        clickBlock.gameObject.SetActive(true);
        base.OnShow();
        levelText.text=$"Level {PlayerService.CurrentLevel+1}";
        AudioService.PlaySound(AudioName.Win);

        //Bug.Log($"current level {PlayerService.CurrentLevel}, max level = {levelAsset.listLevel.Count}");
        if(PlayerService.CurrentLevel >= levelAsset.listLevel.Count-1)
        {
            nextButton.gameObject.SetActive(false);
            homeButton.gameObject.SetActive(true);
        }
        else
        {
            nextButton.gameObject.SetActive(true);
            homeButton.gameObject.SetActive(false);
        }
        //get current best solution move count

        skateUnlocker.Show();
    }
    private void DisableCommon()
    {
        awesome.gameObject.SetActive(false);
        good.gameObject.SetActive(false);
        nice.gameObject.SetActive(false);
        passed.gameObject.SetActive(false);
    }
}
