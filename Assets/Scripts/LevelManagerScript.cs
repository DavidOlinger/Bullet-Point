using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text HPText;
    public int score;
    public int hp;



    void Start()
    {
        score = 0;
        
    }




    void Update()
    {
        scoreText.SetText(score+ " ");
        HPText.SetText("HP: " + hp);

        if(hp == 0)
        {
            Invoke("loadDeathScreen", 2);
        }
    }



    void loadDeathScreen()
    {
        SceneManager.LoadSceneAsync(5);
    }
}
