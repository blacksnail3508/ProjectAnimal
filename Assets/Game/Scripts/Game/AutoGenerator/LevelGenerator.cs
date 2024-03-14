#if UNITY_EDITOR
using LazyFramework;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public LevelAsset levelAsset;
    public int row;
    public int col;
    public int difficult;
    public int randomTime;
    public bool isBoxAllowed = false;

    int maxX;
    int maxY;

    public void RandomGenerate()
    {
        for (int i = 0; i<randomTime; i++)
        {
            int row = RandomUtils.RandomInSpecificRange(new int[] { 3 , 4 , 5 , 6 , 7 , 8 });
            int col = RandomUtils.RandomInSpecificRange(new int[] { 3 , 4 , 5 , 6 , 7 , 8 });
            int dif = RandomUtils.RandomInSpecificRange(new int[] { 5 , 8 , 10});
            AutoGenerate(dif , row , col);
        }
    }
    public void Generate()
    {
        AutoGenerate(row, col, difficult);
    }
    public void AutoGenerate(int m , int n , int difficulty)
    {
        int[,] rectangle = new int[m , n];
        maxX=m; maxY=n;

        System.Random rand = new System.Random();
        int randomShape;
        for (int i = 0; i<m; i++)
        {
            for (int j = 0; j<n; j++)
            {
                if (rectangle[i , j]==0&&rand.Next(10)<difficulty)
                {
                    if (isBoxAllowed == true)
                        randomShape = rand.Next(3); // Thêm lựa chọn cho hình vuông
                    else
                    {
                        randomShape = rand.Next(2);
                    }

                    if (randomShape==0&&i<m-1&&rectangle[i+1 , j]==0)
                    {
                        rectangle[i , j]=1;
                        rectangle[i+1 , j]=1;
                    }
                    else if (randomShape==1&&j<n-1&&rectangle[i , j+1]==0)
                    {
                        rectangle[i , j]=2;
                        rectangle[i , j+1]=2;
                    }
                    else if (randomShape==2)
                    {
                        rectangle[i , j]=3; // Giá trị 3 cho hình vuông
                    }
                }
            }
        }
        //DisplayBoard(rectangle,maxX,maxY);

        //save to asset
        var newLevel = new Level();
        newLevel.sizeX = maxX; newLevel.sizeY = maxY;

        SaveCannonHorizontal(rectangle , m , n, newLevel);
        SaveCannonVertical(rectangle , m , n, newLevel);
        SaveBoxPosition(rectangle , m , n , newLevel);

        newLevel.name=$"Level {levelAsset.listLevel.Count}";
        levelAsset.listLevel.Add(newLevel);
    }

    void SaveCannonHorizontal(int[,] rectangle , int m , int n, Level level)
    {
        for (int i = 0; i<m; i++)
        {
            int count = 0;
            for (int j = 0; j<n; j++)
            {
                // Chỉ đếm các ô có giá trị là 2
                if (rectangle[i , j]==2)
                {
                    count++;
                    // Nếu đang xét hình chữ nhật dạng 2x1, bỏ qua ô kế tiếp
                    j++;
                }
            }

            if (count!=0)
            {
                var rand = RandomUtils.RandomInSpecificRange(new int[] { -1 , maxY});

                Bug.Log($"Cannon ({rand},{i}) {count} pigs");
                var newCannon = new CannonData();
                newCannon.posY=rand;
                newCannon.posX=i;
                newCannon.animalCount=count;

                level.listCannon.Add(newCannon);
            }

        }
    }

    void SaveCannonVertical(int[,] rectangle , int m , int n, Level level)
    {
        for (int j = 0; j<n; j++)
        {
            int count = 0;
            for (int i = 0; i<m; i++)
            {
                // Chỉ đếm các ô có giá trị là 1
                if (rectangle[i , j]==1)
                {
                    count++;
                    // Nếu đang xét hình chữ nhật dạng 1x2, bỏ qua hàng kế tiếp
                    i++;
                }
            }

            if(count !=0)
            {
                var rand = RandomUtils.RandomInSpecificRange(new int[] { -1 , maxX});

                Bug.Log($"Cannon ({j+1},{rand}) {count} pigs");
                var newCannon = new CannonData();
                newCannon.posX=rand;
                newCannon.posY=j;
                newCannon.animalCount=count;

                level.listCannon.Add(newCannon);
            }
        }
    }
    private void DisplayBoard(int[,] rectangle,int m, int n)
    {
        // In ra màn hình để kiểm tra kết quả
        for (int i = 0; i<m; i++)
        {
            string row = "";
            for (int j = 0; j<n; j++)
            {
                row+=rectangle[i , j]+" ";
            }
            Debug.Log(row);
        }
    }
    void SaveBoxPosition(int[,] rectangle , int m , int n,Level level)
    {
        for (int i = 0; i<m; i++)
        {
            for (int j = 0; j<n; j++)
            {
                if (rectangle[i , j]==3) // Kiểm tra nếu giá trị là 3
                {
                    var newBox = new ObstacleData();
                    newBox.type=ObjectType.Box;
                    newBox.posX = i;
                    newBox.posY = j;
                    level.listObstacle.Add(newBox);
                    //Bug.Log($"Square at ({i},{j})");
                }
            }
        }
    }
}
#endif