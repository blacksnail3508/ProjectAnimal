using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Animal
{
    [SerializeField] GameConfig gameConfig;
    [SerializeField] BoxCollider2D box;
    [SerializeField] SpriteRenderer spriteRenderer;
    /// <summary>
    /// set up direction, animal size, start position
    /// </summary>
    /// <param name="direction"> both facing direction and move direction</param>
    /// <param name="bodyLength"> the length that animal occupied on cage</param>
    /// <param name="x">board position x</param>
    /// <param name="y">board position y</param>
    public void Init(FaceDirection direction,int bodyLength, int x, int y)
    {
        this.direction = direction;
        this.bodyLength = bodyLength;



        switch (direction)
        {
            case FaceDirection.Up:
                box.size=new Vector2(1 , bodyLength);
                box.offset = new Vector2(0 , - bodyLength/2f + 0.5f);
                spriteRenderer.transform.rotation=Quaternion.Euler(0 , 0 , 180);

                break;
            case FaceDirection.Down:
                box.size=new Vector2(1 , bodyLength);
                box.offset=new Vector2(0 , bodyLength/2f-0.5f);
                spriteRenderer.transform.rotation=Quaternion.Euler(0 , 0 , 180);
                break;
            case FaceDirection.Left:
                box.size=new Vector2(bodyLength , 1);
                box.offset=new Vector2(bodyLength/2f-0.5f , 0);
                spriteRenderer.transform.rotation=Quaternion.Euler(0 , 0 , -90);
                break;
            case FaceDirection.Right:
                box.size=new Vector2(bodyLength , 1);
                box.offset=new Vector2(-bodyLength/2f+0.5f , 0);
                spriteRenderer.transform.rotation=Quaternion.Euler(0 , 0 , 90);
                break;
        }

        spriteRenderer.transform.localPosition = box.offset;

        transform.localPosition = GameServices.BoardPositionToLocalPosition(x, y);
        UpdatePositionOnBoard(x , y);
    }
    public override void Move()
    {
        switch(direction)
        {
            case FaceDirection.Up:
                var isPositionMoveable = GameServices.IsPositionMoveable(posX , posY+1);

                if (isPositionMoveable == true)
                {
                    MoveUp();
                }
                break;
        }
    }
    private void UpdatePositionOnBoard(int x, int y)
    {
        this.posX = x;
        this.posY = y;
    }
    private void MoveUp()
    {
        var moveTime = 1f/gameConfig.AnimalConfig.tilePerSec;
        transform.DOMoveY(transform.position.y+1 , moveTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            UpdatePositionOnBoard(posX , posY+1);
            Move();
        });

    }
    private void MoveDown()
    {

    }
    private void MoveLeft()
    {

    }
    private void MoveRight()
    {

    }
}
