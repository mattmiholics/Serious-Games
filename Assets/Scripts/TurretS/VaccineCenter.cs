using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccineCenter : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    public float vaccineDuration = 10;
    public int pointsRequired = 50;

    private float originalSpeed;
    // Update is called once per frame
    void Update()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        vaccineDuration -= Time.deltaTime;
        if(vaccineDuration < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyScript>() && collision.tag == "Virus")
        {
            originalSpeed = collision.GetComponent<EnemyScript>().moveSpeed;
            collision.GetComponent<EnemyScript>().moveSpeed = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyScript>() && collision.tag == "Virus")
        {
            collision.GetComponent<EnemyScript>().moveSpeed = originalSpeed;
        }
    }
}
