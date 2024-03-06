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

    //call when all object complete action
    private void OnPlayerTurn(OnPlayerTurn e)
    {
        isPlayerTurn = true;
    }

    //call when receive user control
    private void OnUserMove(OnUserMove e)
    {
        if (isPlayerTurn == true)
        {
            GameServices.PlayerMove++;
            UpdateMoveCount();
            isPlayerTurn=false;

        }
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
        Event<OnUserMove>.Subscribe(OnUserMove);
        Event<OnUndo>.Subscribe(OnUndo);
        Event<OnPlayLevel>.Subscribe(OnPlayLevel);
        Event<OnPlayerTurn>.Subscribe(OnPlayerTurn);
    }
    public override void Unsubscribe()
    {
        base.Unsubscribe();
        Event<OnUserMove>.Unsubscribe(OnUserMove);
        Event<OnUndo>.Unsubscribe(OnUndo);
        Event<OnPlayLevel>.Unsubscribe(OnPlayLevel);
        Event<OnPlayerTurn>.Unsubscribe(OnPlayerTurn);
    }
}
