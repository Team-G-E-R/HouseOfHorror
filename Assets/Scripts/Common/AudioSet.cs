using UnityEngine;

public class AudioSet : MonoBehaviour
{
    [Header("Main audio settings")]
    [Tooltip("Select the audio that will play at the beginning of the scene")]
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _needToBeLooped;
    [Space(15)]
    [Header("Additional")]
    [SerializeField] private bool _needScreamer;
    [SerializeField] private bool _screamerMustBeLooped;
    [Tooltip("Will work only if upper bool true")]
    [SerializeField] private AudioClip _screamerToPlay;
    
    private AudioSource _audioSource;
    private AudioSource _audioSource2;

    private void Start()
    {
       FindAudio();
    }

    private void FindAudio()
    {
        if (_audioSource == null && AllServices.Singleton == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            MusicSet();
        }
        else
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
        if (_needScreamer && _screamerMustBeLooped)
        {
           _audioSource2 = gameObject.AddComponent<AudioSource>();
           _audioSource2.loop = true;
           _audioSource2.clip = _screamerToPlay;
           _audioSource2.Play();
        }
        else
        {
            _audioSource2 = gameObject.AddComponent<AudioSource>();
            _audioSource2.clip = _screamerToPlay;
            _audioSource2.Play();  
        }
    }

    public void ScreamerStop()
    {
        _audioSource2.Stop();
    }
}
