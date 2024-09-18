using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{

    public GameObject bullet;
    public float bulletSpawnPos = 1;

    public float spawnCooldown = 1; 
    private float timeSinceLastSpawn = 0;



    void Start()
    {
        
    }

    void Update()
    {

        timeSinceLastSpawn += Time.deltaTime;


        if (Input.GetKey(KeyCode.N))
        {
            if(timeSinceLastSpawn > spawnCooldown)
            {
                Vector3 playerPosition = transform.position;

                Vector3 spawnPosition = playerPosition + new Vector3(0, bulletSpawnPos, 0);

                Instantiate(bullet, spawnPosition, Quaternion.identity);

                timeSinceLastSpawn = 0;
            }
            
        }
    }
}
