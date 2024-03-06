using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] SpriteRenderer squarePrefab;
    [SerializeField] SpriteRenderer boardRenderer;
    public void SetupBoard(int width, int height)
    {
        //save board size for calculation
        GameServices.SaveCurrentLevelSize(width, height);

        Clear();
        boardRenderer.size = new Vector2 (width, height);

        for(int valueX = 0; valueX < width; valueX++)
        {
            for(int valueY = 0; valueY < height; valueY++)
            {
                var square = Instantiate(squarePrefab, this.transform);
                square.transform.localPosition = GameServices.BoardPositionToLocalPosition(valueX,valueY);
                square.gameObject.SetActive(true);
            }
        }
    }
    public void Clear()
    {
        for(int i = transform.childCount -1; i>=0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
