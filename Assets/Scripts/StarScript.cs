using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject nextSlide;
    public float starSpeed;
    public float spawnNextHeight;
    public float destroyHeight;
    public float yOffset;
    private bool _nextSpawned;
    // Start is called before the first frame update
    void Start()
    {
        _nextSpawned = false;
        rb = GetComponent<Rigidbody2D>();

        gameObject.name = "Stars";
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < spawnNextHeight && !_nextSpawned)
        {
            Instantiate(nextSlide, new Vector3(transform.position.x, transform.position.y + yOffset), Quaternion.identity);
            _nextSpawned |= true;

        } else if (transform.position.y < destroyHeight)
        {
            Destroy(gameObject);
        } else 
        {
            rb.velocity = new Vector2(0, -starSpeed);
        }
    }
}
