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

    public DamageType turretDamageType;

    public bool boosted = false;
    public float turretRange = 10f;
    public float turretReload = 4f;


    private float timer = 0f;
    // Start is called before the first frame update
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, turretRange);
    }
#endif
    private void Start()
    {
        gameManager = GameManager.main;
    }
    // Update is called once per frame
    void Update()
    {
        var child = transform.Find("Boosted").gameObject;
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
        if (child)
        {
            child.transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y - 0.2f, transform.position.z);
            child.transform.rotation = Quaternion.Euler(0,0,0);
            if (boosted)
            {
                child.SetActive(true);
            }
            else
            {
                child.SetActive(false);
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
                ProjectileScript projectileScript = projectile.GetComponent<ProjectileScript>();

                projectileScript.target = turretTarget.transform;

                projectileScript.speed = projectileSpeed;
                projectileScript.damageType = turretDamageType;

                timer = turretReload;
            }
        }
    }
}
