using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] LayerMask animalLayer;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //Bug.Log("Click on ui");
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition)
                , Vector2.zero, Mathf.Infinity, animalLayer);

            if (hit.collider == null) return;

            if (hit.collider.CompareTag("Cannon"))
            {
                AnimalCannon cannon = hit.collider.GetComponent<AnimalCannon>();
                cannon.Shoot();
            }
        }
    }
}
