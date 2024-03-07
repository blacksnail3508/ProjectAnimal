using UnityEngine;
using UnityEngine.UIElements;

public class BoardObject : MonoBehaviour
{
    [Header("Board unit informations")]
    public int positionX;
    public int positionY;
    public bool IsBlocked;
    public ObjectType objectType;

    public void SetPosition(int positionX , int positionY)
    {
        this.positionX=positionX;
        this.positionY=positionY;

        //calculate position base on posX, posY and size of game
        var localPosition = GameServices.BoardPositionToLocalPosition(this.positionX , this.positionY);
        transform.localPosition = localPosition;
    }
    public Vector2 GetBoardPosition()
    {
        return new Vector2(positionX , positionY);
    }
    public virtual bool IsOccupied(int x, int y)
    {
        if(x == positionX && y == positionY) return true;

        return false;
    }
    public virtual bool IsSafe()
    {
        return true;
    }
}
