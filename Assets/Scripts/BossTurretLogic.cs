using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurretLogic : MonoBehaviour
{
    public GameObject boss;
    public float relx;
    public float rely;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(boss.transform.position);
        transform.position = new Vector3(boss.transform.position.x + relx, boss.transform.position.y + rely, transform.position.z);
    }
}
