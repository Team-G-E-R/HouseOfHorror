using UnityEngine;

public class BootstrapSystem : MonoBehaviour
{
    [SerializeField] private AudioClip _menuMusic;
    
    private AudioSource _audioSource;
    private float _soundVolume;
    private Data _data;
    private const string DataName = "GameData";
    private const string AudioName = "AudioService";

    private void Awake()
    { 
        InitializeData();
        AudioCreate();
        MenuLoad();
    }

    private void InitializeData()
    {
        GameObject gameDataFile = new GameObject();
        gameDataFile.name = DataName;
        gameDataFile.tag = "Data";
        _data = gameDataFile.AddComponent<Data>();
        _data.Load();
        _soundVolume = _data.GameData.Volume;
        DontDestroyOnLoad(_data);
    }
    
    private void AudioCreate()
    {
        GameObject audioFile = new GameObject();
        audioFile.name = AudioName;
        audioFile.tag = "Audio";
        _audioSource = audioFile.AddComponent<AudioSource>();
        
        AudioSystemSet();
        DontDestroyOnLoad(_audioSource);
    }
    
    private void MenuLoad()
    {
        var _menuObj = Instantiate(Resources.Load<GameObject>("Menu/Menu"));
    }

    private void AudioSystemSet()
    {
        _audioSource.clip = _menuMusic;
        _audioSource.loop = true;
        _audioSource.volume = _soundVolume;
        _audioSource.Play();
    }
}
