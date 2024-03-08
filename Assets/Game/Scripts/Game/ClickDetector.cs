using LazyFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] LayerMask animalLayer;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition)
                ,Vector2.zero , Mathf.Infinity , animalLayer);

            if (hit.collider==null) return;

            if(hit.collider.CompareTag("Cannon"))
            {
                AnimalCannon cannon = hit.collider.GetComponent<AnimalCannon>();
                cannon.Shoot();
            }
        }
    }
}
