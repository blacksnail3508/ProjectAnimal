using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Animal : BoardObject
{
    [SerializeField] GameConfig config;
    [SerializeField] Transform sprite;
    public int bodyLength;
    public FaceDirection direction;
    [SerializeField] Ease moveEase;
    [SerializeField] List<TilePosition> path = new List<TilePosition>();
    private void Start()
    {
        GameServices.Add(this);
    }
    public void Move(Action OnStop = null)
    {
        //parse enum direction to vector
        var moveDirection = GameServices.DirectionToVector(direction);

        //check if next position is moveable
        int targetX = positionX+(int)moveDirection.x;
        int targetY = positionY+(int)moveDirection.y;

        path.Add(new TilePosition(targetX , targetY));

        while (GameServices.IsPositionMoveable(targetX , targetY)==true)
        {
            targetX+=(int)moveDirection.x;
            targetY+=(int)moveDirection.y;
            path.Add(new TilePosition(targetX , targetY));
        }

        //remove position that animal will stop on
        if (path.Count>=1) path.RemoveAt(path.Count-1); //remove the last checking point(it result is not moveable)
        if (path.Count>=1) path.RemoveAt(path.Count-1); //remove 1 position of head
        if (path.Count>=1) path.RemoveAt(path.Count-1); //remove 1 position of tail

        if (path.Count>0) GameServices.BlockPath(path);

        targetX-=(int)moveDirection.x;
        targetY-=(int)moveDirection.y;
        targetX-=positionX;
        targetY-=positionY;

        //Bug.Log($"path length = {path.Count}");
        //for (int i = 0; i < path.Count; i++)
        //{
        //    Bug.Log($"{path[i].x},{path[i].y}");
        //}

        moveDirection=new Vector3(targetX , targetY);
        //update this position param
        UpdatePositionOnBoard(targetX+positionX , targetY+positionY);

        transform.DOMove(transform.position+moveDirection , config.AnimalConfig.animationTime).SetEase(moveEase).OnComplete(() =>
        {
            GameServices.ReleasePath(path);
            OnStop?.Invoke();
        });
    }

    //calculate it position and bodyLength
    public override bool IsOccupied(int x , int y)
    {
        //this head position
        if (x==positionX&&y==positionY) return true;

        //tail position
        switch (direction)
        {
            //facing right, that tail is one step on left
            case FaceDirection.Right:
                if (x==positionX-1&&y==positionY) return true;
                break;

            //facing left, tail is on the right
            case FaceDirection.Left:
                if (x==positionX+1&&y==positionY) return true;
                break;

            //facing up, tail is downward
            case FaceDirection.Up:
                if (x==positionX&&y==positionY-1) return true;
                break;

            //facing down, tail is upward
            case FaceDirection.Down:
                if (x==positionX&&y==positionY+1) return true;
                break;
        }

        return false;
    }


    public int ChangeRotation(FaceDirection direction)
    {
        this.direction=direction;
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
        bool isHeadSafe = GameServices.IsPositionOnBoard(positionX , positionY);
        bool isTailSafe = false;
        switch (direction)
        {
            case FaceDirection.Left:
                isTailSafe=GameServices.IsPositionOnBoard(positionX+1 , positionY);
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

        //Bug.Log($"{gameObject.name} head safe = {isHeadSafe}, tail safe = {isTailSafe}");
        if (isHeadSafe==true&&isTailSafe==true) return true;
        return false;
    }
    public Vector2 TailPosition()
    {
        //tail position
        switch (direction)
        {
            //facing right, that tail is one step on left
            case FaceDirection.Right:
                return new Vector2(positionX-1, positionY);
            //facing left, tail is on the right
            case FaceDirection.Left:
                return new Vector2(positionX+1 , positionY);
            //facing up, tail is downward
            case FaceDirection.Up:
                return new Vector2(positionX , positionY-1);

            //facing down, tail is upward
            case FaceDirection.Down:
                return new Vector2(positionX , positionY+1);
        }
        return Vector2.zero;
    }
}
