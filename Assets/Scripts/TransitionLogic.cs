using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TransitionLogic : MonoBehaviour
{

    public TMP_Text levelText;

    private dontDeleteManager ddm;

    Scene currScene;





    // Start is called before the first frame update
    void Start()
    {

        

        ddm = GameObject.FindGameObjectWithTag("dontDelete").GetComponent<dontDeleteManager>();
        ddm.nextLevel++;




        

        if(ddm.nextLevel == 4)
        {
            levelText.SetText("Final Boss");
            Invoke("loader", 4);

        }
        else
        {
            levelText.SetText("Level " + ddm.nextLevel);
            Invoke("loader", 4);
        }
        

        
        

    }

    void loader()
    {
        SceneManager.LoadSceneAsync(ddm.nextLevel);
    }
}
