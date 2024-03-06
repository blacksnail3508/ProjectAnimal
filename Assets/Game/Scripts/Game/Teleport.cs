using LazyFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : BoardObject
{
    [Header("Teleport position")]
    [SerializeField] Vector2 targetPosition;

    public void DoTeleport(BoardObject archer)
    {
        Bug.Log($"{archer.name} teleport to {targetPosition}");
        archer.SetPosition(this.targetPosition);
    }
    public void SetTargetPosition(Vector2 to)
    {
        targetPosition = to;
    }
}
