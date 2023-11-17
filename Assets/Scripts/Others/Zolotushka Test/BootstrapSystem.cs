using UnityEngine;

public class BootstrapSystem : Finder
{
    [SerializeField] private AudioClip _menuMusic;
    
    private Data _data;
    private AudioSource _audioSource;
    private float _soundVolume;

    private void Awake()
    { 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjs();
        AudioSystemSet();
        MenuLoad();
    }

    private void MenuLoad()
    {
        Instantiate(Resources.Load<GameObject>("Menu/Menu"));
    }

    private void AudioSystemSet()
    {
        AudioSourceObj.clip = _menuMusic;
        AudioSourceObj.loop = true;
        AudioSourceObj.volume = _soundVolume;
        AudioSourceObj.Play();
    }
}
