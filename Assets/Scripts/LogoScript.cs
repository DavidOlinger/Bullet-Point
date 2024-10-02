using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScript : MonoBehaviour
{
    public float hovery;
    public float hoverSpeed;
    public float hoverSpeedCap;
    public bool hoverSpeedDiry;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (hoverSpeedDiry)
        {
            rb.AddForce(new Vector2(0, hoverSpeed));
            hovery += hoverSpeed;
        }
        else
        {
            rb.AddForce(new Vector2(0, -hoverSpeed));
            hovery -= hoverSpeed;
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
