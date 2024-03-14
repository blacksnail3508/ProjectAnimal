using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateShop : MonoBehaviour
{
    [SerializeField] SkateBoardLibrary library;
    [SerializeField] SkateButton skateButtonPrefab;
    [SerializeField] Transform layout;
    [SerializeField] RectTransform rect;

    List<SkateButton> listButton = new List<SkateButton>();

    private void Start()
    {
        Load();
    }
    private void Load()
    {
        for(int  i= 0; i< library.listSkateBoard.Count; i++)
        {
            var newButton = Instantiate(skateButtonPrefab , layout);
            newButton.SetData(library.listSkateBoard[i],i);
            listButton.Add(newButton);
        }

        var row = library.listSkateBoard.Count.DivideRoundUp(3);
        Resize(row);

    }
    public void Resize(int numberOfRow)
    {
        var height = 300*numberOfRow + (numberOfRow - 1)*50 + 200;
        rect.sizeDelta = new Vector2 (0, height);
    }

    public void Reload()
    {
        //foreach(var item in listButton)
        //{
        //    item.CheckUnlock();
        //}
    }
}
