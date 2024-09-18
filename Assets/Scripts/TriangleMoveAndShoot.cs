using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMoveAndShoot : MonoBehaviour
{

    public float moveSpeed;
    Rigidbody2D rb;

    public GameObject bullet;
    public float bulletSpawnPos = 1;

    public float spawnCooldown = 1;
    private float timeSinceLastSpawn = 0;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if(rb.transform.position.x > 8.5 && moveSpeed > 0)
        {
            moveSpeed = moveSpeed * -1;
        }

        if (rb.transform.position.x < -8.5 && moveSpeed < 0)
        {
            moveSpeed = moveSpeed * -1;
        }

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn > spawnCooldown)
            {
                Vector3 playerPosition = transform.position;

                Vector3 spawnPosition = playerPosition + new Vector3(0, bulletSpawnPos, 0);

                Instantiate(bullet, spawnPosition, Quaternion.identity);

                timeSinceLastSpawn = 0;
            }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveSpeed, 0);
    }
}
