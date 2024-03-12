using AdsAnalytics;
using LazyFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

    [Header("Sorting group")]
    [SerializeField] SortingGroup columnUp;
    [SerializeField] SortingGroup columnDown;
    [SerializeField] SortingGroup columnLeft;
    [SerializeField] SortingGroup columnRight;

    [SerializeField] SortingGroup gateHorizontalSort;
    [SerializeField] SortingGroup gateVerticalSort;

    Vector3 moveDirection { get { return GameServices.DirectionToVector(direction); } }

    public FaceDirection Direction { get => direction; private set { } }
    public int posX;
    public int posY;

    public int shoot = 0;

    private void Awake()
    {
        Close();
    }
    public void Open()
    {
        //Bug.Log("Opened");
        box.enabled=true;
        horizontalGate.gameObject.SetActive(false);
        verticalGate.gameObject.SetActive(false);
    }

    public void Close()
    {
        //Bug.Log("Closed");
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
    public void Init(FaceDirection direction , float x , float y)
    {
        //sorting if horizontal
        columnLeft.sortingOrder=-(int)(y+1)*3;
        columnRight.sortingOrder=-(int)(y+1)*3;
        gateHorizontalSort.sortingOrder=-(int)(y+1)*3-1;

        //sorting vertical
        columnUp.sortingOrder=-(int)(y+1)*3;
        gateVerticalSort.sortingOrder=-(int)(y+1)*3+1;
        columnDown.sortingOrder=-(int)(y+1)*3+2;

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
                box.size=new Vector2(1 , 2);
                box.offset=new Vector2(0 , -1f);
                spriteRenderer.transform.rotation=Quaternion.Euler(0 , 0 , 180);
                transform.localPosition=GameServices.BoardPositionToLocalPosition(x+0.5f , y);
                break;
            case FaceDirection.Down:
                box.size=new Vector2(1 , 2);
                box.offset=new Vector2(0 , 1f);
                spriteRenderer.transform.rotation=Quaternion.Euler(0 , 0 , 180);
                transform.localPosition=GameServices.BoardPositionToLocalPosition(x+0.5f , y);
                break;
            case FaceDirection.Left:
                box.size=new Vector2(2 , 1);
                box.offset=new Vector2(1f , 0);
                spriteRenderer.transform.rotation=Quaternion.Euler(0 , 0 , -90);
                transform.localPosition=GameServices.BoardPositionToLocalPosition(x , y+0.5f);
                break;
            case FaceDirection.Right:
                box.size=new Vector2(2 , 1);
                box.offset=new Vector2(-1f , 0);
                spriteRenderer.transform.rotation=Quaternion.Euler(0 , 0 , 90);
                transform.localPosition=GameServices.BoardPositionToLocalPosition(x , y+0.5f);
                break;
        }

        spriteRenderer.transform.localPosition=box.offset;
    }
    public void SetPosition(int x , int y)
    {
        posX=x; posY=y;
    }
    public void LoadAnimal(int count)
    {
        //reset shoot count
        shoot=0;
        //create and sort animal in clip
        ReleaseLoadedAnimals();

        for (int i = 0; i<count; i++)
        {
            var animal = GameServices.AnimalPool.GetAnimal();
            animal.gameObject.SetActive(true);
            animal.PlayIdle();
            loadedAnimals.Add(animal);
            //sort
            animal.ChangeRotation(this.direction);

            var _direction = GameServices.DirectionToVector(direction);

            animal.SetPosition(posX-(int)_direction.x*2*i , posY-(int)_direction.y*2*i);
        }
        Open();

        //subscribe to GameService
        GameServices.AddCannon(this);
    }
    public void ReturnPool()
    {
        gameObject.SetActive(false);
        ReleaseLoadedAnimals();
    }

    public void Shoot()
    {
        //check for blocked path
        var path = new List<TilePosition>();
        int targetX = posX+(int)moveDirection.x;
        int targetY = posY+(int)moveDirection.y;

        path.Add(new TilePosition(targetX , targetY));

        while (GameServices.IsPositionMoveable(targetX , targetY)==true)
        {
            targetX+=(int)moveDirection.x;
            targetY+=(int)moveDirection.y;
            path.Add(new TilePosition(targetX , targetY));
        }

        if (path.Count>0)
        {
            path.RemoveAt(path.Count-1);

            if (GameServices.IsBlocked(path)==true)
            {
                Bug.Log("Path is blocked");
                return;
            }
        }

        //if path not blocked (animal can be shoot), write to history
        GameServices.WriteHistory(this);

        //get 1st queue animal
        var animal = loadedAnimals[shoot];
        shoot++;
        box.enabled=false;

        //create callback
        Action onStop = () =>
        {
            Bug.Log("Callback stop");

            //check if it realy get inside
            if(animal.IsSafe() == true)
            {
                ////remove from list
                //loadedAnimals.Remove(animal);

                box.enabled=true;

                //sort if queue is remaining
                if (loadedAnimals.Count > shoot)
                {
                    //sort
                    SortAnimal();
                }
                else //if no animal remaining, check if all animal is get inside cage
                {
                    Close();
                    GameServices.OnCannonShot();
                }
                //GameServices.OnCannonShot();
            }
            else
            {
                //for (int i = shoot-1; i<loadedAnimals.Count; i++)
                //{
                //    GameServices.OnCannonShot();
                //}
                GameServices.OnCannonShot();
                box.enabled=false;
            }
        };

        //move
        animal.Move(onStop);
    }

    private void SortAnimal()
    {
        var _direction = GameServices.DirectionToVector(direction);
        for (int i = shoot; i < loadedAnimals.Count; i++)
        {
            int positionInCannon = i-shoot;
            loadedAnimals[i].SetPosition(posX-(int)_direction.x*2*positionInCannon , posY-(int)_direction.y*2*positionInCannon);
        }
    }
    private void ReleaseLoadedAnimals()
    {
        loadedAnimals.Clear();
    }

    public void Undo()
    {
        //recall last shoot animal, then sort
        shoot--;
        SortAnimal();
        Open();

        //reallow click
        box.enabled=true;

        //var isFreshed = (loadedAnimals.Count - shoot)  > 0 ? true : false;
        //return isFreshed;
    }
    public bool IsEmpty()
    {
        return loadedAnimals.Count-shoot==0;
    }
}
