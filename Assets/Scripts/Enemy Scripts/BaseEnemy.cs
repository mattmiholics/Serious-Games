using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : EnemyScript
{
        protected override void InitializeEnemy()
        {
            // Initialization code...
        }

        protected override bool IsWeakAgainst(DamageType type)
        {
            return type == null;
        }
 }

