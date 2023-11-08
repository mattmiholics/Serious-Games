using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hitPoints = 10;
    public bool isDestroyed = false;
    public int pointsWorth;
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0 && !isDestroyed) 
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            GameManager.main.AddPoints(pointsWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
