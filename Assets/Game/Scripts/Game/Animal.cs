using DG.Tweening;
using LazyFramework;
using System;
using UnityEngine;

public class Animal : BoardObject
{
    [SerializeField] Transform sprite;
    public int bodyLength;
    public FaceDirection direction;
    private Action onStop;
    [SerializeField] Ease moveEase;
    private void Start()
    {
        GameServices.Add(this);
    }
    public void Move(Action OnStop = null)
    {
        //cache callback
        if(OnStop!= null) onStop = OnStop;

        //parse enum direction to vector
        var moveDirection = GameServices.DirectionToVector(direction);

        //check if next position is moveable
        int nextX = positionX + (int)moveDirection.x;
        int nextY = positionY + (int)moveDirection.y;

        if (GameServices.IsPositionMoveable(nextX , nextY) == true)
        {
            //move tranform
            transform.DOMove(transform.position + moveDirection , 0.25f).SetEase(moveEase).OnComplete(() =>
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
            onStop?.Invoke();
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


    public int ChangeRotation(FaceDirection direction)
    {
        this.direction = direction;
        switch (direction)
        {
            case FaceDirection.Left:
                sprite.rotation=Quaternion.Euler(new Vector3(0 , 0 , -90));
                return 90;
            case FaceDirection.Right:
                sprite.rotation=Quaternion.Euler(new Vector3(0 , 0 , 90));
                return -90;
            case FaceDirection.Up:
                sprite.rotation=Quaternion.Euler(new Vector3(0 , 0 , -180));
                return 0;
            case FaceDirection.Down:
                sprite.rotation=Quaternion.Euler(new Vector3(0 , 0 , 0));
                return -180;
        }
        return 0;
    }
    private void UpdatePositionOnBoard(int x , int y)
    {
        this.positionX=x;
        this.positionY=y;
    }
    public void ReturnPool()
    {
        this.gameObject.SetActive(false);
    }

    public override bool IsSafe()
    {
        bool isHeadSafe = GameServices.IsPositionOnBoard(positionX, positionY);
        bool isTailSafe = false;
        switch (direction)
        {
            case FaceDirection.Left:
                isTailSafe =GameServices.IsPositionOnBoard(positionX+1, positionY);
                break;
            case FaceDirection.Right:
                isTailSafe=GameServices.IsPositionOnBoard(positionX-1 , positionY);
                break;
            case FaceDirection.Up:
                isTailSafe=GameServices.IsPositionOnBoard(positionX , positionY-1);
                break;
            case FaceDirection.Down:
                isTailSafe=GameServices.IsPositionOnBoard(positionX , positionY+1);
                break;
        }

        Bug.Log($"{gameObject.name} head safe = {isHeadSafe}, tail safe = {isTailSafe}");
        if (isHeadSafe == true && isTailSafe==true) return true;
        return false;
    }
}
