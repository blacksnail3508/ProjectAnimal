#if UNITY_EDITOR
using LazyFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GamePlay gamePlay;
    [SerializeField] LevelMap levelMap;
    [SerializeField] UIMenuTest controller;
    [SerializeField] LevelAsset levelAsset;
    [SerializeField] SolutionGenerator solutionGenerator;
    [Header("integer")]
    [SerializeField] List<int> solution = new List<int>();
    [SerializeField] int currentStep = 0;
    [SerializeField] int currentLevel = 0;
    [Header("Solution str")]
    /// <summary>
    /// this is output solution, all step that checked is stored here
    /// </summary>
    [SerializeField] string solutionStr = "";
    List<Enemy> listEnemyLast;
    List<Archer> listArcherLast;
    List<Enemy> listEnemyCurrent;
    List<Archer> listArcherCurrent;
    int currentStepNumber = 0;

    private void Awake()
    {
        Subscribe();
        Time.timeScale=10f;
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }

    private async void OnAllSolutionGenerated(OnAllSolutionGenerated e)
    {
        GameServices.ChangeGameState(GameState.Playing);
        //play 1st game
        Restart();

        PlayerService.PlayLevel(0);
        GetPositionData();
        CachePositionData();
        solution=ReadFirstSolutionFromGenerator();

        await Task.Delay(1000);
        MakeNextMove();
        //wait for event
    }

    private void OnPlayerTurn(OnPlayerTurn e)
    {
        if (GameServices.gameState == GameState.Idle)
        {
            return;
        }
        Bug.Log("Player turn");
        //get archer and enemy info
        GetPositionData();

        //check win/lose
        if(listArcherCurrent.Count == 0)
        {
            //lose
            SaveLoseSolution();
            CleanSolution();
            StartNewSolutionOrNewLevel();
            return;
        }
        if(listEnemyCurrent.Count == 0)
        {
            //win
            SaveWinSolution();
            CleanSolution();
            StartNewSolutionOrNewLevel();
            return;
        }

        //if it made a stupid move (or die)
        if (IsStupidMove())
        {
            Bug.Log("Stupid move");
            GameServices.ChangeGameState(GameState.Idle);

            SaveLoseSolution();
            CleanSolution();

            StartNewSolutionOrNewLevel();
            return;
        }

        //if not stupid enought to end, continue to play
        if (IsHasNextStep())
        {
            //save current archer and enemy position
            CachePositionData();
            MakeNextMove();
        }
        else
        {
            //solution end
            GameServices.ChangeGameState(GameState.Idle);

            SaveLoseSolution();
            CleanSolution();
            StartNewSolutionOrNewLevel();
            return;
        }
    }

    private  void OnWin(OnWin e)
    {
        SaveWinSolution();
        //Invoke("StartNewSolutionOrNewLevel" , 0.5f);
        StartNewSolutionOrNewLevel();
    }
    private  void OnLose(OnLose e)
    {
        SaveLoseSolution();
        //Invoke("StartNewSolutionOrNewLevel" , 0.5f);
        StartNewSolutionOrNewLevel();
    }
    private void CleanSolution()
    {
        solutionGenerator.CleanSolution();
    }
    private void StartNewSolutionOrNewLevel()
    {

        //still have solution
        if(solutionGenerator.ListSolutions.Count != 0)
        {
            Bug.Log("Start New Solution");
            Restart();
            PlayerService.ReplayLevel();

            solution = ReadFirstSolutionFromGenerator();
            if(solution==null)
            {
                //all solution checked

                //load new level
                solutionGenerator.Regenerate();
                //Invoke("PlayNextLevel" , 1);

                PlayNextLevel();
                return;
            }
            MakeNextMove();
            //wait for event
        }
        else
        {
            Bug.Log("All solution checked, start new level");
            //all solution checked
            levelAsset.listLevel[currentLevel].solution = GetBestSolution();
            levelAsset.listLevel[currentLevel].dificulty=levelAsset.listLevel[currentLevel].solution.Length;

            //load new level
            solutionGenerator.Regenerate();

            var listBullet = FindObjectsByType<Bullet>(0);
            foreach(Bullet bullet in listBullet)
            {
                Destroy(bullet.gameObject);
            }

            PlayNextLevel();
        }
    }

    private string GetBestSolution()
    {
        var minLength = 100;
        string bestSolution = "";
        foreach (var solution in solutionGenerator.ListWinSolution)
        {
            if(solution.Length < minLength)
            {
                bestSolution = solution;
                minLength = solution.Length;
            }
        }

        return bestSolution;
    }

    private void Subscribe()
    {
        Event<OnAllSolutionGenerated>.Subscribe(OnAllSolutionGenerated);
        Event<OnPlayerTurn>.Subscribe(OnPlayerTurn);
        //Event<OnWin>.Subscribe(OnWin);
        //Event<OnLose>.Subscribe(OnLose);
    }
    private void Unsubscribe()
    {
        Event<OnAllSolutionGenerated>.Unsubscribe(OnAllSolutionGenerated);
        Event<OnPlayerTurn>.Unsubscribe(OnPlayerTurn);
        //Event<OnWin>.Unsubscribe(OnWin);
        //Event<OnLose>.Unsubscribe(OnLose);
    }
    /// <summary>
    /// Clear solution and slution string, reset current step and step number,
    /// solution generator clean
    /// </summary>
    private void Restart()
    {
        if(solution !=null)
        {
            solution.Clear();
        }

        currentStep=0;
        currentStepNumber=0;
        solutionStr = string.Empty;

        solutionGenerator.CleanSolution();
    }
    #region DONE
    private void SaveStep()
    {
        solutionStr+=currentStep;
    }
    private void SaveWinSolution()
    {
        solutionGenerator.ListWinSolution.Add(solutionStr);
    }
    private void SaveLoseSolution()
    {
        solutionGenerator.ListDeadSolution.Add(solutionStr);
    }
    List<int> ConvertStringToIntList(string inputString)
    {
        return inputString.Select(c => int.Parse(c.ToString())).ToList();
    }
    private List<int> ReadFirstSolutionFromGenerator()
    {
        if(solutionGenerator.ListSolutions.Count > 0)
        {
            return ConvertStringToIntList(solutionGenerator.ListSolutions[0]);
        }
        else
        {
            return null;
        }

    }
    private async void PlayNextLevel()
    {
        await Task.Delay(1500);
        currentLevel++;
        PlayerService.PlayLevel(currentLevel);
        GetPositionData();
        CachePositionData();
        Restart();
        ReadSolution();

        MakeNextMove();
    }

    private void ReadSolution()
    {
        solution=ReadFirstSolutionFromGenerator();
    }
    private bool IsStupidMove()
    {
        bool isEnemyKill = false;
        //check if player kill any enemy
        if (listEnemyLast.Count != listEnemyCurrent.Count)
        {
            isEnemyKill = true;
        }

        //check if any archer die
        bool isArcherDie = false;
        if (listArcherLast.Count!=listArcherCurrent.Count)
        {
            isArcherDie = true;
        }

        //check if any archer move
        bool isArcherMove  = false;
        for (int i = 0; i<listArcherLast.Count; i++)
        {
            //check for all archer that make a move or not
            if (listArcherLast[i].GetBoardPosition() != listArcherCurrent[i].GetBoardPosition())
            {
                isArcherMove = true;
            }
        }

        return isArcherDie || !isArcherMove && !isEnemyKill;
    }
    private void CachePositionData()
    {
        listArcherLast=new List<Archer>(listArcherCurrent);
        listEnemyLast=new List<Enemy>(listEnemyCurrent);
    }
    private void GetPositionData()
    {
        listArcherCurrent=new List<Archer>(gamePlay.GetListArcher());
        listEnemyCurrent=new List<Enemy>(gamePlay.GetListEnemy());
    }
    private void MakeNextMove()
    {
        currentStep = solution[currentStepNumber];

        switch (currentStep)
        {
            case 1:
                Bug.Log("Move up");
                gamePlay.Up();
                break;
            case 2:
                Bug.Log("Move Down");
                gamePlay.Down();
                break;
            case 3:
                Bug.Log("Move Left");
                gamePlay.Left();
                break;
            case 4:
                Bug.Log("Move Right");
                gamePlay.Right();
                break;
        }
        SaveStep();
        currentStepNumber++;
    }
    private bool IsHasNextStep()
    {
        if (currentStepNumber<solution.Count)
        {
            return true;
        }

        return false;
    }
    #endregion
}

#endif