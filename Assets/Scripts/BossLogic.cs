using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
    public bool bossLoopActive;

    public float moveSpeed;

    public float bulletFieldDelay, beamDelay, missileDelay, shotgunDelay;

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

    public int SGnumBullets, SGnumWaves;
    public float SGspread, SGdirection, SGwaveDelay, SGwaveOffset;


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
    public bool bulletOn;
    public bool missileOn;
    public bool shotgunOn;





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
                bossLoopActive = true;
                StartCoroutine(MainBossLoop());
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
        bossLoopActive = false;
        yield return new WaitForSeconds(3f);
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
        while (bossLoopActive)
        {
            //boss main attack starts (just a slow, sparse bullet field)
            StartCoroutine(BulletField());
            //beam splitter attack
            StartCoroutine(BeamSplitterAttackStart());

            yield return new WaitForSeconds(9);

            //turn off beam splitter
            bulletOn = false;
            //turn off bulle
            beamOn = false;

            yield return new WaitForSeconds(5f);


            //start missile attack
            StartCoroutine(MissileAttack());

            //wait
            yield return new WaitForSeconds(9f);

            //turn off missile attack
            missileOn = false;

            //wait


            //different bullet spray
            StartCoroutine(BulletField());
            yield return new WaitForSeconds(3);
            StartCoroutine(ShotgunAttack());
            yield return new WaitForSeconds(9);

            shotgunOn = false;    
            bulletOn = false;


            Debug.Log("GAP");
            yield return new WaitForSeconds(5);

            // beam splitter and homing missiles
            StartCoroutine(BeamSplitterAttackStart());
            StartCoroutine(MissileAttack());
            yield return new WaitForSeconds(9f);

            missileOn = false;
            beamOn = false;

            yield return new WaitForSeconds(3f);

            StartCoroutine(BulletField());

            StartCoroutine(ShotgunAttack());
            yield return new WaitForSeconds(6f);

            shotgunOn= false;
            yield return new WaitForSeconds(4f);

            StartCoroutine(BeamSplitterAttackStart());
            StartCoroutine(MissileAttack());
            yield return new WaitForSeconds(12f);

            missileOn = false;
            beamOn = false;
            bulletOn= false;


            //repeat from top
            yield return new WaitForSeconds(3f);
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
                hitCounter = 0;
                StartCoroutine(BossDeath());
            }
        }
        
        
    }

    IEnumerator BeamSplitterAttackStart()
    {
        beamOn = true;
        while (beamOn && bossLoopActive) 
        {
            yield return new WaitForSeconds(beamDelay);
            beamTurret.GetComponent<TriangleMoveAndShoot>().enemyAttack(BSnumBullets, BSspread, BSnumWaves, BSwaveDelay, BSwaveOffset);
        }
        
    }



    IEnumerator BulletField()
    {
        bulletOn = true;
        while (bulletOn && bossLoopActive)
        {
            Debug.Log("BULLET FIELD");
            yield return new WaitForSeconds(bulletFieldDelay);
            StartCoroutine(FireBulletSpread(numBullets, spread, direction, numWaves, waveDelay, waveOffset));
        }
        
    }

    IEnumerator MissileAttack()
    {
        yield return new WaitForSeconds(0.1f);
        missileOn = true;
        while (missileOn && bossLoopActive)
        {
            yield return new WaitForSeconds(missileDelay);
            leftTurret.GetComponent<TriangleMoveAndShoot>().enemyAttack(MSnumBullets, MSspread, MSnumWaves, MSwaveDelay, MSwaveOffset);
            rightTurret.GetComponent<TriangleMoveAndShoot>().enemyAttack(MSnumBullets, MSspread, MSnumWaves, MSwaveDelay, MSwaveOffset);
        }
    }

    IEnumerator ShotgunAttack()
    {
        yield return new WaitForSeconds(0.1f);
        shotgunOn = true;
        while (shotgunOn && bossLoopActive)
        {
            Debug.Log("ShotGUN");
            yield return new WaitForSeconds(shotgunDelay);
            StartCoroutine(FireBulletSpread(SGnumBullets, SGspread, SGdirection, SGnumWaves, SGwaveDelay, SGwaveOffset));
        }
    }


}
