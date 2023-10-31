using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TurretProto : MonoBehaviour
{
    [SerializeField] 
    private GameObject turretProjectile;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private float projectileSpeed = 20f;

    public float turretRange = 10f;
    public float turretReload = 4f;

    private float timer = 0f;
    // Start is called before the first frame update

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, turretRange);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        foreach (GameObject enemy in gameManager.enemiesList)
        {
            if (enemy) 
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= turretRange)
                {
                    turretFiring(enemy);
                }
                else if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, enemy.transform.position))
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

            transform.rotation = Quaternion.Euler(0,0,RotationZ);

            if (timer < 0f)
            {
                var projectile = Instantiate(turretProjectile, transform.position, transform.rotation);

                projectile.GetComponent<ProjectileScript>().target = turretTarget.transform;

                projectile.GetComponent<ProjectileScript>().speed = projectileSpeed;

                timer = turretReload;
            }
        }
    }
}
