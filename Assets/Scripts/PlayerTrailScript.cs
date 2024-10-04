using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailScript : MonoBehaviour
{
    private Vector2 startpos;
    public float maxDist;
    public float tailSpeed;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        startpos = gameObject.transform.position;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity= new Vector2(0, -tailSpeed);
        //spriteRenderer.color = new Color(1, 1, 1, (1 / (Vector2.Distance(startpos, transform.position) / maxDist)) );
        if (Vector2.Distance(startpos, transform.position) > maxDist)
        {
            Destroy(gameObject);
        }
    }
}
