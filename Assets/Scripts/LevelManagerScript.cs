using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManagerScript : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text HPText;
    public int score;
    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText(score+ " ");
        HPText.SetText("HP: " + hp);
    }
}
