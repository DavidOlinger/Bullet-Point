using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public int hp;
    public float invulCooldownLength; 
    private float invulCooldown;
    public bool isInvulnerable;
    SpriteRenderer spriteRenderer;
    Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        invulCooldown = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
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


        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") && !isInvulnerable)
        {
            //Debug.Log("player Hit");
            hp--;
            invulCooldown = invulCooldownLength;
            isInvulnerable = true;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.1f);
        }
    }
}
