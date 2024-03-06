using LazyFramework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintButton : ButtonBase
{
    [SerializeField] LevelAsset levelAsset;
    [SerializeField] TMP_Text buttonText;
    List<int> listMove = new List<int>();
    MoveDirection nextStepDirection;
    int step = 0;
    bool isPlayerTurn = true;
    public override void OnClick()
    {
        base.OnClick();
        ShowThisLevelHintAndReplay();
    }

    private void ShowThisLevelHintAndReplay()
    {
        PlayerService.ReplayLevel();
        DisplayService.ShowMenu(UIMenuName.MenuGameplay);

        int currentLevel = PlayerService.CurrentLevel;
        string solution = levelAsset.listLevel[currentLevel].solution;

        //read solution to step
        listMove = StringUtils.StringToIntList(solution);
        ShowNextHint();

        //add listener
        Subscribe();
        Bug.Log("Start hint");
    }

    private void ShowNextHint()
    {
        button.interactable=false;
        if (step >= listMove.Count)
        {
            EndHint();
        }
        var move = listMove[step];

        switch (move)
        {
            case 8:
                nextStepDirection = MoveDirection.Up;
                buttonText.text="Up";
                break;
            case 2:
                nextStepDirection = MoveDirection.Down;
                buttonText.text="Down";
                break;
            case 4:
                nextStepDirection=MoveDirection.Left;
                buttonText.text="Left";
                break;
            case 6:
                nextStepDirection = MoveDirection.Right;
                buttonText.text="Right";
                break;
            default:
                buttonText.text="...";
                break;
        }
        step++;


    }

    private void OnUserMove(OnUserMove e)
    {
        if(isPlayerTurn==false)
        {
            return;
        }

        if(e.direction == nextStepDirection)
        {
            ShowNextHint();
            isPlayerTurn = false;
        }
        else
        {
            EndHint();
        }
    }
    private void EndHint()
    {
        Unsubscribe();
        step = 0;
        buttonText.text="Hint";
        button.interactable = true;
    }
    private void OnPlayerTurn(OnPlayerTurn e)
    {
        isPlayerTurn = true;
    }
    private void OnPlayLevel(OnPlayLevel e)
    {
        EndHint();
    }
    private void OnUndo(OnUndo e)
    {
        EndHint();
    }
    private void Subscribe()
    {
        Event<OnUserMove>.Subscribe(OnUserMove);
        Event<OnPlayerTurn>.Subscribe(OnPlayerTurn);
        Event<OnPlayLevel>.Subscribe(OnPlayLevel);
        Event<OnUndo>.Subscribe(OnUndo);
    }

    private void Unsubscribe()
    {
        Event<OnUserMove>.Unsubscribe(OnUserMove);
        Event<OnPlayerTurn>.Unsubscribe(OnPlayerTurn);
        Event<OnPlayLevel>.Unsubscribe(OnPlayLevel);
        Event<OnUndo>.Unsubscribe(OnUndo);
    }
}
