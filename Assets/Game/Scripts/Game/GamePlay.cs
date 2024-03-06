using LazyFramework;
using UnityEngine;
public class GamePlay : MonoBehaviour
{
    [SerializeField] LevelAsset levelAsset;
    [SerializeField] Cage cage;
    [SerializeField] Prey pigPrefab;

    [SerializeField] Transform animalRoot;

    private void Start()
    {
        Subscribe();

        //create game board
        cage.Create(3 , 3);
        GameServices.SaveCurrentLevelSize(3 , 3);

        //spawn prey
        var newPig = Instantiate(pigPrefab , animalRoot);
        newPig.Init(FaceDirection.Up , 2 , 0 , -1);
        GameServices.AddAnimal(newPig);

        var newPig2 = Instantiate(pigPrefab , animalRoot);
        newPig2.Init(FaceDirection.Up , 2 , 0 , -3);
        GameServices.AddAnimal(newPig2);
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

        //spawn prey
        var newPig = Instantiate(pigPrefab , animalRoot);
        newPig.Init(FaceDirection.Up , 2 , 0 , -1);

        //spawn predator

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
