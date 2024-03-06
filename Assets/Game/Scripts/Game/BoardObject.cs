using UnityEngine;
using UnityEngine.UIElements;

public class BoardObject : MonoBehaviour
{
    [Header("Board unit informations")]
    public int positionX;
    public int positionY;
    public bool IsBlocked;
    public ObjectType objectType;

    public void SetPosition(Vector2 position)
    {
        positionX=(int)position.x;
        positionY=(int)position.y;

        var localPosition = GameServices.BoardPositionToLocalPosition((int)position.x , (int)position.y);
        transform.localPosition = localPosition;
    }

    public void SetPosition(int positionX , int positionY)
    {
        this.positionX=positionX;
        this.positionY=positionY;

        var localPosition = GameServices.BoardPositionToLocalPosition(this.positionX , this.positionY);
        transform.localPosition=localPosition;
    }
    public Vector2 GetBoardPosition()
    {
        return new Vector2(positionX , positionY);
    }
}
