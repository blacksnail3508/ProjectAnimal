using LazyFramework;
using TMPro;
using UnityEngine;

public class UIGamePlay : UIMenuBase
{
    [SerializeField] TMP_Text moveCountTxt;
    [SerializeField] TMP_Text text;
    bool isPlayerTurn = true;
    protected override void OnEnable()
    {
        base.OnEnable();
        text.text=$"Level {PlayerService.CurrentLevel+1}";
        AudioService.PlayMusic(AudioName.BG_Level);
    }
    private void UpdateMoveCount()
    {
        moveCountTxt.text = GameServices.PlayerMove.ToString();
    }

    private void OnUndo(OnUndo e)
    {
        GameServices.PlayerMove--;
        GameServices.PlayerMove = Mathf.Max(GameServices.PlayerMove , 0);
        UpdateMoveCount() ;
    }
    private void OnPlayLevel(OnPlayLevel e)
    {
        GameServices.PlayerMove = 0;
        isPlayerTurn = true;
        UpdateMoveCount();
    }
    public override void Subscribe()
    {
        base.Subscribe();
        Event<OnUndo>.Subscribe(OnUndo);
        Event<OnPlayLevel>.Subscribe(OnPlayLevel);
    }
    public override void Unsubscribe()
    {
        base.Unsubscribe();
        Event<OnUndo>.Unsubscribe(OnUndo);
        Event<OnPlayLevel>.Unsubscribe(OnPlayLevel);
    }
}
