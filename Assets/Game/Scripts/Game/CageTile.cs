using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageTile : MonoBehaviour
{
    public void ReturnPool()
    {
        gameObject.SetActive(false);
    }
}
