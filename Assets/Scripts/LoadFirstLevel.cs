using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFirstLevel : MonoBehaviour
{
    
    public void OnButtonClick()
    {
        SceneManager.LoadScene("Transition");
    }

    
}
