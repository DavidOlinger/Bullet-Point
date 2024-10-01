using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{

    public GameObject bullet;
    public float bulletSpawnPos = 1;

    public float spawnCooldown = 1; 
    private float timeSinceLastSpawn = 0;

    AudioSource audioSource;

    public AudioClip shootSound;

    public bool isShooting;
    




    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {

        timeSinceLastSpawn += Time.deltaTime;


        if (Input.GetKey(KeyCode.Space)) //Could def replace with inputManager
        {
            isShooting = true;

            if(timeSinceLastSpawn > spawnCooldown)
            {
                Vector3 playerPosition = transform.position;

                Vector3 spawnPosition = playerPosition + new Vector3(0, bulletSpawnPos, 0);

                Instantiate(bullet, spawnPosition, Quaternion.identity);

                audioSource.PlayOneShot(shootSound);


                timeSinceLastSpawn = 0;
            }
            
        }
        else
        {
            isShooting = false;
        }
    }


}

