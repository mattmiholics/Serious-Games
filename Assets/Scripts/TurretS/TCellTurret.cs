using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCellTurret : MonoBehaviour
{
    [SerializeField]private GameManager gameManager;
    [SerializeField]int damage = 1;

    public int pointsRequired = 50;
    public float turretRange = 5f;
    public float turretReload = 4f;
    public bool boosted = false;

    private float originalSpeed;
    private float timer;

    private void Update()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timer -= Time.deltaTime;

        foreach (GameObject enemy in gameManager.enemiesList)
        {
            if (enemy)
            {
                if (enemy.GetComponent<EnemyScript>())
                {
                    originalSpeed = enemy.GetComponent<EnemyScript>().moveSpeed;
                    if (Vector3.Distance(transform.position, enemy.transform.position) <= turretRange)
                    {
                        if (!enemy.GetComponent<EnemyScript>().isSlowed)
                        {
                            enemy.GetComponent<EnemyScript>().isSlowed = true;
                            enemy.GetComponent<EnemyScript>().moveSpeed *= 0.85f;
                        }
                    }
                    else
                    {
                        enemy.GetComponent<EnemyScript>().moveSpeed = originalSpeed;
                        enemy.GetComponent<EnemyScript>().isSlowed = false;
                    }
                }
            }
        }
    }
}
