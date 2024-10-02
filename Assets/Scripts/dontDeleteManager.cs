using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class dontDeleteManager : MonoBehaviour
{


    public int nextLevel = 0;

    AudioSource audioSource;

    public int score = 0;


    private static dontDeleteManager instance;


    void Awake()
    {
        

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nextLevel = 0;
            SceneManager.LoadScene("Transition");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            nextLevel = 1;
            SceneManager.LoadScene("Transition");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            nextLevel = 2;
            SceneManager.LoadScene("Transition");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            nextLevel = 3;
            SceneManager.LoadScene("Transition");
        }

    }




}
