using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Tile prefabs")]
    [SerializeField] CageTile tilePrefab;
    [SerializeField] Transform tileRoot;

    [Header("Gate prefabs")]
    [SerializeField] AnimalCannon cannonPrefab;
    [SerializeField] Transform cannonRoot;

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
            var newCannon1 = GetGate();
            newCannon1.transform.localPosition = new Vector2(-(float)x/2f+0.5f+i , -(float)y/2f);
            newCannon1.Init(FaceDirection.Up,i - sizeX/2f-0.5f , - sizeY/2f-0.5f);
            newCannon1.SetPosition(i,-1);

            //spawn top gate
            var newCannon2 = GetGate();
            newCannon2.transform.localPosition=new Vector2(-(float)x/2f+0.5f+i , (float)y/2f);
            newCannon2.Init(FaceDirection.Down , i-sizeX/2f -0.5f , y-sizeY/2f-0.5f);
            newCannon2.SetPosition(i,y);
        }

        //spawn gates vertical
        for (int i = 0; i<y; i++)
        {
            //spawn left gates
            var newCannon1 = GetGate();
            newCannon1.transform.localPosition=new Vector2(-(float)x/2f, -(float)y/2f+0.5f+i);
            newCannon1.Init(FaceDirection.Right, 0-sizeX/2f-0.5f , i-sizeY/2f-0.5f);
            newCannon1.SetPosition(-1,i);

            //spawn right gates
            var newCannon2 = GetGate();
            newCannon2.transform.localPosition=new Vector2((float)x/2f , -(float)y/2f+0.5f+i);
            newCannon2.Init(FaceDirection.Left,x-sizeX/2f-0.5f , i-sizeY/2f-0.5f);
            newCannon1.SetPosition(x , i);
        }
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
    private AnimalCannon GetGate()
    {
        foreach (var gate in cannonPool)
        {
            if (gate.gameObject.activeSelf==false)
            {
                gate.gameObject.SetActive(true);
                return gate;
            }
        }

        var newGate = Instantiate(cannonPrefab , cannonRoot);
        cannonPool.Add(newGate);

        return newGate;
    }
}
