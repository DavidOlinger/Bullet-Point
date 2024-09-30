using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class dontDeleteManager : MonoBehaviour
{


    public int nextLevel = 0;

    AudioSource audioSource;

    public int score = 0;

    Scene currScene;

    private static dontDeleteManager instance;


    void Awake()
    {
        currScene = SceneManager.GetActiveScene();


        if (currScene.name == "MainMenu")
        {
            score = 0;
            nextLevel = 0;
        }

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


    


}
