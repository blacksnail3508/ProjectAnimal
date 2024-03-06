using LazyFramework;
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
    public static void EndLevel()
    {
        Event<OnEndLevel>.Post(new OnEndLevel());
    }
    #region Gameplay infomations
    static Vector2 currentLevelSize;
    static float width => currentLevelSize.x;

    static float height => currentLevelSize.y;
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

    public static Vector2 BoardPositionToLocalPosition(int valueX , int valueY)
    {
        return new Vector2(valueX+0.5f-width/2f , valueY+0.5f-height/2f);
    }
    public static Vector2 TransformPositionToBoardPosition(Vector2 position)
    {
        int boardX = 0;
        int boardY = 0;

        boardX=(int)(position.x+(float)width/2f);
        boardY=(int)(position.y+(float)height/2f);

        return new Vector2(boardX , boardY);
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
