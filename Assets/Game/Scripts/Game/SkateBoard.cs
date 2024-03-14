using UnityEngine;

public class SkateBoard : MonoBehaviour
{
    [SerializeField] SkateBoardLibrary library;

    [SerializeField] SpriteRenderer board;

    void Load(int index)
    {
        board.sprite=library.GetBoard(index);
    }

    public void RandomLoad()
    {
        var index = Random.Range(0 , library.listSkateBoard.Count);
        Load(index);
    }
    public void LoadEquipingSkateBoard()
    {
        Load(DataService.GetEquipingSkateBoard());
    }
}
