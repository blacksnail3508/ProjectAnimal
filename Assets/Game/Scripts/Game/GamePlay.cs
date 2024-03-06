using LazyFramework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class GameData<T>
{
    public List<EnemyData> ListEnemyData;
    public List<ArcherData> ListArcherData;
    public List<TrapData> listTrapData;
}
public class GamePlay : MonoBehaviour
{
    [Header("Level data asset")]
    [SerializeField] LevelAsset levelAsset;
    [Header("List units")]
    [SerializeField] List<Archer> listArcher;
    [SerializeField] List<Enemy> listEnemies;
    [SerializeField] List<TrapBase> listTraps;
    [Header("Number of units in action")]
    [SerializeField] float archerShooting = 0;
    [SerializeField] float enemyShooting = 0;
    [SerializeField] bool isControllerAvailable = true;
    [Header("References")]
    [SerializeField] LevelMap levelMap;

    private Stack<GameData<string>> history = new Stack<GameData<string>>();
    private void Awake()
    {
        Subscribe();
        levelMap.gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }

    #region Get unit data
    public List<Enemy> GetListEnemy()
    {
        return listEnemies;
    }
    public List<Archer> GetListArcher()
    {
        return listArcher;
    }
    #endregion
    #region UNDO FUNCTION
    private void SaveGameStep()
    {
        var step = new GameData<string>();

        step.ListArcherData = new List<ArcherData>();
        step.ListEnemyData = new List<EnemyData>();
        step.listTrapData = new List<TrapData>();

        //clone data trap
        for(int i = 0; i < listTraps.Count; i++)
        {
            var origin = listTraps[i];
            var clone = new TrapData();

            clone.isActive = origin.isActive;

            step.listTrapData.Add(clone);
        }

        //clone data archer
        for (int i = 0; i<listArcher.Count; i++)
        {
            var origin = listArcher[i];
            var clone = new ArcherData();

            clone.isDead = origin.isDead;
            clone.positionX = origin.positionX;
            clone.positionY = origin.positionY;
            clone.FaceDirection = origin.FaceDirection;

            step.ListArcherData.Add(clone);
        }
        //clone data enemy
        for (int i = 0; i<listEnemies.Count; i++)
        {
            var origin = listEnemies[i];
            var clone = new EnemyData();

            clone.isDead=origin.isDead;
            clone.positionX=origin.positionX;
            clone.positionY=origin.positionY;

            step.ListEnemyData.Add(clone);
        }

        history.Push(step);
    }

    private void Undo()
    {
        //remove last step
        if (history.Count>1)
        {
            history.Pop();
        }
        else
        {
            return;
        }

        //load last step (after remove, it is previous step)
        var previousStep = history.Peek();
        //load trap data
        for(int i = 0; i<listTraps.Count; i++)
        {
            listTraps[i].isActive = previousStep.listTrapData[i].isActive;
            listTraps[i].UpdateState();

        }

        //load enemy data
        for (int i = 0; i<listEnemies.Count; i++)
        {
            if (previousStep.ListEnemyData[i].isDead)
            {
                listEnemies[i].Deactive();
            }
            else
            {
                listEnemies[i].Reactive();
            }

        }
        //load archer data
        for (int i = 0; i<listArcher.Count; i++)
        {
            var data = previousStep.ListArcherData[i];
            if (data.isDead)
            {
                listArcher[i].Deactive();
            }
            else
            {
                listArcher[i].Reactive();
            }

            var position = data.GetBoardPosition();
            listArcher[i].SetPosition(position);

            listArcher[i].ChangeRotation(data.FaceDirection);
        }
    }
    #endregion
    #region Gameplay And User Command Event
    private void DisableController()
    {
        isControllerAvailable=false;
    }
    private void EnableController()
    {
        isControllerAvailable=true;
    }
    private void OnArcherShoot(OnArcherShoot e)
    {
        archerShooting++;
    }
    private void OnArcherShootEnd(OnArcherShootEnd e)
    {
        archerShooting--;
        if (archerShooting<=0)
        {
            archerShooting=0;
            //enemies attack
            EnemiesAttack();
        }
    }
    private void OnEnemyShoot(OnEnemyShoot e)
    {
        enemyShooting++;
    }
    private void OnEnemyShootEnd(OnEnemyShootEnd e)
    {
        enemyShooting--;
        if (enemyShooting<=0)
        {
            enemyShooting=0;
            //end turn
            EnableController();

            //post event
            GameServices.OnPlayerTurn();

            SaveGameStep();
            levelMap.SaveObjectPosition();
        }
    }
    private void EnemiesAttack()
    {
        foreach (var enemy in listEnemies)
        {
            enemy.Fire();
        }
    }
    private void OnEnemyDie(OnEnemyDie e)
    {
        //count enemies left
        var enemyCount = 0;
        foreach (var enemy in listEnemies)
        {
            enemyCount+=enemy.gameObject.activeSelf ? 1 : 0;
        }

        if (enemyCount==0)
        {
            //post event
            Bug.Log("Game Win");
            isControllerAvailable=true;
            GameServices.OnWin();
            PlayerService.Level = Mathf.Max(PlayerService.CurrentLevel + 1 , PlayerService.Level);
            Invoke("ShowWin" , 0.5f);
        }
    }
    private void ShowWin()
    {
        Vibration.Vibrate();
        DisplayService.ShowPopup(UIPopupName.PopupWin);

        if(PlayerService.Level >=9)
        {
            if (PlayerService.IsRated==1) return;
            else
            {
                DisplayService.ShowPopup(UIPopupName.PopupRate);
            }
        }
    }
    private void OnEndLevel(OnEndLevel e)
    {
        levelMap.gameObject.SetActive(false);
    }
    private void OnArcherDie(OnArcherDie e)
    {
        //count enemies left
        var archerCount = 0;
        foreach (var archer in listArcher)
        {
            archerCount+=archer.gameObject.activeSelf ? 1 : 0;
        }

        if (archerCount==0)
        {
            Vibration.Vibrate();
            GameServices.ChangeGameState(GameState.Idle);
            isControllerAvailable=true;
            //post event
            GameServices.OnLose();

            Invoke("ShowLose" , 0.5f);
        }
    }
    private void ShowLose()
    {
        DisplayService.ShowPopup(UIPopupName.PopupLose);
    }
    private void OnUndo(OnUndo e)
    {
        Undo();
    }
    private void OnPlayLevel(OnPlayLevel e)
    {
        //create new board
        levelMap.gameObject.SetActive(true);
        levelMap.LoadLevel(e.Level);

        //cache new unit data
        listArcher = levelMap.listArchers;
        listEnemies = levelMap.listEnemies;
        listTraps = levelMap.listTraps;
        //clear event
        archerShooting =0;
        enemyShooting=0;

        //enable controller
        isControllerAvailable=true;

        //clear game history
        history.Clear();

        //save to undo
        SaveGameStep();

        //scale camera
        GameServices.SetCameraZoom(Camera.main);
    }
    private void OnUserMove(OnUserMove e)
    {
        switch (e.direction)
        {
            case MoveDirection.Up:
                Up();
                break;
            case MoveDirection.Down:
                Down();
                break;
            case MoveDirection.Left:
                Left();
                break;
            case MoveDirection.Right:
                Right();
                break;
        }
    }
    #endregion
    #region Archer Move Function
    public async void OnUserMove(MoveDirection inputDirection)
    {
        //start turn
        DisableController();

        //all trap activate
        foreach (var trap in listTraps)
        {
            trap.Activate();
        }

        await Task.Delay(100);
        //Archer move
        foreach (var archer in listArcher)
        {
            archer.Move(inputDirection);
        }
        //Enemy action
    }
    public void Up()
    {
        if (isControllerAvailable==false) return;
        OnUserMove(MoveDirection.Up);
    }
    public void Down()
    {
        if (isControllerAvailable==false) return;
        OnUserMove(MoveDirection.Down);
    }
    public void Right()
    {
        if (isControllerAvailable==false) return;
        OnUserMove(MoveDirection.Right);
    }
    public void Left()
    {
        if (isControllerAvailable==false) return;
        OnUserMove(MoveDirection.Left);
    }
    #endregion
    #region subcribe events
    private void Subscribe()
    {
        //GAME PLAY EVENT
        Event<OnArcherShoot>.Subscribe(OnArcherShoot);
        Event<OnArcherShootEnd>.Subscribe(OnArcherShootEnd);
        Event<OnEnemyShoot>.Subscribe(OnEnemyShoot);
        Event<OnEnemyShootEnd>.Subscribe(OnEnemyShootEnd);
        Event<OnArcherDie>.Subscribe(OnArcherDie);
        Event<OnEnemyDie>.Subscribe(OnEnemyDie);
        //user Command event
        Event<OnPlayLevel>.Subscribe(OnPlayLevel);
        Event<OnUserMove>.Subscribe(OnUserMove);
        Event<OnEndLevel>.Subscribe(OnEndLevel);
        Event<OnUndo>.Subscribe(OnUndo);
    }
    private void Unsubscribe()
    {
        //gameplay event
        Event<OnArcherShoot>.Unsubscribe(OnArcherShoot);
        Event<OnArcherShootEnd>.Unsubscribe(OnArcherShootEnd);
        Event<OnEnemyShoot>.Unsubscribe(OnEnemyShoot);
        Event<OnEnemyShootEnd>.Unsubscribe(OnEnemyShootEnd);
        Event<OnArcherDie>.Unsubscribe(OnArcherDie);
        Event<OnEnemyDie>.Unsubscribe(OnEnemyDie);

        //user Command event
        Event<OnPlayLevel>.Unsubscribe(OnPlayLevel);
        Event<OnUserMove>.Unsubscribe(OnUserMove);
        Event<OnEndLevel>.Unsubscribe(OnEndLevel);
        Event<OnUndo>.Unsubscribe(OnUndo);
    }
    #endregion
}
