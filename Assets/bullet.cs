using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    Transform bulletPos;
    // Start is called before the first frame update
    void Start()
    {
        bulletPos = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletPos.position.x > 10 || (bulletPos.position.x < -10)){
            Destroy(gameObject);
        }
        if (bulletPos.position.y > 10 || (bulletPos.position.y < -10))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
