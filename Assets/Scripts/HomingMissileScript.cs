using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileScript : MonoBehaviour
{
    private Rigidbody2D rb; //get references to everything
    public GameObject player;

    public float pullForce; //the strength of the force that pulls the missile to the player

    public bool missileEnabled;
    public float disableRange;

    private Vector2 baseVector;
    Vector2 dirVector; //direction vector 
    public float launchAngle = 0f; //angle of Launch
    public float launchVel = 10f;

    // Start is called before the first frame update
    void Start()
    {
        missileEnabled = true;
        baseVector = new Vector2(-pullForce, 0);
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>(); //finding instances of components
        dirVector = vecRotate(baseVector, launchAngle);
        rb.velocity = dirVector * launchVel;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10 || transform.position.y > 10 || transform.position.x < -10 || transform.position.x > 10)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate() //while there is time stored up for physics updates
    {
 
        //get distance along each axis to mouse pointer
        if (player != null && missileEnabled)
        {


            // Get direction from the sprite to the player, get the angle in degrees, and rotate the object to face player.
            Vector3 directionToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            angle -= 90;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            rb.AddForce(-vecRotate(baseVector, angle -270));
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if ( distance < disableRange)
        {
            missileEnabled = false;
            rb.drag = 0f;
        }
    }

    Vector2 vecRotate(Vector2 inVector, float angleDelta) // Rotates inVector and returns the rotated version
    {
        Vector2 outVector = Quaternion.AngleAxis(angleDelta, Vector3.forward) * inVector;
        return outVector;
    }
}
