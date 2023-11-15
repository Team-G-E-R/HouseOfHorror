using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BootstrapSystem : MonoBehaviour
{
    private AudioSource _audioSource;
    private GameObject _menuObj;
    private float _soundVolume;
    private Settings _settings; 

    private void Awake()
    {
        AudioCreate();
        MenuLoad();
        DataGet();
    }

    private void AudioCreate()
    {
        GameObject audio = new GameObject();
        audio.name = "AudioService";
        audio.tag = "Audio";
        _audioSource = audio.AddComponent<AudioSource>();
        
        GetComponent<AudioSystem>().AudioSourceSet();
        
        DontDestroyOnLoad(_audioSource);
    }

    private void MenuLoad()
    {
        _menuObj = Instantiate(Resources.Load<GameObject>("Menu/Menu"));
    }

    private void DataGet()
    {
        _settings = JsonUtility.FromJson<Settings>(File.ReadAllText(Application.streamingAssetsPath + "/settings.json"));
        _soundVolume = _settings.Volume;
        _menuObj.GetComponentInChildren<Slider>().value = _soundVolume;
        _audioSource.volume = _soundVolume;
    }
    
    [System.Serializable]
    public class Settings
    {
        public float Volume;
    }
}
