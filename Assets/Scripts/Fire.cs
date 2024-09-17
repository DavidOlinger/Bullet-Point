using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Transform gunPos;       
    public Transform playerPos;     

    public float minAngle = 0f;    
    public float maxAngle = 180f;  

    public GameObject bullet;  
    public float bulletSpeed = 5f; 

    Vector3 direction;
    float angle;

    void Update()
    {
        
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        
        direction = (mouseWorldPos - playerPos.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        gunPos.rotation = Quaternion.Euler(0f, 0f, angle - 90);


        if (Input.GetMouseButtonDown(0))
        {
            fireBullet();
        }
    }






    void fireBullet()
    {
        GameObject newBullet = Instantiate(bullet, gunPos.position, Quaternion.identity);

        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

        
        newBullet.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);

        

        rb.velocity = direction * bulletSpeed;  //NEED TO FIX BUG WHEN AIMINGIN DOWN
    }
}
