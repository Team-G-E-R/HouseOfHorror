using UnityEngine;

public class BootstrapSystem : Finder
{
    [SerializeField] private AudioClip _menuMusic;
    
    private float _soundVolume;

    private void Start()
    {
        CursorOn();
        FindObjs();
        AudioSystemSet();
    }

    private void AudioSystemSet()
    {
        _soundVolume = GameData.Volume;
        foreach (var a in AudioSourceObj)
        {
            a.clip = _menuMusic;
            a.loop = true;
            a.volume = _soundVolume;
            a.Play();   
        }
    }
}
