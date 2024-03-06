#if UNITY_EDITOR // => Ignore from here to next endif if not in editor

using LazyFramework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GamePlay gameplay;
    [SerializeField] LevelAsset levelAsset;
    [SerializeField] string path = "Assets/Game/Scripts/ScriptableObject/";
    [Header("Number of map generate")]
    [SerializeField] int numberOfMap = 1;
    [Header("Config")]
    [SerializeField] bool isFullyRandom = false;

    [DrawIf("isFullyRandom" , false)][SerializeField] Vector2 levelWidth;
    [DrawIf("isFullyRandom" , false)][SerializeField] Vector2 levelHeight;
    [DrawIf("isFullyRandom" , false)][SerializeField] Vector2 numberOfEnemies;
    [DrawIf("isFullyRandom" , false)][SerializeField] Vector2 numberOfArcher;
    [DrawIf("isFullyRandom" , false)][SerializeField] Vector2 numberOfWall;
    [DrawIf("isFullyRandom" , false)][SerializeField] Vector2 numberOfHole;

    [DrawIf("isFullyRandom" , true)][SerializeField] int rateOfNone;
    [DrawIf("isFullyRandom" , true)][SerializeField] int rateOfArcher;
    [DrawIf("isFullyRandom" , true)][SerializeField] int rateOfEnemyUp;
    [DrawIf("isFullyRandom" , true)][SerializeField] int rateOfEnemyDown;
    [DrawIf("isFullyRandom" , true)][SerializeField] int rateOfEnemyLeft;
    [DrawIf("isFullyRandom" , true)][SerializeField] int rateOfEnemyRight;
    [DrawIf("isFullyRandom" , true)][SerializeField] int rateOfWallAcute;
    [DrawIf("isFullyRandom" , true)][SerializeField] int rateOfWallGrave;
    [DrawIf("isFullyRandom" , true)][SerializeField] int rateOfWallHole;


    #region Utils
    public void GenerateFullyRandom()
    {
        Level newLevel = ScriptableObject.CreateInstance<Level>();

        //random number
        newLevel.width=Random.Range((int)levelWidth.x , (int)levelWidth.y);
        //newBoard.height =Random.Range((int) levelHeight.x , (int)levelHeight.y);
        newLevel.height=newLevel.width;
        CreateDefaultListObject(newLevel);

        //random every slot // except archer
        for (int i = 0; i<newLevel.listObject.Count; i++)
        {
            newLevel.listObject[i].objectType=RandomObjectType();
        }

        //spawn a random archer
        int archerPosition = Random.Range(0 , newLevel.width*newLevel.height);
        newLevel.listObject[archerPosition].objectType=ObjectType.Archer;

        //ceate asset file
        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{path}/Level{levelAsset.listLevel.Count}_temp.asset");
        AssetDatabase.CreateAsset(newLevel , assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        levelAsset.listLevel.Add(newLevel);

        // Select the newly created asset
        EditorUtility.FocusProjectWindow();
        Selection.activeObject=newLevel;
    }

    public void GenerateMapDesignedRandom()
    {
        Level newLevel = ScriptableObject.CreateInstance<Level>();

        //random number
        newLevel.width=Random.Range((int)levelWidth.x , (int)levelWidth.y);
        //newBoard.height =Random.Range((int) levelHeight.x , (int)levelHeight.y);
        newLevel.height=newLevel.width;
        CreateDefaultListObject(newLevel);

        //random object number
        int _numberOfArcher = Random.Range((int)numberOfArcher.x , (int)numberOfArcher.y);
        int _numberOfEnemy = Random.Range((int)numberOfEnemies.x , (int)numberOfEnemies.y);
        int _numberOfWall = Random.Range((int)numberOfWall.x , (int)numberOfWall.y);
        int _numberOfHole = Random.Range((int)numberOfHole.x , (int)numberOfHole.y);

        newLevel.numberOfHole=_numberOfHole;
        newLevel.numberOfWall=_numberOfWall;
        newLevel.numberOfEnemy=_numberOfEnemy;

        CreateBoardUnit(newLevel , _numberOfArcher , _numberOfEnemy , _numberOfWall , _numberOfHole);

        // Create the asset file
        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{path}/Level{levelAsset.listLevel.Count}_temp.asset");
        AssetDatabase.CreateAsset(newLevel , assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        levelAsset.listLevel.Add(newLevel);
        // Select the newly created asset
        EditorUtility.FocusProjectWindow();
        Selection.activeObject=newLevel;

        Debug.Log("New MyScriptableObject created: "+assetPath);
    }

    private void CreateDefaultListObject(Level boardData)
    {
        boardData.listObject.Clear();
        for (int i = 0; i<boardData.width*boardData.height; i++)
        {
            boardData.listObject.Add(new SlotData());
        }
    }
    private void CreateBoardUnit(Level newLevel , int archerNum , int enemyNum , int wallNum , int holeNum)
    {
        List<int> listPositionOccupied = new List<int>();
        //spawn enemy
        for (int i = 0; i<enemyNum; i++)
        {
            int enemyPosition = Random.Range(0 , newLevel.height*newLevel.width);

            while (listPositionOccupied.Contains(enemyPosition))
            {
                enemyPosition=Random.Range(0 , newLevel.height*newLevel.width);
            }
            //                   optimize stupid direction
            int enemyDirection = RandomDirection(newLevel , enemyPosition);

            newLevel.listObject[enemyPosition].objectType=SetEnemyDirection(enemyDirection);
            listPositionOccupied.Add(enemyPosition);
        }

        //spawn wall
        for (int i = 0; i<wallNum; i++)
        {
            int wallPosition = Random.Range(0 , newLevel.height*newLevel.width);
            while (listPositionOccupied.Contains(wallPosition))
            {
                wallPosition=Random.Range(0 , newLevel.height*newLevel.width);
            }
            var listDirection = new List<int> { 0 , 1 };
            newLevel.listObject[wallPosition].objectType=RandomUtils.RandomInSpecificRange(listDirection)==0 ? ObjectType.BounceWallAcute : ObjectType.BounceWallGrave;
            listPositionOccupied.Add(wallPosition);
        }
        //spawn hole
        for (int i = 0; i<holeNum; i++)
        {
            int holePosition = Random.Range(0 , newLevel.height*newLevel.width);
            while (listPositionOccupied.Contains(holePosition))
            {
                holePosition = Random.Range(0 , newLevel.height*newLevel.width);
            }
            newLevel.listObject[holePosition].objectType = ObjectType.Wall;
            listPositionOccupied.Add(holePosition);
        }

        //spawn archer
        for (int i = 0; i<archerNum; i++)
        {
            int archerPosition = Random.Range(0 , newLevel.height*newLevel.width);
            while (listPositionOccupied.Contains(archerPosition))
            {
                archerPosition = Random.Range(0 , newLevel.height*newLevel.width);
            }
            newLevel.listObject[archerPosition].objectType = ObjectType.Archer;
            listPositionOccupied.Add(archerPosition);
        }
    }
    private int RandomDirection(Level boardData , int position)
    {
        List<int> listDirection = new List<int> { 0 , 1 , 2 , 3 };
        if (IsUnitInTopRear(boardData , position))
            listDirection.Remove(0);
        if (IsUnitInBotRear(boardData , position))
            listDirection.Remove(1);
        if (IsUnitInLeftRear(boardData , position))
            listDirection.Remove(2);
        if (IsUnitInRightRear(boardData , position))
            listDirection.Remove(3);

        return RandomUtils.RandomInSpecificRange(listDirection);
    }

    private bool IsUnitInTopRear(Level boardData , int position)
    {
        if ((position+1)/boardData.height<=boardData.width)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    private bool IsUnitInBotRear(Level boardData , int position)
    {
        if (position/boardData.height==0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsUnitInLeftRear(Level boardData , int position)
    {
        if (position%boardData.width==0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsUnitInRightRear(Level boardData , int position)
    {
        if ((position+1)%boardData.height==0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private MoveDirection RandomDirection(int i) => i switch
    {
        0 => MoveDirection.Up,
        1 => MoveDirection.Down,
        2 => MoveDirection.Left,
        3 => MoveDirection.Right,
        _ => throw new System.NotImplementedException()
    };
    private ObjectType SetEnemyDirection(int direction) => direction switch
    {
        0 => ObjectType.EnemyUp,
        1 => ObjectType.EnemyDown,
        2 => ObjectType.EnemyLeft,
        3 => ObjectType.EnemyRight,
        _ => throw new System.NotImplementedException()
    };
    #endregion

    [CustomEditor(typeof(LevelGenerator))]
    public class LevelGeneratorGUI : Editor
    {
        public LevelGenerator levelGenerator;
        private SerializedProperty levelIndex;

        private void OnEnable()
        {
            levelGenerator=(LevelGenerator)target;

        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            if (GUILayout.Button("Generate level"))
            {
                if (levelGenerator.isFullyRandom == true)
                {
                    Bug.Log("Please turn off random attribute == tick the fully random");
                    return;
                }
                for (int i = 0; i<levelGenerator.numberOfMap; i++)
                {
                    levelGenerator.GenerateMapDesignedRandom();
                }
            }
            if (GUILayout.Button("Clear"))
            {
                for (int i = 0; i<levelGenerator.numberOfMap; i++)
                {
                    AssetDatabase.DeleteAsset($"{levelGenerator.path}/Level{i}_temp.asset");
                }
                levelGenerator.levelAsset.listLevel.Clear();
            }

            if (GUILayout.Button("Generate fully random"))
            {
                if(levelGenerator.isFullyRandom ==false)
                {
                    Bug.Log("Please turn on random attribute == tick the fully random");
                    return;
                }

                for (int i = 0; i<levelGenerator.numberOfMap; i++)
                {
                    levelGenerator.GenerateFullyRandom();
                }
            }
        }

    }
    public ObjectType RandomObjectType()
    {
        //none//archer//enemy//WallAcute//WallGrave//hole//
        var listTypeIndex = new List<int> { 0 , 1 , 2 , 3 , 4 , 5 , 6 , 7 , 8 };
        var listWeight = new List<int>
            { rateOfNone , rateOfArcher , rateOfEnemyUp , rateOfEnemyDown , rateOfEnemyLeft ,
            rateOfEnemyRight , rateOfWallAcute , rateOfWallGrave , rateOfWallHole };

        int type = RandomUtils.RandomWithWeight(listTypeIndex , listWeight);

        switch (type)
        {
            case 0:
                return ObjectType.None;
            case 1:
                return ObjectType.Archer;
            case 2:
                return ObjectType.EnemyUp;
            case 3:
                return ObjectType.EnemyDown;
            case 4:
                return ObjectType.EnemyLeft;
            case 5:
                return ObjectType.EnemyRight;
            case 6:
                return ObjectType.BounceWallAcute;
            case 7:
                return ObjectType.BounceWallGrave;
            case 8:
                return ObjectType.Wall;
            default: return ObjectType.None;
        }
    }
}

#endif