using UnityEngine;
using UnityEngine.EventSystems;
using LazyFramework;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] LayerMask cannonLayer;

    [SerializeField] LayerMask animalLayer;
    void Update()
    {
        if(GameServices.GameMode == GameMode.Normal)
        {
            NormalUpdateMethod();
        }

        if(GameServices.GameMode == GameMode.Ufo)
        {
            UfoUpdateMethod();
        }

    }
    void NormalUpdateMethod()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //Bug.Log("Click on ui");
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition)
                , Vector2.zero , Mathf.Infinity , cannonLayer);

            if (hit.collider==null) return;

            if (hit.collider.CompareTag("Cannon"))
            {
                AnimalCannon cannon = hit.collider.GetComponent<AnimalCannon>();
                cannon.Shoot();
            }
        }
    }

    void UfoUpdateMethod()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if (EventSystem.current.IsPointerOverGameObject())
            //{
            //    Bug.Log("Click on ui");
            //    return;
            //}

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition)
                , Vector2.zero , Mathf.Infinity , animalLayer);

            if (hit.collider == null) return;

            if (hit.collider.CompareTag("Animal"))
            {
                Animal animal = hit.collider.GetComponent<Animal>();

                //animation

                //safe animal
                animal.Captured();
                DisplayService.HidePopup(UIPopupName.PopupUfo);
                GameServices.StopUfoMode();
            }
        }
    }
}
