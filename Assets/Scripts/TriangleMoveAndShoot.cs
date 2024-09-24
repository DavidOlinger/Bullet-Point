using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMoveAndShoot : MonoBehaviour
{

    public float moveSpeed;
    Rigidbody2D rb;
    public int numBullets;
    public float spread;
    public float direction;
    public int numWaves;
    public float waveDelay;

    public GameObject bullet;
    public float bulletSpawnPos = 1;

    public float spawnCooldown = 1;
    private float timeSinceLastSpawn = 0;

    
    public bool SideToSideMover;
    public bool inVertPosition;
    public float VertPosition;
    public bool HoverMover;

    public int hitCounter = 0;
    public int maxHealth;

    


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        
        if (SideToSideMover)
        {
            if (rb.transform.position.x > 4.5 && moveSpeed > 0)
            {
                moveSpeed = moveSpeed * -1;
            }

            if (rb.transform.position.x < -4.5 && moveSpeed < 0)
            {
                moveSpeed = moveSpeed * -1;
            }
        }
        

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn > spawnCooldown && inVertPosition)
            {
                                //Vector3 playerPosition = transform.position;

                                //Vector3 spawnPosition = playerPosition + new Vector3(0, bulletSpawnPos, 0);

                                //Instantiate(bullet, spawnPosition, Quaternion.identity);
            StartCoroutine(FireBulletSpread(numBullets, spread, direction, numWaves, waveDelay));

                timeSinceLastSpawn = 0;
            }

    }


    private void FixedUpdate()
    {
        if (rb.position.y > VertPosition)
        {
            rb.velocity = new Vector2(0, -2);
        }
        else
        {
            inVertPosition = true;
        }

        if (inVertPosition)
        {
            if (SideToSideMover)
            {
                rb.velocity = new Vector2(moveSpeed, 0);
            }
            else if (HoverMover)
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        
    }

    IEnumerator FireBulletSpread(int numBullets, float spread, float direction, int numWaves, float waveDelay) //this is special!
        //this is a coroutine, which means we can manage time with it. This is powerful but the implementation is a bit strange.
        //look up some documentation on it when you get the chance.
    {
        Vector3 playerPosition = transform.position;

        Vector3 spawnPosition = playerPosition + new Vector3(0, bulletSpawnPos, 0);
        direction += 180; //FIXME
        for (int i = 0; i < numWaves; i++)
        {
            for (int j = 0; j < numBullets; j++)
            {
                float fireAngle = direction + spread * ((2f * j / (numBullets - 1)) - 1f);
                GameObject firedBullet = Instantiate(bullet, spawnPosition, Quaternion.identity);
                EnemyBulletScript fbScript = firedBullet.GetComponent<EnemyBulletScript>();
                if (fbScript != null)
                {
                    fbScript.forceAngle = fireAngle;
                } else
                {
                    Debug.Log("Failed to obtain bullet reference.");
                    yield break;
                }
            }
            yield return new WaitForSeconds(waveDelay);
        }
       
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerBullet"))
        {
            Destroy(collision.gameObject);
            hitCounter++;
            if (hitCounter >= maxHealth)
            {
                Destroy(gameObject);
            }
        }
        
        
    }



}
