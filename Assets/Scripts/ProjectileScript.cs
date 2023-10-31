using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed;
    public Transform target;
    public int damage = 1;

    private float completion;
    private float flightTime;
    private float elapsedTime;
    private float distance;

    private void Start()
    {
        distance = Vector3.Distance(transform.position, target.position);
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        flightTime = distance / speed;
        float completion = elapsedTime / flightTime;
        transform.position = Vector3.Lerp(transform.position, target.position, completion);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("john hit");
        collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        Destroy(gameObject);
    }
}