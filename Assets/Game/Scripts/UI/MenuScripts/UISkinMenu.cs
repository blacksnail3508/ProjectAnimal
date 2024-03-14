using LazyFramework;
using System;
using UnityEngine;

public class UISkinMenu : UIMenuBase
{
    [SerializeField] SkateShop skateShop;

    protected override void OnEnable()
    {
        base.OnEnable();

        Reload();
    }

    public void Reload()
    {
        skateShop.Reload();
    }
}
