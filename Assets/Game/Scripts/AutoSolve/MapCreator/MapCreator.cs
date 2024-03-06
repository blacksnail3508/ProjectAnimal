#if UNITY_EDITOR
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MapCreator : MonoBehaviour
{
    [Header("save path")]
    [SerializeField] string path;
    [Header("Data")]
    [SerializeField] LevelAsset levelAsset;
    [Header("References")]
    [SerializeField] GridLayoutGroup buttonLayout;
    [SerializeField] GridLayoutGroup menuRoot;
    [SerializeField] TMP_InputField mapSizeInput;
    [SerializeField] TMP_InputField inputX;
    [SerializeField] TMP_InputField inputY;
    [Header("Popup")]
    [SerializeField] GameObject popupTele;
    [SerializeField] GameObject popupSpike;
    [SerializeField] GameObject spike;

    [Header("Ruler")]
    [SerializeField] Ruler rulerX;
    [SerializeField] Ruler rulerY;
    [Header("Prefabs")]
    [SerializeField] SlotButton slotButtonPrefab;
    [SerializeField] CreatorLevelButton levelButtonPrefab;
    [Header("input data")]
    [SerializeField] List<SlotButton> listSlot;
    [SerializeField] public SlotButton selectedSlot;
    public int selectedType = 0;
    int selectedLevel = 0;
    int size;

    private void Start()
    {
        CreateMenu();
    }
    public void Clear()
    {
        foreach(var slot in listSlot)
        {
            slot.SetType(0);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }
    public void ShowPopupTele()
    {
        popupTele.SetActive(true);
    }
    public void ShowPopupSpike(bool isOn)
    {
        popupSpike.SetActive(true);
        spike.SetActive(isOn);
        selectedSlot.isOn = isOn;
    }

    public void SetSpikeState()
    {
        bool isOn = !selectedSlot.isOn;
        selectedSlot.isOn=isOn;
        spike.SetActive(isOn);
    }
    public void SetTelePosition()
    {
        int x = int.Parse(inputX.text);
        int y = int.Parse(inputY.text);
        selectedSlot.SetTelePosition(x , y);
        popupTele.SetActive(false);
    }
    public void CreateMap()
    {
        size = int.Parse(mapSizeInput.text);

        rulerX.SetSize(size);
        rulerY.SetSize(size);

        int numberOfSlot = size*size;
        buttonLayout.cellSize=new Vector3(1000f/size , 1000f/size);

        //reuse
        if (listSlot.Count>numberOfSlot)
        {
            foreach (SlotButton button in listSlot)
            {
                button.gameObject.SetActive(false);
            }

            for (int i = 0; i<numberOfSlot; i++)
            {
                listSlot[i].gameObject.SetActive(true);
            }
        }
        else        //create new
        {
            foreach (SlotButton button in listSlot)
            {
                button.gameObject.SetActive(true);
            }
            int buttonToCreate = numberOfSlot-listSlot.Count;
            for (int i = 0; i<buttonToCreate; i++)
            {
                var button = Instantiate(slotButtonPrefab , buttonLayout.transform);
                listSlot.Add(button);
            }
        }
    }

    public void SaveMapAsNew()
    {
        //save new map

        Level newLevel = ScriptableObject.CreateInstance<Level>();
        newLevel.width = size;
        newLevel.height = size;
        newLevel.listObject.Clear();
        for (int i = 0; i<size*size; i++)
        {
            var newSlot = new SlotData();
            newSlot.objectType = listSlot[i].ObjectType;
            newSlot.valueX = i%size;
            newSlot.valueY = i/size;
            newSlot.slotName = $"slot {newSlot.valueX}_{newSlot.valueY}";

            //save teleport position
            if(newSlot.objectType ==ObjectType.Teleport)
            {
                newSlot.TargetPosition = listSlot[i].telePosition;
            }

            //save spike state
            if(newSlot.objectType ==ObjectType.SpikeTrap)
            {
                newSlot.isOn = listSlot[i].isOn;
            }
            newLevel.listObject.Add(newSlot);
        }

        //ceate asset file
        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{path}/Level{levelAsset.listLevel.Count}.asset");
        AssetDatabase.CreateAsset(newLevel , assetPath);
        AssetDatabase.SaveAssets();

        levelAsset.listLevel.Add(newLevel);

        CreateNewMenuButton();
    }
    public void CreateMenu()
    {
        for (int i = 0; i<levelAsset.listLevel.Count; i++)
        {
            var levelButton = Instantiate(levelButtonPrefab , menuRoot.transform);
            levelButton.gameObject.SetActive(true);
            levelButton.SetData(i);
        }
    }
    public void CreateNewMenuButton()
    {
        int index = levelAsset.listLevel.Count-1;
        var levelButton = Instantiate(levelButtonPrefab , menuRoot.transform);
        levelButton.gameObject.SetActive(true);
        levelButton.SetData(index);
    }

    public void LoadLevel(int index)
    {
        selectedLevel=index;
        var level = levelAsset.listLevel[index];

        size=level.width;
        rulerX.SetSize(size);
        rulerY.SetSize(size);
        int numberOfSlot = size*size;
        buttonLayout.cellSize=new Vector3(1000f/size , 1000f/size);

        //reuse
        if (listSlot.Count>numberOfSlot)
        {
            foreach (SlotButton button in listSlot)
            {
                button.gameObject.SetActive(false);
            }

            for (int i = 0; i<numberOfSlot; i++)
            {
                listSlot[i].gameObject.SetActive(true);
            }
        }
        else        //create new
        {
            foreach (SlotButton button in listSlot)
            {
                button.gameObject.SetActive(true);
            }
            int buttonToCreate = numberOfSlot-listSlot.Count;
            for (int i = 0; i<buttonToCreate; i++)
            {
                var button = Instantiate(slotButtonPrefab , buttonLayout.transform);
                listSlot.Add(button);
            }
        }

        for (int i = 0; i<numberOfSlot; i++)
        {
            var newSlot = listSlot[i];
            var data = level.listObject[i];
            newSlot.SetType(data.objectType);
            newSlot.isOn = data.isOn;
            newSlot.telePosition = data.TargetPosition;
        }
    }
    public void Save()
    {
        var oldLevel = levelAsset.listLevel[selectedLevel];
        oldLevel.width=size;
        oldLevel.height=size;
        oldLevel.listObject.Clear();
        for (int i = 0; i<size*size; i++)
        {
            var newSlot = new SlotData();
            newSlot.objectType=listSlot[i].ObjectType;
            newSlot.valueX=i%size;
            newSlot.valueY=i/size;
            newSlot.slotName=$"slot {newSlot.valueX}_{newSlot.valueY}";
            if (newSlot.objectType==ObjectType.Teleport)
            {
                newSlot.TargetPosition=listSlot[i].telePosition;
            }
            //save spike state
            if (newSlot.objectType==ObjectType.SpikeTrap)
            {
                newSlot.isOn=listSlot[i].isOn;
            }
            oldLevel.listObject.Add(newSlot);
        }
    }
}
#endif
