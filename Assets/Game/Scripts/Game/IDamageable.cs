using LazyFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDamageable : BoardObject
{
    [SerializeField] public bool isDead = false;
    public virtual void OnHit()
    {
        gameObject.SetActive(false);
        isDead = true;
    }
    public bool IsDead()
    {
        return isDead;
    }
    public void Reactive()
    {
        gameObject.SetActive(true);
        isDead=false;
    }
    public void Deactive()
    {
        gameObject.SetActive(false);
        isDead=true;
    }
}
