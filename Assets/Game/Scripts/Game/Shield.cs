using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Collectible
{
    public override void OnCollected()
    {
        this.gameObject.SetActive(false);
    }
}
