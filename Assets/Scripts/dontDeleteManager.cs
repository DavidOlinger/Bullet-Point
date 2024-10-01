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

    




}
