using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text HPText;
    public int hp;


    private dontDeleteManager ddm;

    public AudioSource music;


    void Start()
    {

        ddm = GameObject.FindGameObjectWithTag("dontDelete").GetComponent<dontDeleteManager>();

        music = GetComponent<AudioSource>();


    }





    void Update()
    {
        scoreText.SetText(ddm.score+ " ");
        HPText.SetText("HP: " + hp);

        if(hp == 0)
        {
            music.mute = true;
            Invoke("loadDeathScreen", 3);
        }
    }



    void loadDeathScreen()
    {
        SceneManager.LoadSceneAsync(5);
    }






}


