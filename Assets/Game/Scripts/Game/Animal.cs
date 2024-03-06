using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    public Vector2 position;
    public AnimalType AnimalType;
    public int bodyLenght;
    public FaceDirection direction;

    public abstract void Move();
    public abstract bool IsOccupied(int x,int y);

}
