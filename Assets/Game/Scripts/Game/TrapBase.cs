using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBase : BoardObject
{
    public bool isActive = false;

    /// <summary>
    /// call 1st every turn
    /// </summary>
    public virtual void Activate()
    {

    }
    public virtual void UpdateState()
    {

    }
}
