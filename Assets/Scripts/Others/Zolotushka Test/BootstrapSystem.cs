using UnityEngine;

public class BootstrapSystem : MonoBehaviour
{
    public AudioClip MenuMusic;
    
    private AudioSource _audioSource;
    private float _soundVolume;
    private SavedDataScript _dataScript;

    private void Awake()
    {
        DataCreate();
        AudioCreate();
        MenuLoad();
    }

    private void DataCreate()
    {
        GameObject _data = new GameObject();
        _data.name = "GameData";
        _data.tag = "Data";
        _dataScript = _data.AddComponent<SavedDataScript>();
        _dataScript.Load();
        _soundVolume = _dataScript._data.Volume;
        DontDestroyOnLoad(_data);
    }
    
    private void AudioCreate()
    {
        GameObject audio = new GameObject();
        audio.name = "AudioService";
        audio.tag = "Audio";
        _audioSource = audio.AddComponent<AudioSource>();
        
        AudioSystemSet();
        DontDestroyOnLoad(_audioSource);
    }
    
    private void MenuLoad()
    {
        var _menuObj = Instantiate(Resources.Load<GameObject>("Menu/Menu"));
    }

    private void AudioSystemSet()
    {
        _audioSource.clip = MenuMusic;
        _audioSource.loop = true;
        _audioSource.volume = _soundVolume;
        _audioSource.Play();
    }
}
