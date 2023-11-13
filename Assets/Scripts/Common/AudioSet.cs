using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class AudioSet : MonoBehaviour
{
    [Header("Main audio settings")]
    [Tooltip("Select the audio that will play at the beginning of the scene")]
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _needToBeLooped;
    [Space(15)]
    [Header("Additional")]
    [SerializeField] private bool _needScreamer;
    [Tooltip("Will work only if upper bool true")]
    [SerializeField] private AudioClip _screamerToPlay;
    
    private AudioSource _audioSource;

    private void Awake()
    {
       FindAudio();
    }

    private void FindAudio()
    {
        if (GameObject.FindWithTag("Audio") == null)
        {
            transform.gameObject.tag = "Audio";
            _audioSource = gameObject.AddComponent<AudioSource>();

            MusicSet();
        }
        else if (GameObject.FindWithTag("Audio") != null)
        {
            _audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
            MusicSet();
        }
    }

    private void MusicSet()
    {
        _audioSource.clip = _audioClip;
        _audioSource.Play();
        if (_needToBeLooped)
        {
            _audioSource.loop = true;   
        }   
    }

    public void ScreamerPlay()
    {
        if (_needScreamer)
        {
           var _audioSource2 = gameObject.AddComponent<AudioSource>();
           _audioSource2.clip = _screamerToPlay;
           _audioSource2.Play();
        }
    }
}
