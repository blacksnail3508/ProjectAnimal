#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotButton : MonoBehaviour
{
    [SerializeField] MapCreator creator;
    [Header("Sprite")]
    [SerializeField] List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField] Image slotImage;
    public ObjectType ObjectType;
    int currentType = 0;
    public Vector2 telePosition;
    public bool isOn = false;

    public void OnClick()
    {
        if (creator.selectedSlot != this)
        {
            if(creator.selectedSlot !=null)
            {
                creator.selectedSlot.SetUnselected();
            }

            creator.selectedSlot=this;
            this.SetSelected();
        }
        else
        {
            if (ObjectType==ObjectType.Teleport)
            {
                creator.ShowPopupTele();
            }

            if (ObjectType==ObjectType.SpikeTrap)
            {
                creator.ShowPopupSpike(isOn);
            }
        }

    }
    public void SetTelePosition(int x, int y)
    {
        telePosition = new Vector2(x, y);
    }
    public void SetSelected()
    {
        slotImage.color=Color.red;
    }
    public void SetUnselected()
    {
        slotImage.color=Color.white;
    }
    public void SetType(int index)
    {
        currentType=index;
        ObjectType=GetObjectType(currentType);
        ChangeImage();
    }
    public void SetType(ObjectType type)
    {
        ObjectType=type;
        currentType=GetTypeIndex(type);
        ChangeImage();
    }
    public ObjectType GetObjectType(int index) => index switch
    {
        0 => ObjectType.None,
        1 => ObjectType.Archer,
        2 => ObjectType.EnemyUp,
        3 => ObjectType.EnemyDown,
        4 => ObjectType.EnemyLeft,
        5 => ObjectType.EnemyRight,
        6 => ObjectType.BounceWallAcute,
        7 => ObjectType.BounceWallGrave,
        8 => ObjectType.Wall,
        9 => ObjectType.Box,
        10 => ObjectType.TurretUp,
        11 => ObjectType.TurretDown,
        12 => ObjectType.TurretLeft,
        13 => ObjectType.TurretRight,
        14 => ObjectType.Shield,
        15 => ObjectType.Split,
        16 => ObjectType.Teleport,
        17 => ObjectType.IceSurface,
        18 => ObjectType.SpikeTrap,
        _ => throw new System.NotImplementedException()
    };
    public int GetTypeIndex(ObjectType type) => type switch
    {
        ObjectType.None => 0,
        ObjectType.Archer => 1,
        ObjectType.EnemyUp => 2,
        ObjectType.EnemyDown => 3,
        ObjectType.EnemyLeft => 4,
        ObjectType.EnemyRight => 5,
        ObjectType.BounceWallAcute => 6,
        ObjectType.BounceWallGrave => 7,
        ObjectType.Wall => 8,
        ObjectType.Box => 9,
        ObjectType.TurretUp => 10,
        ObjectType.TurretDown => 11,
        ObjectType.TurretLeft => 12,
        ObjectType.TurretRight => 13,
        ObjectType.Shield => 14,
        ObjectType.Split => 15,
        ObjectType.Teleport => 16,
        ObjectType.IceSurface => 17,
        ObjectType.SpikeTrap => 18,
        _ => throw new System.NotImplementedException()
    };
    private void ChangeImage()
    {
        foreach (var obj in gameObjects)
        {
            obj.gameObject.SetActive(false);
        }

        gameObjects[currentType].SetActive(true);
    }
}
#endif