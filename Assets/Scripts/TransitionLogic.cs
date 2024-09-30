using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TransitionLogic : MonoBehaviour
{

    public TMP_Text levelText;

    private dontDeleteManager ddm;






    // Start is called before the first frame update
    void Start()
    {

        

        ddm = GameObject.FindGameObjectWithTag("dontDelete").GetComponent<dontDeleteManager>();
        ddm.nextLevel++;


        

        


        levelText.SetText("Level " + ddm.nextLevel);
        Invoke("loader", 3);

        
        

    }

    void loader()
    {
        SceneManager.LoadSceneAsync(ddm.nextLevel);
    }
}
