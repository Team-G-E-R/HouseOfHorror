using System;
using UnityEngine;

public class AudioSet : Finder
{
    [Header("Main audio settings")]
    [Tooltip("Select the audio that will play at the beginning of the scene")]
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _needToBeLooped;
    [SerializeField] private bool _needToDisableOnSceneChange;
    [Space(15)]
    [Header("Additional")]
    [SerializeField] private bool _needScreamer;
    [SerializeField] private bool _screamerMustBeLooped;
    [Tooltip("Will work only if upper bool true")]
    [SerializeField] private AudioClip _screamerToPlay;
    
    private AudioSource _audioSource2;
    private bool notFound = true;

    private void Awake()
    {
       FindObjs();
       MusicSet();
       ScreamerSet();
    }

    private void MusicSet()
    {
        if (_audioClip != null)
        {
            if (_needToBeLooped)
            {
                foreach (var a in AudioSourceObj)
                {
                    if (a.clip == null)
                    {
                        notFound = false;
                        a.clip = _audioClip;
                        a.volume = SettingsDataobj.GameSettingsData.Volume;
                        a.loop = true;
                        a.Play();
                    }
                }
                if (notFound)
                {
                    GameObject audObj = new GameObject();
                    audObj.name = "Audio " + _audioClip;
                    audObj.tag = "Audio";

                    audObj.AddComponent<AudioSource>();
                    var audSource = audObj.GetComponent<AudioSource>();
                    audSource.clip = _audioClip;
                    audSource.volume = SettingsDataobj.GameSettingsData.Volume;
                    audSource.loop = true;
                    audSource.Play();
                }
            }
            else
            {
                foreach (var a in AudioSourceObj)
                { 
                    a.PlayOneShot(_audioClip, 1);
                }    
            }
        }
    }

    private void AudioSourceCreate()
    {
        GameObject go = new GameObject();
        go.name = "Audio" + _screamerToPlay;
        go.tag = "Audio";

        _audioSource2 = go.AddComponent<AudioSource>();
        _audioSource2.clip = _screamerToPlay;
        _audioSource2.volume = SettingsDataobj.GameSettingsData.Volume;
        _audioSource2.loop = true;
        _audioSource2.Play();
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
        if (_screamerMustBeLooped) AudioSourceCreate();
        else
        {
            foreach (var a in AudioSourceObj)
            { 
                a.PlayOneShot(_screamerToPlay, 1);
            }   
        }
    }

    private void OnDisable()
    {
        if (_needToDisableOnSceneChange)
        {
            FindObjs();
            if (AudioSourceObj != null)
            {
                foreach (var a in AudioSourceObj)
                {
                    a.clip = null;
                    a.Stop();
                }   
            }
        }
    }
}
