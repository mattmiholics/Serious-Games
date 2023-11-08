using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public bool isDead = false;
    public Rigidbody2D rb;

    private float moveSpeed = 2f;
    private Transform target;
    private int pathIndex = 0;
    public int worthLives = 1;
    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.main.path[pathIndex];
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
}
