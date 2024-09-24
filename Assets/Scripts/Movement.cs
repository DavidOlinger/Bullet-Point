using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{

    public float moveSpeed;
    public float ySpeedScale;
    Rigidbody2D rb;
    public float playerBounds;
    Vector2 moveDirection = Vector2.zero;

    Transform t;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
    }


    private void Update()
    {
      

    }

    private void FixedUpdate()
    {
        if (transform.position.x > playerBounds && moveDirection.x > 0)
        {
            moveDirection.x = 0;
        }
        if (transform.position.x < -playerBounds && moveDirection.x < 0)
        {
            moveDirection.x = 0;
        }
        rb.velocity = moveDirection * moveSpeed;
    }

    void OnPlayerMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
        moveDirection.y = moveDirection.y * ySpeedScale;

    }
}
