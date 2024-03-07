using DG.Tweening;
using UnityEngine;

public class Animal : BoardObject
{
    [SerializeField] Transform sprite;
    public int bodyLength;
    public FaceDirection direction;

    public void Move()
    {
        //parse enum direction to vector
        var moveDirection = DirectionToVector(direction);

        //check if next position is moveable
        int nextX = positionX + (int)moveDirection.x;
        int nextY = positionY + (int)moveDirection.y;
        if (GameServices.IsPositionMoveable(nextX , nextY) == true)
        {
            //move tranform
            transform.DOMove(transform.position+moveDirection , 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                //update this position param
                UpdatePositionOnBoard(nextX , nextY);

                //repeat move sequence
                Move();
            });
        }
        else
        {
            //Stop and check if it already get inside cage
        }
    }

    //calculate it position and bodyLength
    public override bool IsOccupied(int x , int y)
    {
        //this head position
        if(x == positionX && y == positionY) return true;

        //tail position
        switch (direction)
        {
            //facing right, that tail is one step on left
            case FaceDirection.Right:
                if ( x == positionX - 1 && y == positionY) return true;
                break;

            //facing left, tail is on the right
            case FaceDirection.Left:
                if ( x == positionX + 1 && y == positionY) return true;
                break;

            //facing up, tail is downward
            case FaceDirection.Up:
                if (x == positionX && y == positionY - 1) return true;
                break;

            //facing down, tail is upward
            case FaceDirection.Down:
                if (x == positionX && y == positionY + 1) return true;
                break;
        }

        return false;
    }

    private Vector3 DirectionToVector(FaceDirection direction)
    {
        switch (direction)
        {
            case FaceDirection.Up:
                return Vector2.up;
            case FaceDirection.Down:
                return Vector2.down;
            case FaceDirection.Left:
                return Vector2.left;
            case FaceDirection.Right:
                return Vector2.right;
            default: return Vector2.zero;
        }
    }
    public int ChangeRotation(FaceDirection direction)
    {
        switch (direction)
        {
            case FaceDirection.Left:
                sprite.rotation=Quaternion.Euler(new Vector3(0 , 0 , 90));
                return 90;
            case FaceDirection.Right:
                sprite.rotation=Quaternion.Euler(new Vector3(0 , 0 , -90));
                return -90;
            case FaceDirection.Up:
                sprite.rotation=Quaternion.Euler(new Vector3(0 , 0 , 0));
                return 0;
            case FaceDirection.Down:
                sprite.rotation=Quaternion.Euler(new Vector3(0 , 0 , -180));
                return -180;
        }
        return 0;
    }
    private void UpdatePositionOnBoard(int x , int y)
    {
        this.positionX=x;
        this.positionY=y;
    }
}
