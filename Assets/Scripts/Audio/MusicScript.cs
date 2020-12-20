using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource musicAudioSource;
    void Start()
    {
        if(musicAudioSource)
        {
            musicAudioSource.Play();
        }
    }

    // Update is called once per frame
}
