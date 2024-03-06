using System;
using UnityEngine;

public class SpikeTrap : TrapBase
{
    [SerializeField] SpriteRenderer spike;
    public override void Activate()
    {
        isActive = !isActive;
        UpdateState();
    }
    public override void UpdateState()
    {
        if(isActive)
        {
            SpikeOn();
        }
        else
        {
            SpikeOff();
        }
    }
    public void SpikeOn()
    {
        isActive = true;
        spike.gameObject.SetActive(true);
    }
    public void SpikeOff()
    {
        isActive = false;
        spike.gameObject.SetActive(false);
    }
    public void OnArcherStep(Archer archer)
    {
        if (isActive == true)
        {
            archer.OnHit();
        }
    }
}
