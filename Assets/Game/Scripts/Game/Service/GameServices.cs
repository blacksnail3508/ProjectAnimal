using LazyFramework;
using System.Collections.Generic;
using UnityEngine;

public static class GameServices
{
    #region controller functions
    public static void UpBtn()
    {
        Event<OnUserMove>.Post(new OnUserMove(MoveDirection.Up));
    }
    public static void DownBtn()
    {
        Event<OnUserMove>.Post(new OnUserMove(MoveDirection.Down));
    }
    public static void LeftBtn()
    {
        Event<OnUserMove>.Post(new OnUserMove(MoveDirection.Left));
    }
    public static void RightBtn()
    {
        Event<OnUserMove>.Post(new OnUserMove(MoveDirection.Right));
    }
    public static void UndoBtn()
    {
        Event<OnUndo>.Post(new OnUndo());
    }
    #endregion
    #region game event
    public static void ArcherShoot()
    {
        Event<OnArcherShoot>.Post(new OnArcherShoot());
    }
    public static void ArcherShootEnd()
    {
        Event<OnArcherShootEnd>.Post(new OnArcherShootEnd());
    }
    public static void OnEnemyShoot()
    {
        Event<OnEnemyShoot>.Post(new OnEnemyShoot());

    }
    public static void OnEnemyShootEnd()
    {
        Event<OnEnemyShootEnd>.Post(new OnEnemyShootEnd());

    }
    public static void OnArcherDie()
    {
        Event<OnArcherDie>.Post(new OnArcherDie());
    }
    public static void OnEnemyDie()
    {
        Event<OnEnemyDie>.Post(new OnEnemyDie());
    }
    public static void OnPlayerTurn()
    {
        Event<OnPlayerTurn>.Post(new OnPlayerTurn());
    }
    public static void OnLose()
    {
        Event<OnLose>.Post(new OnLose());
        AdsService.ShowInter("lose");
    }
    public static void OnWin()
    {
        Event<OnWin>.Post(new OnWin());
        AdsService.ShowInter("win");
    }
    #endregion
    /// <summary>
    /// Clear game play
    /// </summary>
    public static void EndLevel()
    {
        Event<OnEndLevel>.Post(new OnEndLevel());
    }
#if UNITY_EDITOR
    public static void OnAllSolutionGenerated()
    {
        Event<OnAllSolutionGenerated>.Post(new OnAllSolutionGenerated());
    }
#endif
    public static GameState gameState;
    public static void ChangeGameState(GameState _gameState)
    {
        gameState=_gameState;
    }
    #region Gameplay infomations
    static Vector2 currentLevelSize;
    static float width => currentLevelSize.x;

    static float height => currentLevelSize.y;
    static List<BoardObject> AllBoardObject;

    public static void SaveCurrentLevelSize(float width , float height)
    {
        currentLevelSize=new Vector2(width , height);
    }
    public static void SetCameraZoom(Camera camera)
    {
        float mapSize = Mathf.Max(width , height);
        float screenResolution = (float)Screen.width/(float)Screen.height;

        float deltaSize = MapLerp(screenResolution ,0.41f ,0.75f, 0f, 2f);
        camera.orthographicSize = mapSize + deltaSize;

    }
    public static void SaveLevelObjects(List<BoardObject> boardObjects)
    {
        AllBoardObject=boardObjects;
    }
    /// <summary>
    /// Get the object that occupied board position
    /// </summary>
    /// <returns></returns>
    private static List<BoardObject> result = new List<BoardObject>();
    public static List<BoardObject> GetObjectsAtPosition(int x , int y)
    {
        result.Clear();
        //check bound
        if (x<0||y<0) return null;
        if (x>width-1) return null;
        if (y>height-1) return null;

        //check obj
        foreach (var obj in AllBoardObject)
        {
            if (obj.GetBoardPosition().x == x && obj.GetBoardPosition().y == y)
            {
                if (obj.gameObject.activeSelf == false)
                {
                }
                else
                {
                    result.Add(obj);
                }
            }
        }
        return result;
    }
    public static bool IsPositionMoveable(int x , int y)
    {
        //check bound
        if (x<0||y<0) return false;
        if (x>width-1) return false;
        if (y>height-1) return false;

        //check all obj on board
        foreach (var obj in AllBoardObject)
        {
            if (obj.GetBoardPosition().x == x && obj.GetBoardPosition().y == y)
            {
                if(obj.IsBlocked == true)
                {
                    return !obj.gameObject.activeSelf;
                }
                else
                {
                    return true;
                }
            }
        }
        return true;
    }
    public static bool IsBulletOnBoard(int x , int y)
    {
        if (x<0||y<0) return false;
        if (x>width) return false;
        if (y>height) return false;

        return true;
    }
    public static Vector2 BoardPositionToLocalPosition(int valueX , int valueY)
    {
        return new Vector2(valueX+0.5f-width/2f , valueY+0.5f-height/2f);
    }
    public static Vector2 TransformPositionToBoardPosition(Vector2 position)
    {
        int boardX = 0;
        int boardy = 0;

        boardX=(int)(position.x+(float)width/2f);
        boardy=(int)(position.y+(float)height/2f);

        return new Vector2(boardX , boardy);
    }

    public static int PlayerMove;
    #endregion
    public static float MapLerp(float value , float from , float to , float outMin , float outMax)
    {
        // Clamp the input value to the specified range
        value = Mathf.Clamp(value , from , to);

        // Map the clamped value to the [outMin, outMax] range using lerp
        return Mathf.Lerp(outMax , outMin , Mathf.InverseLerp(from , to , value));
    }
}
