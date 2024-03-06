#if UNITY_EDITOR
using LazyFramework;
using TMPro;
using UnityEditor;
using UnityEngine;

public class UIMenuTest : UIMenuBase
{
    [SerializeField] string path;
    [SerializeField] LevelMap levelMap;
    [SerializeField] LevelAsset tempAsset;
    [SerializeField] LevelAsset trueAsset;
    [SerializeField] TMP_InputField levelInput;
    [SerializeField] int currentLevel;
    [SerializeField] TMP_Text currentLevelTxt;
    private void Start()
    {
        UpdateUI();
    }
    public void LoadLevel()
    {
        currentLevel=int.Parse(levelInput.text);
        levelMap.LoadLevel(currentLevel);
    }
    private void LoadLevel(int level)
    {
        Bug.Log($"load level {level}");
        levelMap.LoadLevel(level);
        currentLevel=level;
        UpdateUI();
    }
    public void PreviousLevel()
    {
        LoadLevel((tempAsset.listLevel.Count+currentLevel-1)%tempAsset.listLevel.Count);
        UpdateUI();
    }
    public void NextLevel()
    {
        LoadLevel((tempAsset.listLevel.Count+currentLevel+1)%tempAsset.listLevel.Count);
        UpdateUI();
    }
    public void Save()
    {
        Level newLevel = ScriptableObject.CreateInstance<Level>();
        var old = tempAsset.listLevel[currentLevel];
        newLevel.width=old.width;
        newLevel.height=old.height;
        newLevel.listObject=old.listObject;
        newLevel.numberOfEnemy=old.numberOfEnemy;
        newLevel.numberOfHole=old.numberOfHole;
        newLevel.numberOfWall=old.numberOfWall;
        // Create the asset file
        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{path}/Level{trueAsset.listLevel.Count}.asset");
        AssetDatabase.CreateAsset(newLevel , assetPath);
        AssetDatabase.SaveAssets();
        trueAsset.listLevel.Add(newLevel);

        //clear old data
        tempAsset.listLevel.RemoveAt(currentLevel);
    }
    private void UpdateUI()
    {
        currentLevelTxt.text=$"Current Level = {tempAsset.listLevel[currentLevel].name} at index {currentLevel}";
    }
}
#endif