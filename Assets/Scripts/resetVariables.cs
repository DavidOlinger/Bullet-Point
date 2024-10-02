using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetVariables : MonoBehaviour
{

    private dontDeleteManager ddm;

    void Start()
    {

        ddm = GameObject.FindGameObjectWithTag("dontDelete").GetComponent<dontDeleteManager>();



        ddm.score = 0;
        ddm.nextLevel = 0;
    }

    

}
