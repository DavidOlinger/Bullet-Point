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

    public float updatesPerSecond = 50f; // How many times per second the force pull should occur
    private float dtAccumulator = 0f;    // to correct for low framerates. stores up delta time to apply force consistently
    private float fixedTimestep; //inverse of updates per second


    // Start is called before the first frame update
    void Start()
    {
        fixedTimestep = 1f / updatesPerSecond; //setting starting values
        baseVector = new Vector2(0, 1);
        dirVector = vecRotate(baseVector, forceAngle);
        bulletBody = GetComponent<Rigidbody2D>(); //finding instances of components

        bulletBody.AddForce(dirVector * velCoef); // add starting velocity
    }

    // Update is called once per frame
    void Update()
    {
        dtAccumulator += Time.deltaTime;
        accCoef += Time.deltaTime * jerkCoef; //jerk the acceleration
        while (dtAccumulator >= fixedTimestep) //while there is time stored up for physics updates
        {
            bulletBody.AddForce(dirVector * accCoef); // accelerate the bullet
            dtAccumulator -= fixedTimestep; //spend the stored up time
        }
    }

    Vector2 vecRotate(Vector2 inVector, float angleDelta) // Magical Math Machine
    {
        Vector2 outVector = Quaternion.AngleAxis(angleDelta, Vector3.forward) * inVector; 
        return outVector;
    }
}
