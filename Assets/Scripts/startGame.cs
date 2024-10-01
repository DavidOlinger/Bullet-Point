using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class startGame : MonoBehaviour
{

    public bool Return = false;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Return)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene("Transition");

            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Return == false)
            {
                SceneManager.LoadScene("Controls");
            }
        }



    }



}
