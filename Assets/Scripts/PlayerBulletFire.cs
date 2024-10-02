using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletFire : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }


    private void Update()
    {
        
        rb.velocity = new Vector2(0, moveSpeed);

        if (transform.position.y > 5)
        {
            Destroy(gameObject);
        }


    }

    
}
