using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class deathScreenLoads : MonoBehaviour
{

    private dontDeleteManager ddm;

    private void Start()
    {
        ddm = GameObject.FindGameObjectWithTag("dontDelete").GetComponent<dontDeleteManager>();
        ddm.score = 0;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ddm.nextLevel--;
            SceneManager.LoadScene("Transition");
        }

        



    }



}
