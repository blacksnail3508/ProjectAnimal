using UnityEngine;

public class SkateBoard : MonoBehaviour
{
    [SerializeField] SpriteLibrary library;

    [SerializeField] SpriteRenderer board;
    [SerializeField] SpriteRenderer decor;

    public void Load(int boardId , int decorId)
    {
        board.sprite=library.GetBoard(boardId);
        decor.sprite=library.GetDecor(decorId);
    }

    public void RandomLoad()
    {
        var boardId = Random.Range(0 , library.SkateBoardAsset.Count);
        var decorId = Random.Range(0 , library.SkateDecorAsset.Count);

        board.sprite=library.GetBoard(boardId);
        decor.sprite=library.GetDecor(decorId);
    }
}
