using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    public bool isSlowed = false;
    public bool isDead = false;
    public Rigidbody2D rb;
    public float moveSpeed = 2f;

    public int worthLives;


    private Transform target;
    private int pathIndex = 0;

    public int hitPoints = 10;
    public bool isDestroyed = false;
    public int pointsWorth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        target = GameManager.main.path[pathIndex];
        InitializeEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            if (pathIndex == GameManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                GameManager.main.LoseLives(worthLives);
                Destroy(gameObject);
                return;
            }
            else
            {
                target = GameManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    protected abstract bool IsWeakAgainst(DamageType type);


    public void TakeDamage(int amount, DamageType type)
    {
        if (IsWeakAgainst(type))
        {
            amount *= 2; // Double damage if weak against this type
        }

        hitPoints -= amount;

        if (hitPoints <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            GameManager.main.AddPoints(pointsWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    // Initialize enemy-specific properties
    protected abstract void InitializeEnemy();
}
