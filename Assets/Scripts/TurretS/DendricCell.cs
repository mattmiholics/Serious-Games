using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DendricCell : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    public float boostTimer = 8f;
    public float turretReload = 10f;
    public float turretRange = 5f;
    public int pointsRequired = 50;

    private float reloadTimer = 0f;
    private float activeTimer = 0f;
    private bool active = false;
    // Update is called once per frame
    void Update()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        reloadTimer = Time.deltaTime;

        foreach (GameObject enemy in gameManager.enemiesList)
        {
            if (enemy)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= turretRange)
                {
                    if (reloadTimer >= 0)
                    {
                        active = false;
                    }
                    else if (reloadTimer < 0 && !active)
                    {
                        activeTimer = boostTimer;
                        active = true;
                    }
                    foreach (GameObject tower in gameManager.towersList)
                    {
                        var tempReload = 5f;
                        if (Vector3.Distance(transform.position, tower.transform.position) <= turretRange && active)
                        {
                            if (tower.GetComponent<TurretProto>())
                            {
                                if (!tower.GetComponent<TurretProto>().boosted)
                                {
                                    tempReload = tower.GetComponent<TurretProto>().turretReload;
                                    tower.GetComponent<TurretProto>().turretReload *= 0.6f;
                                }
                            }
                            else if (tower.GetComponent<Macrophage>())
                            {
                                if (!tower.GetComponent<Macrophage>().boosted)
                                {
                                    tempReload = tower.GetComponent<Macrophage>().turretReload;
                                    tower.GetComponent<Macrophage>().turretReload *= 0.6f;
                                }
                            }
                        }
                        else
                        {
                            if (tower.GetComponent<TurretProto>())
                            {
                                if (tower.GetComponent<TurretProto>().boosted)
                                {
                                    tower.GetComponent<TurretProto>().turretReload = tempReload;
                                }
                            }
                            else if (tower.GetComponent<Macrophage>())
                            {
                                if (tower.GetComponent<Macrophage>().boosted)
                                {
                                    tower.GetComponent<Macrophage>().turretReload = tempReload;
                                }
                            }
                        }
                    }
                }
            }
        }
        while (active)
        {
            activeTimer -= Time.deltaTime;
            if(activeTimer < 0)
            {
                active = false;
                reloadTimer = turretReload;
            }
        }
    }
}
