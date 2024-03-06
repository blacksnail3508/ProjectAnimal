using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BoardData" , menuName = "ScriptableObjects/BoardData")]
public class Level : ScriptableObject
{
    [SerializeField] public int width;
    [SerializeField] public int height;
    [SerializeField] public List<SlotData> listObject = new List<SlotData>();

    [SerializeField] public int dificulty;
    [SerializeField] public string solution;

    [SerializeField] public int numberOfEnemy;
    [SerializeField] public int numberOfWall;
    [SerializeField] public int numberOfHole;
    private void OnValidate()
    {
        for (int i = 0; i<listObject.Count; i++)
        {
            listObject[i].valueX=i%width;
            listObject[i].valueY=i/height;
            listObject[i].slotName=$"slot {listObject[i].valueX}_{listObject[i].valueY}";
        }
    }
}
[Serializable]
public class SlotData
{
    public string slotName;
    public int valueX;
    public int valueY;
    public ObjectType objectType;
    [DrawIf("objectType" , ObjectType.Teleport)] public Vector2 TargetPosition;
    [DrawIf("objectType" , ObjectType.SpikeTrap)] public bool isOn;
}
