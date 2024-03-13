using LazyFramework;
using TMPro;
using UnityEngine;

public class UIGamePlay : UIMenuBase
{
    [SerializeField] TMP_Text moveCountTxt;
    [SerializeField] TMP_Text text;
    protected override void OnEnable()
    {
        base.OnEnable();
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
}
