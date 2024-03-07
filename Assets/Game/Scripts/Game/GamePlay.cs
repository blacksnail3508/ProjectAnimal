using LazyFramework;
using System.Threading.Tasks;
using UnityEngine;
public class GamePlay : MonoBehaviour
{
    [SerializeField] LevelAsset levelAsset;
    [SerializeField] Cage cage;
    [SerializeField] int animalRemaining;

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
        //reset and release all pool
        animalRemaining=0;

        //fetch size from level asset
        var data = levelAsset.listLevel[e.Level];
        int sizeX = data.sizeX;
        int sizeY = data.sizeY;
        //create game board
        GameServices.SaveCurrentLevelSize(sizeX, sizeY);
        cage.Create(sizeX, sizeY);

        //set camera size
        GameServices.SetCameraZoom(Camera.main);

        //load cannon
        foreach (var cannonData in data.listCannon)
        {
            cage.SetCannon(cannonData.posX , cannonData.posY , cannonData.animalCount);
        }
    }

    private void OnCannonLoaded(OnCannonLoaded e)
    {
        animalRemaining += e.count;
    }

    private async void OnCannonShot(OnCannonShot e)
    {
        animalRemaining--;
        await Task.Delay(500);
        if (animalRemaining == 0)
        {
            //win

            if (GameServices.IsAllAnimalSafe())
            {
                PlayerService.UpdateLevel();
                DisplayService.ShowPopup(UIPopupName.PopupWin);
            }
            else
            {
                DisplayService.ShowPopup(UIPopupName.PopupLose);
            }
        }
    }

    private void Subscribe()
    {
        Event<OnPlayLevel>.Subscribe(OnPlayLevel);
        Event<OnCannonLoaded>.Subscribe(OnCannonLoaded);
        Event<OnCannonShot>.Subscribe(OnCannonShot);
    }
    private void Unsubscribe()
    {
        Event<OnPlayLevel>.Unsubscribe(OnPlayLevel);
        Event<OnCannonLoaded>.Unsubscribe(OnCannonLoaded);
        Event<OnCannonShot>.Unsubscribe(OnCannonShot);
    }
}
