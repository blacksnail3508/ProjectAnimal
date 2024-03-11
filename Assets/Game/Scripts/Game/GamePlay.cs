using LazyFramework;
using System;
using System.Threading.Tasks;
using UnityEngine;
public class GamePlay : MonoBehaviour
{
    [SerializeField] LevelAsset levelAsset;
    [SerializeField] Cage cage;
    [SerializeField] int animalRemaining;
    [SerializeField] Predator wolf;

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
        //clear history
        GameServices.ClearHistory();

        //reset and release all pool
        animalRemaining=0;

        //fetch size from level asset
        var data = levelAsset.listLevel[e.Level];
        int sizeX = data.sizeX;
        int sizeY = data.sizeY;
        //create game board
        GameServices.SaveCurrentLevelSize(sizeX, sizeY);
        cage.Create(sizeX, sizeY);

        //relocate predator
        wolf.SetPosition(-2 , sizeY+1);

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

    private void OnCannonShot(OnCannonShot e)
    {
        animalRemaining--;
        if (animalRemaining == 0)
        {
            //end turn

            if (GameServices.IsAllAnimalSafe())
            {
                //predator is upset

                PlayerService.UpdateLevel();
                DisplayService.ShowPopup(UIPopupName.PopupWin);
            }
            else
            {
                //predator hunt

                Action onReachTarget = () =>
                {
                    DisplayService.ShowPopup(UIPopupName.PopupLose);
                };

                wolf.StartHunt(onReachTarget);
            }
        }
    }

    public void OnUndo(OnUndo e)
    {
        if(animalRemaining == 0)
        {

        }
        else
        {
            animalRemaining++;
        }

    }
    private void Subscribe()
    {
        Event<OnPlayLevel>.Subscribe(OnPlayLevel);
        Event<OnCannonLoaded>.Subscribe(OnCannonLoaded);
        Event<OnCannonShot>.Subscribe(OnCannonShot);
        Event<OnUndo>.Subscribe(OnUndo);
    }
    private void Unsubscribe()
    {
        Event<OnPlayLevel>.Unsubscribe(OnPlayLevel);
        Event<OnCannonLoaded>.Unsubscribe(OnCannonLoaded);
        Event<OnCannonShot>.Unsubscribe(OnCannonShot);
        Event<OnUndo>.Unsubscribe(OnUndo);
    }
}
