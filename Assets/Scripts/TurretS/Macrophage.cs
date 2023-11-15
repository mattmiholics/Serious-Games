using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macrophage : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    public float turretRange = 5f;
    public float turretReload = 7f;
    public bool boosted = false;

    private float elapsedTime;
    private float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timer = Time.deltaTime;
        foreach (GameObject enemy in gameManager.enemiesList)
        {
            if (enemy)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= turretRange)
                {
                    elapsedTime += Time.deltaTime;
                    var speed = 10f;
                    var distance = Vector3.Distance(transform.position, enemy.transform.position);
                    var flightTime = distance / speed;
                    float completion = elapsedTime / flightTime;
                    if(timer < 0f)
                    {
                        enemy.transform.position = Vector3.Lerp(enemy.transform.position, transform.position, completion);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
