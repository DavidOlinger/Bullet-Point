using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TransitionLogic : MonoBehaviour
{

    public TMP_Text levelText;
    private static int nextLevel = 0;



    // Start is called before the first frame update
    void Start()
    {
        nextLevel++;

        levelText.SetText("Level " + nextLevel);

        Invoke("loader", 3);


    }

    void loader()
    {
        SceneManager.LoadSceneAsync(nextLevel);
    }
}
