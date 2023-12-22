using UnityEngine;

public class AudioSet : Finder
{
    [Header("Main audio settings")]
    [Tooltip("Select the audio that will play at the beginning of the scene")]
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _needToBeLooped;
    [Tooltip("Set all clips to null when component get off")]
    [SerializeField] private bool _needToDisableOnSceneChange;
    [Space(15)]
    [Header("Additional")]
    [SerializeField] private bool _needScreamer;
    [SerializeField] private bool _screamerMustBeLooped;
    [Tooltip("Will work only if upper bool true")]
    [SerializeField] private AudioClip _screamerToPlay;

    private AudioSource _screamerSource;

    private void Awake()
    {
       FindObjs();
       MusicSet();
       if (_needScreamer)
        ScreamerPlay();
    }

    private void MusicSet()
    {
        AudioSource audioSource = AudioSourceObj.Find(A => A.clip == null);

        if (audioSource == null)
            AudioSourceCreate(_audioClip, _needToBeLooped);
        else if (_needToBeLooped && audioSource != null)
        {
            audioSource.clip = _audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = _audioClip;
            audioSource.loop = _needToBeLooped;
            audioSource.Play();
        }
    }

    public void ScreamerPlay()
    {
        AudioSource audioSource = AudioSourceObj.Find(A => A.clip == null);

        if (_screamerMustBeLooped && audioSource != null)
        {
            audioSource.clip = _screamerToPlay;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (_screamerMustBeLooped && audioSource == null)
            AudioSourceCreate(_screamerToPlay, _screamerMustBeLooped);
        else
        {
            AudioSourceObj[0].PlayOneShot(_screamerToPlay, 1);
        }
    }

    private void AudioSourceCreate(AudioClip audioClip, bool loop)
    {
        GameObject go = new GameObject();
        go.name = "Audio " + audioClip;
        go.tag = "Audio";

        AudioSource newAudioSource = go.AddComponent<AudioSource>();
        newAudioSource.clip = audioClip;
        newAudioSource.volume = SettingsDataobj.GameSettingsData.Volume;
        newAudioSource.loop = loop;
        newAudioSource.Play();
        AudioSourceObj.Add(newAudioSource);
    }

    private void OnDisable()
    {
        if (_needToDisableOnSceneChange && AudioSourceObj != null)
        {
            foreach (var a in AudioSourceObj)
            {
                a.clip = null;
                a.Stop();
            }
        }
    }
}
