using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb;

    public float moveSpeed = 5;
    private Vector3 newMove;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        newMove = new Vector3(x, 0f, 0f).normalized;
    }


    private void FixedUpdate()
    {
        rb.velocity = newMove * moveSpeed;
    }

}
