using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMoveAndShoot : MonoBehaviour
{

    public float moveSpeed;
   
    public int numBullets;
    public float spread;
    public float direction;
    public int numWaves;
    public float waveDelay;
    public float waveOffset;
    public bool targetingPlayer = false;
    private Vector2 swarmDirection;
    public float swarmDistance;

    public GameObject bullet;
    Rigidbody2D rb;
    GameObject player;
    LevelManagerScript levelManager;
    public GameObject explosion;

    public float bulletSpawnPos = 1;
    public float spawnCooldown = 1;

    private float timeSinceLastSpawn = 0;

    private float timeActive = 0;
    public float minShootTime;
    public float maxShootTime;
    private float timeToShoot;

    public bool inVertPosition;
    public float VertPosition;


    public bool SideToSideMover;
    public bool HoverMover;
    public bool flyByeMover;
    public bool TurretMover;
    public bool SwarmMoover;

    public int hitCounter = 0;
    public int maxHealth;
    public int scoreOnKill;

    private manageEnemiesInWave managingWave;
    public bool active;

    private dontDeleteManager ddm;





    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManagerScript>();
        managingWave = GetComponentInParent<manageEnemiesInWave>();
        ddm = GameObject.FindGameObjectWithTag("dontDelete").GetComponent<dontDeleteManager>();

        timeToShoot = UnityEngine.Random.Range(minShootTime, maxShootTime);




    }


    private void Update()
    {
        //moves back and forth across screen
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
        if (targetingPlayer && (player != null)) //add option for a bullet to spawn with its rotation set to point at the player.
        {
            Vector3 directionToTarget = transform.position - player.transform.position;
            direction = -Vector3.Angle(transform.up, directionToTarget);
            if (player.transform.position.x > transform.position.x)
            {
                direction = -direction;
            }
            
        }
        if (flyByeMover)
        {
            if (rb.transform.position.x < -7 || rb.transform.position.x > 7)
            {
                managingWave.enemyDied();
                Destroy(gameObject);
            }
        }
        if (targetingPlayer && (player != null)) //add option for a bullet to spawn with its rotation set to point at the player.
        {
            Vector3 directionToTarget = transform.position - player.transform.position;
            direction = -Vector3.Angle(transform.up, directionToTarget);
            if (player.transform.position.x > transform.position.x)
            {
                direction = -direction;
            }

        }
        if (SwarmMoover && (player != null)) //add option for a bullet to spawn with its rotation set to point at the player.
        {
            swarmDirection = player.transform.position - transform.position;
            swarmDirection.Normalize();
        }
        if (rb.transform.position.y < -6.5)
        {
            managingWave.enemyDied();
            Destroy(gameObject);
        }

        timeSinceLastSpawn += Time.deltaTime;

        if (active)
        {
            timeActive += Time.deltaTime;
        }

        if (timeSinceLastSpawn > spawnCooldown && timeActive > timeToShoot && rb.position.y <= 5)
            {
            StartCoroutine(FireBulletSpread(numBullets, spread, direction, numWaves, waveDelay, waveOffset));
            timeSinceLastSpawn = 0;
            }

    }


    private void FixedUpdate()
    {
        if (rb.position.y > VertPosition)
        {
            if (active)
            {
                rb.velocity = new Vector2(0, -moveSpeed);
            }
        }
        else
        {
            inVertPosition = true;
        }

        if (inVertPosition && active)
        {
            if (SideToSideMover)
            {
                rb.velocity = new Vector2(moveSpeed, 0);
            }
            else if (HoverMover)
            {
                rb.velocity = new Vector2(0, 0);
                
            }
            else if(flyByeMover)
                {
                rb.velocity = new Vector2(moveSpeed, 0);
            }
            else if (TurretMover)
            {
                rb.velocity = new Vector2(0, -moveSpeed);
            }
            else if (SwarmMoover)
            {
                if(Vector2.Distance(transform.position, player.transform.position) > swarmDistance) {
                    rb.velocity = new Vector2(moveSpeed, moveSpeed) * swarmDirection;
                }
                else
                {
                    HoverMover = true;
                }
            }
        }
        
    }




    //fires bullets
    IEnumerator FireBulletSpread(int numBullets, float spread, float direction, int numWaves, float waveDelay, float waveOffset) //this is special!
        //this is a coroutine, which means we can manage time with it. This is powerful but the implementation is a bit strange.
        //look up some documentation on it when you get the chance.
    {
        Vector3 playerPosition = transform.position;
        if (numBullets > 1) { numBullets++; } //FIXME?
        Vector3 spawnPosition = playerPosition + new Vector3(0, bulletSpawnPos, 0);
        direction += 180; //FIXME
        for (int i = 0; i < numWaves; i++)
        {
            for (int j = 0; j < numBullets; j++)
            {
                float fireAngle;
                if (numBullets <= 1) { fireAngle = direction; //if the code in the else statement is run and numBullets = 1,
                                                                    // it divides by zero.
                } else
                { fireAngle = direction + waveOffset * (i-1) + spread * ((2f * j / (numBullets - 1)) - 1f);
                }
                
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




    //take damage when hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerBullet"))
        {
            Destroy(collision.gameObject);
            hitCounter++;
            if (hitCounter >= maxHealth)
            {
                managingWave.enemyDied();

                ddm.score += scoreOnKill;
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        
        
    }




}
