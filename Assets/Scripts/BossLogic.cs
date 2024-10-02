using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
    
    public float moveSpeed;

    public int numBullets;
    public float spread;
    public float direction;
    public int numWaves;
    public float waveDelay;
    public float waveOffset;
    public bool targetingPlayer = false;

    public int BSnumBullets;
    public float BSspread;
    public float BSdirection;
    public int BSnumWaves;
    public float BSwaveDelay;
    public float BSwaveOffset;

    public int MSnumBullets;
    public float MSspread;
    public float MSdirection;
    public int MSnumWaves;
    public float MSwaveDelay;
    public float MSwaveOffset;

    public GameObject bullet;
    Rigidbody2D rb;
    GameObject player;
    LevelManagerScript levelManager;
    public GameObject explosion;
    SpriteRenderer spriteRenderer;

    GameObject leftTurret;
    GameObject rightTurret;
    GameObject beamTurret;

    public float bulletSpawnPos = 1;
    public float spawnCooldown = 1;
    public bool isShooting;

    public float timeSinceLastSpawn = 0;

    public bool inVertPosition;
    public float VertPosition;



    public bool HoverMover;

    public int hitCounter = 0;
    public int maxHealth;
    public int scoreOnKill;
    public bool hovering;

    public float hoverx;
    public float hovery;
    public float hoverSpeed;
    public float hoverSpeedCap;
    public bool hoverSpeedDirx;
    public bool hoverSpeedDiry;
    public bool active;
    public float relx;
    public float rely;

    public bool beamOn;
    public bool missileOn;





    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManagerScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        leftTurret = GameObject.Find("BossMissilesL");
        rightTurret = GameObject.Find("BossMissilesR");
        beamTurret = GameObject.Find("BossBeam");
        beamOn = false;
        missileOn = false;
        StartCoroutine(MainBossLoop());
    }


    private void FixedUpdate()
    {

        // makes sure enemy is on screen before firing
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

        // different movement patterns
        if (inVertPosition && active)
        {
            

            if (!hovering) {
                rb.velocity = new Vector2(0, 0); 
                hovering = true;
            }
                
        }


        if (rb.transform.position.y < -5.5)
        {
            Destroy(gameObject);
        }



        if (targetingPlayer && player != null)
        {
            // Get direction from the sprite to the player, get the angle in degrees, and rotate the object to face player.
            Vector3 directionToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            angle += 90;
            direction = angle;

        }


        if (hovering)
        {

            if (hoverSpeedDirx)
            {
                rb.AddForce(new Vector2(hoverSpeed, 0));
                hoverx += hoverSpeed;
            }
            else
            {
                rb.AddForce(new Vector2(-hoverSpeed, 0));
                hoverx -= hoverSpeed;
            }
            if (hoverx > hoverSpeedCap)
            {
                hoverSpeedDirx = false;
            } else
            {
                if (hoverx < -hoverSpeedCap)
                {
                    hoverSpeedDirx = true;
                }
            }

            if (hoverSpeedDiry)
            {
                rb.AddForce(new Vector2(0, hoverSpeed));
                hovery += 2*hoverSpeed;
            }
            else
            {
                rb.AddForce(new Vector2(0, -hoverSpeed));
                hovery -= 2*hoverSpeed;
            }
            if (hovery > hoverSpeedCap)
            {
                hoverSpeedDiry = false;
            }
            else
            {
                if (hovery < -hoverSpeedCap)
                {
                    hoverSpeedDiry = true;
                }
            }

        }
    }




    //fires bullets
    IEnumerator FireBulletSpread(int numBullets, float spread, float direction, int numWaves, float waveDelay, float waveOffset) //this is special!
        //this is a coroutine, which means we can manage time with it. This is powerful but the implementation is a bit strange.
        //look up some documentation on it when you get the chance.
    {
        isShooting = true;
        Vector3 playerPosition = transform.position;
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
        //yield return new WaitForSeconds(0.1f);
        isShooting = false;
    }


    IEnumerator BossDeath() //Trigger the Bosses death and end the game.
    {
        yield return new WaitForSeconds(5f);
        spriteRenderer.color = new Color(0.35f, 0.35f, 0.35f, 35f);
        for (int i = 0; i < 25; i++) {
            yield return new WaitForSeconds(0.2f);
            relx = transform.position.x;
            rely = transform.position.y;
            float ranx = UnityEngine.Random.Range(-4f, 4f);
            float rany = UnityEngine.Random.Range(-2f, 2f);
            Instantiate(explosion, new Vector3(relx+ranx, rely+rany, -1), Quaternion.identity);
        }
        levelManager.music.Stop();
        rb.gravityScale = 0.1f;
        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(0.2f);
            relx = transform.position.x;
            rely = transform.position.y;
            float ranx = UnityEngine.Random.Range(-4f, 4f);
            float rany = UnityEngine.Random.Range(-2f, 2f);
            Instantiate(explosion, new Vector3(relx + ranx, rely + rany, -1), Quaternion.identity);
        }

        //Send player to win screen

        Destroy(gameObject);

        
    }

    IEnumerator MainBossLoop()
    {
        yield return new WaitForSeconds(5);
        //boss main attack starts (just a slow, sparse bullet field)
        BulletField();
        //wait 5ish seconds
        yield return new WaitForSeconds(5);
        //beam splitter attack
        BeamSplitterAttack();
        //wait
        yield return new WaitForSeconds(8);
        beamOn = false;

        //disable bullet field

        //boss homing missile attack
        MissileAttack();
        //wait
        yield return new WaitForSeconds(5);
        //different bullet spray
        ShotgunAttack();
        // wait
        yield return new WaitForSeconds(5);
        // beam splitter and homing missiles
        BeamSplitterAttack();
        MissileAttack();
        //repeat from top
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
                hitCounter = 0;
                StartCoroutine(BossDeath());
            }
        }
        
        
    }

    IEnumerator BeamSplitterAttackStart()
    {
        yield return new WaitForSeconds(0.1f);
        beamOn = true;
        while (beamOn) 
        {
            yield return new WaitForSeconds(0.05f);
            beamTurret.GetComponent<TriangleMoveAndShoot>().enemyAttack(BSnumBullets, BSspread, BSdirection, BSnumWaves, BSwaveDelay, BSwaveOffset);
        }
        
    }

    void BeamSplitterAttack()
    {
        StartCoroutine(BeamSplitterAttackStart());
    }

    void BulletField()
    {

    }

    void MissileAttack()
    {

    }

    void ShotgunAttack()
    {

    }


}
