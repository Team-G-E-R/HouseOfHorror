using UnityEngine;
using System.IO;

public class AudioSystem : MonoBehaviour
{
    public AudioClip MenuMusic;
    private GameObject _audioObj;
    private AudioSource _audioSource;
    private float _soundVolume;

    public void AudioSourceSet()
    {
        _audioObj = GameObject.FindWithTag("Audio");
        _audioSource = _audioObj.GetComponent<AudioSource>();
        _audioSource.clip = MenuMusic;
        _audioSource.loop = true;
        _audioSource.Play();
    }
}
