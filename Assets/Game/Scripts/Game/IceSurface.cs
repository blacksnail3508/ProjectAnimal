using LazyFramework;

public class IceSurface : BoardObject
{
    bool isTurn = true;
    private void Start()
    {
        Subscribe();
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }
    public void MoveAhead(Archer archer)
    {
        if(isTurn)
        {
            archer.SetPosition(positionX , positionY);
            archer.Move(archer.FaceDirection);
            isTurn = false;
        }
    }
    public void OnPlayerTurn(OnPlayerTurn e)
    {
        isTurn = true;
    }
    private void Subscribe()
    {
        Event<OnPlayerTurn>.Subscribe(OnPlayerTurn);
    }
    private void Unsubscribe()
    {
        Event<OnPlayerTurn>.Unsubscribe(OnPlayerTurn);
    }
}
