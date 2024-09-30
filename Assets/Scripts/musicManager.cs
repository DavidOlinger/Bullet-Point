using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{

    public AudioSource music;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        music = GetComponent<AudioSource>();
    }

    public void muteMusic()
    {
        music.mute = true;
    }
}
