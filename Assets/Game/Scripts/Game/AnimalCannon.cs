using LazyFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCannon : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] GameConfig gameConfig;
    [Header("References")]
    [SerializeField] Transform horizontalRoot;
    [SerializeField] Transform verticalRoot;

    [SerializeField] GameObject horizontalGate;
    [SerializeField] GameObject verticalGate;

    [SerializeField] BoxCollider2D box;
    [SerializeField] SpriteRenderer spriteRenderer;
    [Header("Parameters")]
    [SerializeField] FaceDirection direction;
    [SerializeField] List<Animal> loadedAnimals = new List<Animal>();

    public FaceDirection Direction { get => direction; private set { } }
    public int posX;
    public int posY;

    private void Awake()
    {
        Close();
    }
    public void Open()
    {
        Bug.Log("Opened");
        box.enabled=true;
        horizontalGate.gameObject.SetActive(false);
        verticalGate.gameObject.SetActive(false);
    }

    public void Close()
    {
        Bug.Log("Closed");
        box.enabled=false;
        horizontalGate.gameObject.SetActive(true);
        verticalGate.gameObject.SetActive(true);
    }

    /// <summary>
    /// set up direction, open/close state
    /// </summary>
    /// <param name="direction"> both facing direction and move direction</param>
    /// <param name="bodyLength"> the length that animal occupied on cage</param>
    /// <param name="x">board position x</param>
    /// <param name="y">board position y</param>
    public void Init(FaceDirection direction, float x , float y)
    {
        this.direction=direction;
        //active gate
        switch (direction)
        {
            case FaceDirection.Left:
            case FaceDirection.Right:
                horizontalRoot.gameObject.SetActive(false);
                verticalRoot.gameObject.SetActive(true);
                break;
            case FaceDirection.Up:
            case FaceDirection.Down:
                horizontalRoot.gameObject.SetActive(true);
                verticalRoot.gameObject.SetActive(false);
                break;
        }

        //setup box and sprite
        switch (direction)
        {
            case FaceDirection.Up:
                box.size = new Vector2(1 , 2);
                box.offset = new Vector2(0 , -1f);
                spriteRenderer.transform.rotation=Quaternion.Euler(0 , 0 , 180);
                transform.localPosition=GameServices.BoardPositionToLocalPosition(x + 0.5f , y);
                break;
            case FaceDirection.Down:
                box.size = new Vector2(1 , 2);
                box.offset = new Vector2(0 , 1f);
                spriteRenderer.transform.rotation=Quaternion.Euler(0 , 0 , 180);
                transform.localPosition = GameServices.BoardPositionToLocalPosition(x + 0.5f , y);
                break;
            case FaceDirection.Left:
                box.size=new Vector2(2 , 1);
                box.offset=new Vector2(1f , 0);
                spriteRenderer.transform.rotation=Quaternion.Euler(0 , 0 , -90);
                transform.localPosition=GameServices.BoardPositionToLocalPosition(x , y + 0.5f);
                break;
            case FaceDirection.Right:
                box.size=new Vector2(2 , 1);
                box.offset=new Vector2(-1f , 0);
                spriteRenderer.transform.rotation=Quaternion.Euler(0 , 0 , 90);
                transform.localPosition=GameServices.BoardPositionToLocalPosition(x , y + 0.5f);
                break;
        }

        spriteRenderer.transform.localPosition = box.offset;
    }
    public void SetPosition(int x, int y)
    {
        posX = x; posY = y;
    }
    public void LoadAnimal(int count)
    {
        //create and sort animal in clip
        ReleaseLoadedAnimals();
        for (int i = 0; i<count; i++)
        {
            var animal = GameServices.AnimalPool.GetAnimal();
            animal.gameObject.SetActive(true);
            loadedAnimals.Add(animal);
            //sort
            animal.ChangeRotation(this.direction);

            var _direction = GameServices.DirectionToVector(direction);

            animal.SetPosition(posX - (int)_direction.x*2*i , posY - (int)_direction.y*2*i);
        }
        Open();

        GameServices.OnCannonLoaded(count);
    }
    public void ReturnPool()
    {
        gameObject.SetActive(false);
        ReleaseLoadedAnimals();
    }

    public void Shoot()
    {
        //get 1st queue animal
        var animal = loadedAnimals[0];

        //remove from list
        loadedAnimals.Remove(animal);

        box.enabled = false;

        //create callback
        Action onStop = () =>
        {
            box.enabled = true;

            //sort if queue is remaining
            if (loadedAnimals.Count>0)
            {
                //sort
                SortAnimal();
            }
            else //if no animal remaining, check if all animal is get inside cage
            {
                Close();
            }
            GameServices.OnCannonShot();
            PlayerService.ResetDirection();
        };

        //move
        animal.Move(onStop);
    }

    private void SortAnimal()
    {
        var _direction = GameServices.DirectionToVector(direction);
        for (int i = 0;i <loadedAnimals.Count; i++)
        {
            loadedAnimals[i].SetPosition(posX - (int)_direction.x*2*i , posY - (int)_direction.y*2*i);
        }
    }
    private void ReleaseLoadedAnimals()
    {
        loadedAnimals.Clear();
    }
}