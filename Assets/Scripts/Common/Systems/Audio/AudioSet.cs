using UnityEngine;

public class AudioSet : Finder
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
    
    private AudioSource _audioSource2;

    private void Awake()
    {
       FindObjs();
       MusicSet();
       ScreamerSet();
    }

    private void MusicSet()
    {
        AudioSourceObj.volume = SettingsDataobj.GameSettingsData.Volume;
        AudioSourceObj.clip = _audioClip;
        if (_needToBeLooped) AudioSourceObj.loop = true;
        AudioSourceObj.Play();
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
        GameObject Screamer = new GameObject();
        Screamer.name = "Screamer";
        Screamer.tag = "Audio";
        _audioSource2 = Screamer.AddComponent<AudioSource>();
        _audioSource2.clip = _screamerToPlay;
        _audioSource2.volume = SettingsDataobj.GameSettingsData.Volume;
        if (_screamerMustBeLooped) _audioSource2.loop = true;
        _audioSource2.Play();
    }
}
