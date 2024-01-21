using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioContinue : MonoBehaviour
{
    public AudioClip AudioClip;
    public AudioSource AudioSource;
    
    void Start()
    {
        AudioSource.clip = AudioClip;
        AudioSource.Play();    
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            Debug.Log(AudioSource.time);
            AudioSource.Stop();
        }
    }
}
