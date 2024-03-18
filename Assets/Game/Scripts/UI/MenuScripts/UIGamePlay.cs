using LazyFramework;
using TMPro;
using UnityEngine;

public class UIGamePlay : UIMenuBase
{
    [SerializeField] TMP_Text moveCountTxt;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject bg;
    [SerializeField] GameObject centerMask;
    protected override void OnEnable()
    {
        base.OnEnable();
        bg.gameObject.SetActive(true);
        centerMask.gameObject.SetActive(true);

        text.text=$"Level {PlayerService.CurrentLevel+1}";
        AudioService.PlayMusic(AudioName.BG_Level);
    }

    private void OnPlayLevel(OnPlayLevel e)
    {
        //UpdateMoveCount();
    }
    public override void Subscribe()
    {
        base.Subscribe();
        Event<OnPlayLevel>.Subscribe(OnPlayLevel);
    }
    public override void Unsubscribe()
    {
        base.Unsubscribe();
        Event<OnPlayLevel>.Unsubscribe(OnPlayLevel);
    }
    public override void OnDirectorComplete()
    {
        bg.gameObject.SetActive(false);
        centerMask.gameObject.SetActive(false);
    }
}
