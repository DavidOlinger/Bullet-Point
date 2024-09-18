using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletFire : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rb;

    public float timeToDie;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeToDie);
    }


    private void Update()
    {
        
        rb.velocity = new Vector2(0, moveSpeed);




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
