using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private Rigidbody2D bulletBody;
    public float velCoef = 1; // initial velocity
    public float accCoef = 1; //change in velocity
    public float jerkCoef = 1; //change in acceleration

    private Vector2 baseVector; 
    Vector2 dirVector; //direction vector 
    public float forceAngle = 0f; //angle of movement
    public bool targetingPlayer = false;
    private GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        // fixedTimestep = 1f / updatesPerSecond; //setting starting values
        baseVector = new Vector2(0, 1);
        player = GameObject.FindWithTag("Player");

        if (targetingPlayer && (player != null)) //add option for a bullet to spawn with its rotation set to point at the player.
        { 
            Vector3 directionToTarget = transform.position - player.transform.position;
            forceAngle = Vector3.Angle(-transform.up, directionToTarget);
            if (player.transform.position.x > transform.position.x) 
            { 
                forceAngle = -forceAngle;
            }

            Debug.Log(transform.position - player.transform.position);
        }

        dirVector = vecRotate(baseVector, forceAngle);
        transform.Rotate(Vector3.forward, forceAngle);
        bulletBody = GetComponent<Rigidbody2D>(); //finding instances of components
        bulletBody.velocity = dirVector * velCoef;

        // add starting velocity
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10 || transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        bulletBody.velocity = dirVector * velCoef;
        velCoef += accCoef / 50; // accelerate the bullet
        accCoef += jerkCoef / 50; // Increase acceleration
    }

    Vector2 vecRotate(Vector2 inVector, float angleDelta) // Rotates inVector and returns the rotated version
    {
        Vector2 outVector = Quaternion.AngleAxis(angleDelta, Vector3.forward) * inVector;
        return outVector;
    }
}
