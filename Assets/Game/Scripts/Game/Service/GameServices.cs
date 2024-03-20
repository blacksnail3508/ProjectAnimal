using DG.Tweening;
using LazyFramework;
using System;
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
    public static void OnLose()
    {
        Event<OnLose>.Post(new OnLose());
    }
    public static void OnWin()
    {
        Event<OnWin>.Post(new OnWin());
    }
    public static void EndLevel()
    {
        Event<OnEndLevel>.Post(new OnEndLevel());
    }

    public static void OnCannonLoaded(int count)
    {
        Event<OnCannonLoaded>.Post(new OnCannonLoaded(count));
    }

    public static void OnCannonShot()
    {
        Event<OnCannonShot>.Post(new OnCannonShot());
    }

    #endregion
    #region Gameplay infomations

    static Vector2 currentLevelSize;
    static float width => currentLevelSize.x;
    static float height => currentLevelSize.y;

    private static List<BoardObject> listAnimal = new List<BoardObject>();
    private static List<BoardObject> listObstacles = new List<BoardObject>();
    private static List<AnimalCannon> listCannon = new List<AnimalCannon>();

    public static AnimalPool AnimalPool;

    public static void ClearCannon()
    {
        listCannon.Clear();
    }
    public static void AddCannon(AnimalCannon cannon)
    {
        listCannon.Add(cannon);
    }
    public static bool IsAllCannonShot()
    {
        foreach(var cannon in listCannon)
        {
            if (cannon.IsEmpty() == false) return false;
        }

        return true;
    }

    public static void AnimalCelebrate()
    {
        foreach (var pig in listAnimal)
        {
            pig.Playwin();
        }
    }

    public static void ClearAnimal()
    {
        listAnimal.Clear();
    }

    public static bool IsAllAnimalSafe()
    {
        foreach (var animal in listAnimal)
        {
            if (animal.gameObject.activeSelf==true&&
                animal.IsSafe()==false)
            {
                return false;
            }
        }

        return true;
    }
    public static Animal UnsafeAnimals()
    {
        foreach (var animal in listAnimal)
        {
            if (animal.gameObject.activeSelf==true&&
                animal.IsSafe()==false)
            {
                return animal as Animal;
            }
        }
        return null;
    }

    public static void AddAnimal(BoardObject obj)
    {
        listAnimal.Add(obj);
    }
    public static void AddObstacle(BoardObject obj)
    {
        listObstacles.Add(obj);
    }
    public static void SaveCurrentLevelSize(float width , float height)
    {
        currentLevelSize=new Vector2(width , height);
    }
    public static void SetCameraZoom(Camera camera)
    {
        camera?.DOKill();
        float mapSize = Mathf.Max(width , height);
        float screenResolution = Screen.width/(float)Screen.height;

        float deltaSize = MapLerp(screenResolution , 0.41f , 0.75f , 0f , 2f);

        float finalSize = mapSize+deltaSize*2+4;

        float startSize = finalSize*1.5f;

        camera.orthographicSize = startSize;
        camera.DOOrthoSize(finalSize,1.25f).SetDelay(3f);
    }

    public static Vector2 BoardPositionToLocalPosition(float valueX , float valueY)
    {
        //Bug.Log($"Calculating width {width}, x = {valueX}, position x = {-width/2f+0.5f+valueX} ");
        return new Vector2(-width/2f+0.5f+valueX , -height/2f+0.5f+valueY);
    }
    public static Vector2 TransformPositionToBoardPosition(Vector2 position)
    {
        return new Vector2((int)(position.x+(float)width/2f) , (int)(position.y+(float)height/2f));
    }
    public static bool IsPositionMoveable(int x , int y)
    {
        //if that pos is outside of board
        if (x<0||y<0||x>=width||y>=height)
        {
            //Bug.Log($"Position {x}:{y} is outside of cage {width}x{height}!");
            return false;
        }

        //if any animal stand on that
        foreach (var animal in listAnimal)
        {
            if (animal.gameObject.activeSelf==true)
            {
                if (animal.IsOccupied(x , y)==true)
                {
                    //Bug.Log("position occupied, not moveable");

                    //position is occupied, moveable = false
                    return false;
                }
            }
        }

        //if any animal stand on that
        foreach (var obstacle in listObstacles)
        {
            if (obstacle.gameObject.activeSelf==true)
            {
                if (obstacle.IsOccupied(x , y)==true)
                {
                    //Bug.Log("position occupied, not moveable");
                    //position is occupied, moveable = false
                    return false;
                }
            }
        }

        //every thing checked, available to step on
        //Bug.Log($"Position {x}:{y} is moveable!");
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
        foreach (var obj in listAnimal)
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
    public static bool IsPositionOnBoard(int x , int y)
    {
        //if that pos is outside of board
        if (x<0||y<0||x>=width||y>=height)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static int PlayerMove;
    #endregion

    #region UNDO
    public static bool UndoEnable = true;
    static History history = new History();
    public static History History {  get { return history; } }

    public static void WriteHistory(AnimalCannon cannon)
    {
        history.Push(cannon);
    }

    public static void ClearHistory()
    {
        history.Clear();
    }

    #endregion

    #region math ectension
    public static float MapLerp(float value , float from , float to , float outMin , float outMax)
    {
        // Clamp the input value to the specified range
        value=Mathf.Clamp(value , from , to);

        // Map the clamped value to the [outMin, outMax] range using lerp
        return Mathf.Lerp(outMax , outMin , Mathf.InverseLerp(from , to , value));
    }
    public static Vector3 DirectionToVector(FaceDirection direction)
    {
        switch (direction)
        {
            case FaceDirection.Up:
                return Vector2.up;
            case FaceDirection.Down:
                return Vector2.down;
            case FaceDirection.Left:
                return Vector2.left;
            case FaceDirection.Right:
                return Vector2.right;
            default: return Vector2.zero;
        }
    }
    #endregion

    #region Move Improvement

    static List<TilePosition> blockedPath = new List<TilePosition>();

    public static void BlockPath(List<TilePosition> path)
    {
        blockedPath.Add(path);
    }
    public static void ReleasePath(List<TilePosition> path)
    {
        blockedPath.Remove(path);
    }
    public static bool IsBlocked(List<TilePosition> path)
    {
        return blockedPath.AnyMatch(path);
    }
    public static void ClearPath()
    {
        blockedPath.Clear();
    }

    #endregion

}
