using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    public int posX;
    public int posY;
    public AnimalType AnimalType;
    public int bodyLength;
    public FaceDirection direction;

    public abstract void Move();

    public bool IsOccupied(int x , int y)
    {
        //calculate size, direction and head position
        if (direction==FaceDirection.Up)
        {
            if (posX-x == 0 &&
                posY-y <= bodyLength-1 && posY-y>=0)
            {
                return true;
            }
        }

        if (direction == FaceDirection.Right)
        {
            if (posX-x >= 0 && posX-x <= bodyLength-1&&
                posY-y == 0)
            {
                return true;
            }
        }

        if (direction == FaceDirection.Down)
        {
            if (posX-x == 0 &&
                posY-y <= bodyLength-1 && posY-y >= 0)
            {
                return true;
            }
        }


        return false;
    }
}
