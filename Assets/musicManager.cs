using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
