using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCellTurret : MonoBehaviour
{
    [SerializeField]private GameManager gameManager;

    public float turretRange = 10f;

    private void Update()
    {
        foreach (GameObject tower in gameManager.towersList)
        {
            if (tower)
            {
                if (Vector3.Distance(transform.position, tower.transform.position) <= turretRange)
                {
                    if(tower.GetComponent<TurretProto>())
                    {
                        if (!tower.GetComponent<TurretProto>().boosted)
                        {
                            tower.GetComponent<TurretProto>().turretRange *= 1.1f;
                            tower.GetComponent<TurretProto>().turretReload *= 0.75f;
                            tower.GetComponent<TurretProto>().boosted = true;
                        }
                    }
                }
            }
        }
    }
}
