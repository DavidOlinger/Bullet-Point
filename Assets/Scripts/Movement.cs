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
    public float playerBoundsX;
    public float playerBoundsY;

    Vector2 moveDirection = Vector2.zero;

    Transform t;

    ShootBullet shootScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
        shootScript = GetComponent<ShootBullet>();
    }


    private void Update()
    {
        if (shootScript.isShooting)
        {
            moveSpeed = 3;
        }
        else
        {
            moveSpeed = 4f;
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.x > playerBoundsX && moveDirection.x > 0)
        {
            moveDirection.x = 0;
        }
        if (transform.position.x < -playerBoundsX && moveDirection.x < 0)
        {
            moveDirection.x = 0;
        }

        if (transform.position.y > playerBoundsY && moveDirection.y > 0)
        {
            moveDirection.y = 0;
        }
        if (transform.position.y < -playerBoundsY && moveDirection.y < 0)
        {
            moveDirection.y = 0;
        }

        rb.velocity = moveDirection * moveSpeed;
    }

    void OnPlayerMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
        moveDirection.y = moveDirection.y * ySpeedScale;

    }
}
