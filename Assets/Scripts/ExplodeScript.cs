using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{
    public float explosionTime;
    private float explosionCountdown;
    public float explosionFalloff;
    public float explosionSpeed;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        explosionCountdown = explosionTime;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void FixedUpdate()
    {
        if (explosionCountdown <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            explosionCountdown -= (1f / 50f);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, explosionCountdown / explosionTime);
            explosionSpeed *= explosionFalloff;
            transform.localScale += new Vector3(explosionSpeed, explosionSpeed, 1);
        }
    }
}
