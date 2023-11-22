using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macrophage : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject turretProjectile;
    [SerializeField]
    private float projectileSpeed = 20f;

    public DamageType turretDamageType;

    public float turretRange = 5f;
    public float turretReload = 7f;
    public bool boosted = false;

    private float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timer -= Time.deltaTime;
        foreach (GameObject enemy in gameManager.enemiesList)
        {
            if (enemy)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= turretRange)
                {
                    turretFiring(enemy);
                }
                else if ((Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, enemy.transform.position))
                    && enemy.GetComponent<EnemyScript>().hitPoints > enemy.GetComponent<EnemyScript>().hitPoints)
                {
                    turretFiring(enemy);
                }
            }
        }
    }

    void turretFiring(GameObject turretTarget)
    {
        if (turretTarget != null)
        {
            Vector3 direction = turretTarget.transform.position - transform.position;

            float RotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, RotationZ);

            if (timer < 0f)
            {
                var projectile = Instantiate(turretProjectile, transform.position, transform.rotation);
                ProjectileScript projectileScript = projectile.GetComponent<ProjectileScript>();

                projectileScript.target = turretTarget.transform;

                projectileScript.damage = turretTarget.GetComponent<EnemyScript>().hitPoints;
                projectileScript.speed = projectileSpeed;
                projectileScript.damageType = turretDamageType;

                timer = turretReload;
            }
        }
    }
}
