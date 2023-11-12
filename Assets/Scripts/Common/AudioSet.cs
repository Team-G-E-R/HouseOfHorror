using GameResources.SO;
using MrPaganini.UI.Windows;
using UnityEngine;

public class AudioSet : MonoBehaviour
{
    [Tooltip("Select the audio that will play at the beginning of the scene")]
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _needToBeLooped;
    private AudioSource _audioSource;

    private void Awake()
    {
       
    }

    private void Start()
    {
        //_audioSource = FindObjectOfType<AudioSource>().GetComponent<AudioSource>();
        _audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            transform.gameObject.tag = "Audio";
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
        else if (GameObject.FindWithTag("Audio").GetComponent<AudioSource>() != null)
        {
            _audioSource.clip = _audioClip;
            _audioSource.Play();
            if (_needToBeLooped)
            {
                _audioSource.loop = true;   
            }   
        }
    }
}
