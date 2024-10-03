using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerLogic : MonoBehaviour
{
    
    public float invulCooldownLength; 
    private float invulCooldown;
    public bool isInvulnerable;
    SpriteRenderer spriteRenderer;
    Color startColor;
    LevelManagerScript levelManager;
    public GameObject explosion;
    //public GameObject playerTrail;
    //public float playertraildelay;
    //private float playertrailcounter;

    //public float playerTrailYOff;
    //public float playerTrailxOff;



    AudioSource audioSource;

    public AudioClip dmgSound;





    // Start is called before the first frame update
    void Start()
    {
        invulCooldown = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManagerScript>();

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (invulCooldown <= 0) // turn off invulnerability when cooldown is up
        {
            isInvulnerable = false;
            spriteRenderer.color = startColor;
        }

        if (isInvulnerable) // if invulnerable
        { 
            invulCooldown -= Time.deltaTime; //count down to turning off invisibility
        }

        //playertrailcounter += Time.deltaTime;
        //if (playertrailcounter > playertraildelay)
        //{
        //    playertrailcounter = 0;
        //    Instantiate(playerTrail, new Vector3(transform.position.x + playerTrailxOff, transform.position.y + playerTrailYOff, transform.position.z + 1), Quaternion.identity);
        //    Instantiate(playerTrail, new Vector3(transform.position.x - playerTrailxOff, transform.position.y + playerTrailYOff, transform.position.z + 1), Quaternion.identity);
        //}
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") && !isInvulnerable)
        {
            levelManager.hp--;
            audioSource.PlayOneShot(dmgSound);

            if (levelManager.hp <= 0)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            invulCooldown = invulCooldownLength;
            isInvulnerable = true;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.1f);
        }

        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            levelManager.hp--;
            audioSource.PlayOneShot(dmgSound);

            if (levelManager.hp <= 0)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            invulCooldown = invulCooldownLength;
            isInvulnerable = true;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.1f);
        }
    }

    
}
