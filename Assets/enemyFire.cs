using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFire : MonoBehaviour
{
    public Transform enemyPos;


    public GameObject bullet;
    public float bulletSpeed = 5f;

    public Vector3 direction;

    public float interval = 3f;


    void Start()
    {
        InvokeRepeating("fireBullet", 1, interval);
    }

   






    void fireBullet()
    {
        GameObject newBullet = Instantiate(bullet, enemyPos.position, Quaternion.identity);

        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();


        rb.velocity = direction * bulletSpeed;  
    }
}
