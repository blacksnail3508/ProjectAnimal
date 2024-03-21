using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimal
{
    public bool IsShoot();
    public bool IsCaptured();
    public bool IsSafe();
    public void Move();
    public void Playwin();
    public bool IsActive();
    public void SetPosition(float x, float y);
    public void ReturnPool();
    public void Uncaptured();
    public void Captured();
    public bool IsOccupied(int x , int y);
    public Vector2 GetPosition();

    public BoardObject GetBoardObject();
}
