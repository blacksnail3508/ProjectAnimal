using LazyFramework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSave
{
    public int skateEquiping;
    public List<int> skateList;

    public GameSave()
    {
        skateEquiping=0;
        skateList=new List<int>();
    }
}
public static class DataService
{
    private static GameSave gameSave;
    public static void Save(GameSave data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(KeyString.GameSave , json);
    }

    public static void Run()
    {
        if (PlayerPrefs.HasKey(KeyString.GameSave)==false)
        {
            //init starting items
            gameSave = new GameSave();
            gameSave.skateEquiping = 0;
            gameSave.skateList = new List<int> { 0 };
            Save(gameSave);
        }
        else
        {
            string json = PlayerPrefs.GetString(KeyString.GameSave);
            gameSave = JsonUtility.FromJson<GameSave>(json);
        }
        DisplaySave();
    }

    public static void UnlockSkate(int index)
    {
        //Bug.Log($"Unlock skate {index}");
        if(gameSave.skateList.Contains(index)) { return; }

        gameSave.skateList.Add(index);
        Save(gameSave);
    }
    public static void EquipSkate(int index)
    {
        if (gameSave.skateList.Contains(index))
        {
            gameSave.skateEquiping=index;
            Save(gameSave);

            Event<OnSkateUse>.Post(new OnSkateUse());
        }
    }
    public static bool IsSkateEquiping(int index)
    {
        return gameSave.skateEquiping == index;
    }

    public static bool IsSkateUnlock(int index)
    {
        return gameSave.skateList.Contains(index);
    }
    private static void DisplaySave()
    {
        Bug.Log("Load data success");
        Bug.Log($"item equiping = {gameSave.skateEquiping}");
        Bug.Log($"item list count = {gameSave.skateList.Count}");
    }
    public static int GetEquipingSkateBoard()
    {
        return gameSave.skateEquiping;
    }
}
