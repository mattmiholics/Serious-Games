using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungalCellEnemy : EnemyScript
{
    protected override void InitializeEnemy()
    {
        moveSpeed = 4f; // Faster speed
        worthLives = 1;
    }

    protected override bool IsWeakAgainst(DamageType type)
    {
        return type == null;
    }
}
