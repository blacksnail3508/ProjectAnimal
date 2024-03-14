using LazyFramework;
using System;
using UnityEngine;
public class GamePlay : MonoBehaviour
{
    [SerializeField] LevelAsset levelAsset;
    [SerializeField] Cage cage;
    [SerializeField] Predator wolf;
    [SerializeField] CombatEffect combat;
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
        CancelInvoke();
        //clear history
        GameServices.ClearHistory();
        GameServices.ClearCannon();
        GameServices.ClearPath();
        GameServices.UndoEnable=true;
        cage.HideObstacles();

        //reset and release all pool
        wolf.Idle();
        combat.Hide();

        //fetch size from level asset
        var data = levelAsset.listLevel[e.Level];
        int sizeX = data.sizeX;
        int sizeY = data.sizeY;
        //create game board
        GameServices.SaveCurrentLevelSize(sizeX , sizeY);
        cage.Create(sizeX , sizeY);

        //relocate predator
        wolf.SetPosition(-2 , sizeY+1);

        //set camera size
        GameServices.SetCameraZoom(Camera.main);

        //load cannon
        foreach (var cannonData in data.listCannon)
        {
            cage.SetCannon(cannonData.posX , cannonData.posY , cannonData.animalCount);
        }

        //load obstacles
        cage.CreateObstacles(e.Level);
    }

    private void OnCannonShot(OnCannonShot e)
    {
        if (GameServices.IsAllCannonShot()==true)
        {
            //end turn
            GameServices.UndoEnable = false;

            if (GameServices.IsAllAnimalSafe())
            {
                //predator is upset

                PlayerService.UpdateLevel();
                GameServices.AnimalCelebrate();
                Invoke("ShowPopupWin" , 1);
            }
            else
            {
                //predator hunt

                Action onReachTarget = () =>
                {
                    Action callback = () =>
                    {
                        DisplayService.ShowPopup(UIPopupName.PopupLose);
                    };

                    //hide animal
                    wolf.Hide();
                    GameServices.UnsafeAnimal().ReturnPool();

                    //show combat
                    combat.SetPosition(wolf.positionX , wolf.positionY);
                    combat.Play(callback);
                };

                wolf.StartHunt(onReachTarget);
            }
        }
    }
    private void Subscribe()
    {
        Event<OnPlayLevel>.Subscribe(OnPlayLevel);
        Event<OnCannonShot>.Subscribe(OnCannonShot);
    }
    private void Unsubscribe()
    {
        Event<OnPlayLevel>.Unsubscribe(OnPlayLevel);
        Event<OnCannonShot>.Unsubscribe(OnCannonShot);
    }

    private void ShowPopupWin()
    {
        DisplayService.ShowPopup(UIPopupName.PopupWin);
    }
}
