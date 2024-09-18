using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    Transform t;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
    }


    private void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speedX, speedY);
    }
}
