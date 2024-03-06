using LazyFramework;
public class UndoButton : ButtonBase
{
    private bool isPlayerTurn;
    private void Awake()
    {
        Subscribe();
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }
    public override void OnClick()
    {
        base.OnClick();
        UndoBtn();
    }

    private void UndoBtn()
    {
        if (isPlayerTurn==false)
        {
            return;
        }
        GameServices.UndoBtn();
    }

    private void OnPlayerTurn(OnPlayerTurn e)
    {
        isPlayerTurn = true;
    }
    private void OnUserMove(OnUserMove e)
    {
        if (isPlayerTurn == false)
        {
            return;
        }
        isPlayerTurn = false;
    }
private void Subscribe()
    {
        Event<OnUserMove>.Subscribe(OnUserMove);
        Event<OnPlayerTurn>.Subscribe(OnPlayerTurn);
    }

    private void Unsubscribe()
    {
        Event<OnUserMove>.Unsubscribe(OnUserMove);
        Event<OnPlayerTurn>.Unsubscribe(OnPlayerTurn);
    }
}
