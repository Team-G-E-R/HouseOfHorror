using UnityEngine;

public class AudioSet : MonoBehaviour
{
    [Tooltip("Select the audio that will play at the beginning of the scene")]
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _needToBeLooped;
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
}
