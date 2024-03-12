using LazyFramework;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Tile prefabs")]
    [SerializeField] CageTile tilePrefab;
    [SerializeField] Transform tileRoot;

    [Header("Cannon prefabs")]
    [SerializeField] AnimalCannon cannonPrefab;
    [SerializeField] Transform cannonRoot;

    [Header("Obstacles")]
    [SerializeField] ObstacleSpawner objSpawner;

    private int sizeX;
    private int sizeY;

    private List<CageTile> tilePool = new List<CageTile>();
    private List<AnimalCannon> cannonPool = new List<AnimalCannon>();

    public void Create(int x , int y)
    {
        //return pool all
        foreach (var tile in tilePool)
        {
            tile.ReturnPool();
        }
        foreach (var cannon in cannonPool)
        {
            cannon.ReturnPool();
        }
        GameServices.AnimalPool.ReleaseAll();


        sizeX = x;
        sizeY = y;

        spriteRenderer.size = new Vector2(x , y);

        SpawnTiles();

        SpawnCannons();
    }

    private void SpawnTiles()
    {
        int x = sizeX; int y = sizeY;
        //spawn tiles
        for (int j = 0; j<y; j++)
        {
            for (int i = 0; i<x; i++)
            {
                var newTile = GetTile();
                float posX = -(float)x/2f+0.5f+i;
                float posY = -(float)y/2f+0.5f+j;
                newTile.transform.localPosition=new Vector2(posX , posY);
            }
        }
    }

    private void SpawnCannons()
    {
        int x = sizeX; int y = sizeY;

        //spawn gates horizontal
        for (int i = 0; i<x; i++)
        {
            //spawn bottom gate
            var newCannon1 = GetCannon();
            newCannon1.transform.localPosition = new Vector2(-(float)x/2f+0.5f+i , -(float)y/2f);
            newCannon1.Init(FaceDirection.Up,i -0.5f , -0.5f);
            newCannon1.SetPosition(i,-1);

            //spawn top gate
            var newCannon2 = GetCannon();
            newCannon2.transform.localPosition=new Vector2(-(float)x/2f+0.5f+i , (float)y/2f);
            newCannon2.Init(FaceDirection.Down , i -0.5f , y-0.5f);
            newCannon2.SetPosition(i,y);
        }

        //spawn gates vertical
        for (int i = 0; i<y; i++)
        {
            //spawn left gates
            var newCannon1 = GetCannon();
            newCannon1.transform.localPosition=new Vector2(-(float)x/2f, -(float)y/2f+0.5f+i);
            newCannon1.Init(FaceDirection.Right, 0-0.5f , i-0.5f);
            newCannon1.SetPosition(-1,i);

            //spawn right gates
            var newCannon2 = GetCannon();
            newCannon2.transform.localPosition=new Vector2((float)x/2f , -(float)y/2f+0.5f+i);
            newCannon2.Init(FaceDirection.Left,x-0.5f , i-0.5f);
            newCannon2.SetPosition(x , i);
        }
    }

    /// <summary>
    /// load animal to cannon at position
    /// </summary>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    /// <param name="count"></param>
    public void SetCannon(int posX, int posY, int count)
    {
        var cannon = GetCannon(posX , posY);
        cannon?.LoadAnimal(count);
    }

    /// <summary>
    /// create or reuse tiles
    /// </summary>
    /// <returns></returns>
    private CageTile GetTile()
    {
        foreach(var tile in tilePool)
        {
            if(tile.gameObject.activeSelf ==false)
            {
                tile.gameObject.SetActive(true);
                return tile;
            }
        }

        var newTile = Instantiate(tilePrefab , tileRoot);
        tilePool.Add(newTile);

        return newTile;
    }

    /// <summary>
    /// create or reuse gates
    /// </summary>
    /// <returns></returns>
    private AnimalCannon GetCannon()
    {
        foreach (var cannon in cannonPool)
        {
            if (cannon.gameObject.activeSelf==false)
            {
                cannon.gameObject.SetActive(true);
                cannon.Close();
                return cannon;
            }
        }

        var newCannon = Instantiate(cannonPrefab , cannonRoot);
        cannonPool.Add(newCannon);
        newCannon.Close();
        return newCannon;
    }

    private AnimalCannon GetCannon(int x, int y)
    {
        foreach(var cannon in cannonPool)
        {
            if(cannon.posX == x && cannon.posY == y) return cannon;
        }

        Bug.LogError($"Cannon {x}:{y} not found");
        return null;
    }

    public void CreateObstacles(int level)
    {
        objSpawner.CreateObject(level);
    }
    public void HideObstacles()
    {
        objSpawner.ReturnPoolAll();
    }
}
