using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class highScore : MonoBehaviour
{
    private dontDeleteManager ddm;
    public TMP_Text highScoreText;



    void Start()
    {
        ddm = GameObject.FindGameObjectWithTag("dontDelete").GetComponent<dontDeleteManager>();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            if(ddm.score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", ddm.score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", ddm.score);
        }

        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();



    }


}
