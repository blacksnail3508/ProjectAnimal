using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : Collectible
{
    public override void OnCollected()
    {
        this.gameObject.SetActive(false);
    }
}
