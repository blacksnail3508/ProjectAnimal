using LazyFramework;
using System.Collections.Generic;
using UnityEngine;

public static class GameServices
{
    #region controller functions
    public static void UndoBtn()
    {
        Event<OnUndo>.Post(new OnUndo());
    }
    #endregion

    #region user event
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
    #endregion
    #region Gameplay infomations

    static Vector2 currentLevelSize;
    static float width => currentLevelSize.x;
    static float height => currentLevelSize.y;

    static List<BoardObject> AllBoardObject;
    public static void SaveLevelObjects(List<BoardObject> boardObjects)
    {
        AllBoardObject=boardObjects;
    }
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

    public static Vector2 BoardPositionToLocalPosition(float valueX , float valueY)
    {
        return new Vector2(valueX+0.5f-width/2f , valueY+0.5f-height/2f);
    }
    public static Vector2 TransformPositionToBoardPosition(Vector2 position)
    {
        return new Vector2((int)(position.x+(float)width/2f) , (int)(position.y+(float)height/2f));
    }
    public static bool IsPositionMoveable(int x, int y)
    {
        //if that pos is outside of board
        if (x<0||y<0||x>=width||y>=height)
        {
            Bug.Log("This position is outside of cage!");
            return false;
        }

        //if any animal stand on that
        foreach (var animal in AllBoardObject)
        {
            if (animal.gameObject.activeSelf == true)
            {
                if (animal.IsOccupied(x , y) == true)
                {
                    Bug.Log("position occupied, not moveable");

                    //position is occupied, moveable = false
                    return false;
                }
            }
        }
        Bug.Log("moveable");
        return true;
    }
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
            if (obj.GetBoardPosition().x==x&&obj.GetBoardPosition().y==y)
            {
                if (obj.gameObject.activeSelf==false)
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

    public static int PlayerMove;
    #endregion
    #region math ectension
    public static float MapLerp(float value , float from , float to , float outMin , float outMax)
    {
        // Clamp the input value to the specified range
        value = Mathf.Clamp(value , from , to);

        // Map the clamped value to the [outMin, outMax] range using lerp
        return Mathf.Lerp(outMax , outMin , Mathf.InverseLerp(from , to , value));
    }
    #endregion
}
