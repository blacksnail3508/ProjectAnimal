using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] LevelAsset levelAsset;
    [SerializeField] Transform obstacleRoot;
    [SerializeField] Box boxPrefab;

    List<BoardObject> listBox = new List<BoardObject>();
    void SpawnObject(ObstacleData data)
    {
        switch (data.type)
        {
            case ObjectType.Box:
                var box = CreateOrReuseBox();
                box.gameObject.SetActive(true);
                box.SetPosition(data.posX , data.posY);
                break;
        }
    }

    public void CreateObject(int level)
    {
        var data = levelAsset.listLevel[level].listObstacle;

        foreach (var obstacle in data)
        {
            SpawnObject(obstacle);
        }
    }

    public void ReturnPoolAll()
    {
        foreach (var item in listBox)
        {
            item.gameObject.SetActive(false);
        }
    }
    private Box CreateOrReuseBox()
    {
        foreach(var box in listBox)
        {
            if (box.gameObject.activeSelf == false) return (Box)box;
        }

        var newBox = Instantiate(boxPrefab , obstacleRoot);

        listBox.Add(newBox);
        GameServices.AddObstacle(newBox);

        return newBox;
    }
}
