using LazyFramework;
using System.Collections.Generic;
using UnityEngine;

public class LevelMap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LevelAsset levelAsset;
    [SerializeField] Board board;
    [SerializeField] Transform objectRoot;
    [SerializeField] GameObject trash;
    [Header("Prefabs")]
    [SerializeField] Archer archerPrefab;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] BounceWall wallPrefab;
    [SerializeField] Wall holePrefab;
    [SerializeField] Box boxPrefab;
    [SerializeField] Turret turretPrefab;
    [SerializeField] Shield ShieldPrefab;
    [SerializeField] Teleport TeleportPrefab;
    [SerializeField] IceSurface IceSurfacePrefab;
    [SerializeField] Split SplitPrefab;
    [SerializeField] SpikeTrap SpikeTrapPrefab;
    [Header("Lists")]
    public List<Enemy> listEnemies;
    public List<Archer> listArchers;
    public List<TrapBase> listTraps;
    public List<BoardObject> AllBoardObject = new List<BoardObject>();
    [Header("Game")]
    [SerializeField] int level;
    [SerializeField] int currentLevelNumber;

    public void Reload()
    {
        LoadLevel(currentLevelNumber);
    }
    /// <summary>
    /// Load new level, clear map, cache level
    /// </summary>
    /// <param name="level"></param>
    public void LoadLevel(int level)
    {
        currentLevelNumber=level;
        ClearBoard();
        CreateBoard(levelAsset.listLevel[level]);
    }

    private void ClearBoard()
    {
        GameObject trashTemp = Instantiate(trash , this.transform);
        foreach (var obj in AllBoardObject)
        {
            obj.transform.parent=trashTemp.transform;
        }
        listArchers.Clear();
        listEnemies.Clear();
        listTraps.Clear();
        AllBoardObject.Clear();
        board.Clear();
        Destroy(trashTemp);
    }

    //read data and create board object
    private void CreateBoard(Level boardData)
    {
        board.SetupBoard(boardData.width , boardData.height);

        foreach (var slotData in boardData.listObject)
        {
            switch (slotData.objectType)
            {
                case ObjectType.None:
                    break;
                case ObjectType.Archer:
                    var archer = Instantiate(archerPrefab , objectRoot);
                    archer.SetPosition(slotData.valueX , slotData.valueY);
                    listArchers.Add(archer);
                    AllBoardObject.Add(archer);
                    break;
                case ObjectType.EnemyUp:
                    var enemyUp = Instantiate(enemyPrefab , objectRoot);
                    enemyUp.SetDirection(MoveDirection.Up);
                    enemyUp.SetPosition(slotData.valueX , slotData.valueY);
                    listEnemies.Add(enemyUp);
                    AllBoardObject.Add(enemyUp);
                    break;
                case ObjectType.EnemyLeft:
                    var enemyLeft = Instantiate(enemyPrefab , objectRoot);
                    enemyLeft.SetDirection(MoveDirection.Left);
                    enemyLeft.SetPosition(slotData.valueX , slotData.valueY);
                    listEnemies.Add(enemyLeft);
                    AllBoardObject.Add(enemyLeft);
                    break;
                case ObjectType.EnemyRight:
                    var enemyRight = Instantiate(enemyPrefab , objectRoot);
                    enemyRight.SetDirection(MoveDirection.Right);
                    enemyRight.SetPosition(slotData.valueX , slotData.valueY);
                    listEnemies.Add(enemyRight);
                    AllBoardObject.Add(enemyRight);
                    break;
                case ObjectType.EnemyDown:
                    var enemyDown = Instantiate(enemyPrefab , objectRoot);
                    enemyDown.SetDirection(MoveDirection.Down);
                    enemyDown.SetPosition(slotData.valueX , slotData.valueY);
                    listEnemies.Add(enemyDown);
                    AllBoardObject.Add(enemyDown);
                    break;
                case ObjectType.BounceWallAcute:
                    var wallAcute = Instantiate(wallPrefab , objectRoot);
                    wallAcute.SetData(ObjectType.BounceWallAcute);
                    wallAcute.SetPosition(slotData.valueX , slotData.valueY);
                    AllBoardObject.Add(wallAcute);
                    break;
                case ObjectType.BounceWallGrave:
                    var wallGrave = Instantiate(wallPrefab , objectRoot);
                    wallGrave.SetData(ObjectType.BounceWallGrave);
                    wallGrave.SetPosition(slotData.valueX , slotData.valueY);
                    AllBoardObject.Add(wallGrave);
                    break;
                case ObjectType.Wall:
                    var hole = Instantiate(holePrefab , objectRoot);
                    hole.SetPosition(slotData.valueX , slotData.valueY);
                    AllBoardObject.Add(hole);
                    break;
                case ObjectType.Box:
                    var box = Instantiate(boxPrefab , objectRoot);
                    box.SetPosition(slotData.valueX , slotData.valueY);
                    AllBoardObject.Add(box);
                    break;
                case ObjectType.TurretUp:
                    var TurretUp = Instantiate(turretPrefab , objectRoot);
                    TurretUp.SetDirection(MoveDirection.Up);
                    TurretUp.SetPosition(slotData.valueX , slotData.valueY);
                    listEnemies.Add(TurretUp);
                    AllBoardObject.Add(TurretUp);
                    break;
                case ObjectType.TurretLeft:
                    var TurretLeft = Instantiate(turretPrefab , objectRoot);
                    TurretLeft.SetDirection(MoveDirection.Left);
                    TurretLeft.SetPosition(slotData.valueX , slotData.valueY);
                    listEnemies.Add(TurretLeft);
                    AllBoardObject.Add(TurretLeft);
                    break;
                case ObjectType.TurretRight:
                    var TurretRight = Instantiate(turretPrefab , objectRoot);
                    TurretRight.SetDirection(MoveDirection.Right);
                    TurretRight.SetPosition(slotData.valueX , slotData.valueY);
                    listEnemies.Add(TurretRight);
                    AllBoardObject.Add(TurretRight);
                    break;
                case ObjectType.TurretDown:
                    var TurretDown = Instantiate(turretPrefab , objectRoot);
                    TurretDown.SetDirection(MoveDirection.Down);
                    TurretDown.SetPosition(slotData.valueX , slotData.valueY);
                    listEnemies.Add(TurretDown);
                    AllBoardObject.Add(TurretDown);
                    break;
                case ObjectType.Shield:
                    var Shield = Instantiate(ShieldPrefab , objectRoot);
                    Shield.SetPosition(slotData.valueX , slotData.valueY);
                    AllBoardObject.Add(Shield);
                    break;
                case ObjectType.Teleport:
                    var Teleport = Instantiate(TeleportPrefab , objectRoot);
                    Teleport.SetPosition(slotData.valueX , slotData.valueY);
                    Teleport.SetTargetPosition(slotData.TargetPosition);
                    AllBoardObject.Add(Teleport);
                    break;
                case ObjectType.IceSurface:
                    var IceSurface = Instantiate(IceSurfacePrefab , objectRoot);
                    IceSurface.SetPosition(slotData.valueX , slotData.valueY);
                    AllBoardObject.Add(IceSurface);
                    break;
                case ObjectType.Split:
                    var Split = Instantiate(SplitPrefab , objectRoot);
                    Split.SetPosition(slotData.valueX , slotData.valueY);
                    AllBoardObject.Add(Split);
                    break;
                case ObjectType.SpikeTrap:
                    var SpikeTrap = Instantiate(SpikeTrapPrefab , objectRoot);
                    SpikeTrap.SetPosition(slotData.valueX , slotData.valueY);

                    if (slotData.isOn)
                    {
                        SpikeTrap.SpikeOn();
                    }
                    else
                    {
                        SpikeTrap.SpikeOff();
                    }

                    AllBoardObject.Add(SpikeTrap);
                    listTraps.Add(SpikeTrap);
                    break;
            }
        }

        //save to service
        GameServices.SaveLevelObjects(AllBoardObject);
    }
    public void SaveObjectPosition()
    {
        GameServices.SaveLevelObjects(AllBoardObject);
    }
}
