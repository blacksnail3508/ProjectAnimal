using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Tile prefabs")]
    [SerializeField] CageTile tilePrefab;
    [SerializeField] Transform tileRoot;

    [Header("Gate prefabs")]
    [SerializeField] CageGate gatePrefab;
    [SerializeField] Transform gateRoot;

    private int sizeX;
    private int sizeY;

    private List<CageTile> tilePool = new List<CageTile>();
    private List<CageGate> gatePool = new List<CageGate>();

    //private void Start()
    //{
    //    Create(7 , 8);
    //}

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

        SpawnGates();
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

    private void SpawnGates()
    {
        int x = sizeX; int y = sizeY;

        //spawn gates horizontal
        for (int i = 0; i<x; i++)
        {
            //spawn bottom gate
            var newGate1 = GetGate();
            newGate1.transform.localPosition=new Vector2(-(float)x/2f+0.5f+i , -(float)y/2f);
            newGate1.Init(GateDirection.Bottom);

            //spawn top gate
            var newGate2 = GetGate();
            newGate2.transform.localPosition=new Vector2(-(float)x/2f+0.5f+i , (float)y/2f);
            newGate2.Init(GateDirection.Top);
        }

        //spawn gates vertical
        for (int i = 0; i<y; i++)
        {
            //spawn left gates
            var newGate1 = GetGate();
            newGate1.transform.localPosition=new Vector2(-(float)x/2f, -(float)y/2f+0.5f+i);
            newGate1.Init(GateDirection.Left);

            //spawn right gates
            var newGate2 = GetGate();
            newGate2.transform.localPosition=new Vector2((float)x/2f , -(float)y/2f+0.5f+i);
            newGate2.Init(GateDirection.Right);
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
    private CageGate GetGate()
    {
        foreach (var gate in gatePool)
        {
            if (gate.gameObject.activeSelf==false)
            {
                gate.gameObject.SetActive(true);
                return gate;
            }
        }

        var newGate = Instantiate(gatePrefab , gateRoot);
        gatePool.Add(newGate);

        return newGate;
    }
}
