using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History
{

}
public struct ArcherData
{
    public bool isDead;
    public int positionX;
    public int positionY;
    public MoveDirection FaceDirection;
    public Vector2 GetBoardPosition()
    {
        return new Vector2(positionX , positionY);
    }
}
public struct EnemyData
{
    public bool isDead;
    public int positionX;
    public int positionY;
}
public struct TrapData
{
    public bool isActive;
}
