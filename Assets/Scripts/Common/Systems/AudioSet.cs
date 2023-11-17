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
    private Data _data;

    private void Awake()
    {
       FindObjs();
       MusicSet();
       ScreamerSet();
    }

    private void FindObjs()
    {
        if (GameObject.FindWithTag("Audio") == null)
        {
            GameObject NewData = new GameObject();
            NewData.name = "Data";
            NewData.tag = "Data";
            _data = NewData.AddComponent<Data>();
            _data.Load();
        }
        else _data = GameObject.FindWithTag("Data").GetComponent<Data>();
        if (GameObject.FindWithTag("Audio") == null)
        { 
            _audioSource = gameObject.
                AddComponent<AudioSource>().GetComponent<AudioSource>();
            tag = "Audio";
        }
        else _audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
    }

    private void MusicSet()
    {
        _audioSource.volume = _data.GameData.Volume;
        _audioSource.clip = _audioClip;
        if (_needToBeLooped) _audioSource.loop = true;
        _audioSource.Play();
    }

    public void ScreamerStop()
    {
        _audioSource2.Stop();
    }

    private void ScreamerSet()
    {
        if (_needScreamer) ScreamerPlay();
    }

    public void ScreamerPlay()
    {
        _audioSource2 = gameObject.AddComponent<AudioSource>();
        _audioSource2.clip = _screamerToPlay;
        _audioSource2.volume = _data.GameData.Volume;
        if (_screamerMustBeLooped) _audioSource2.loop = true;
        _audioSource2.Play();
    }
}
