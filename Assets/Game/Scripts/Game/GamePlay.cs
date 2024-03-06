using LazyFramework;
using UnityEngine;
public class GamePlay : MonoBehaviour
{
    [SerializeField] LevelAsset levelAsset;
    [SerializeField] Cage cage;

    private void Start()
    {
        Subscribe();
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void OnPlayLevel(OnPlayLevel e)
    {
        //fetch size from level asset
        int sizeX = levelAsset.listLevel[e.Level].sizeX;
        int sizeY = levelAsset.listLevel[e.Level].sizeY;

        //create game board
        cage.Create(sizeX, sizeY);
    }
    private void Subscribe()
    {
        Event<OnPlayLevel>.Subscribe(OnPlayLevel);
    }
    private void Unsubscribe()
    {
        Event<OnPlayLevel>.Unsubscribe(OnPlayLevel);
    }
}
