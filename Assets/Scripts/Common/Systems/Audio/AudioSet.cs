using UnityEngine;

public class AudioSet : Finder
{
    [Header("Main audio settings")]
    [Tooltip("Select the audio that will play at the beginning of the scene")]
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _beLooped;
    [Tooltip("Set all clips to null when component get off")]
    [SerializeField] private bool _disableOnSceneChange;
    [Tooltip("Save audio playback time to JSON file")]
    [SerializeField] private bool _saveAudioTime;
    [Tooltip("Continue audio playback with time from JSON file")]
    [SerializeField] private bool _continueAudio;
    [Space(15)]
    [Header("Additional")]
    [SerializeField] private bool _needScreamer;
    [SerializeField] private bool _screamerMustBeLooped;
    [Tooltip("Will work only if upper bool true")]
    [SerializeField] private AudioClip _screamerToPlay;

    private AudioSource _screamerSource;

    private void OnEnable()
    {
       FindObjs();
       MusicSet();
       if (_continueAudio)
        AudioContinue();
       if (_needScreamer)
        ScreamerPlay();
    }

    private void MusicSet()
    {
        AudioSource audioSource = AudioSourceObj.Find(A => A.clip == null);

        if (audioSource == null)
            AudioSourceCreate(_audioClip, _beLooped);
        else if (_beLooped && audioSource != null)
        {
            audioSource.clip = _audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = _audioClip;
            audioSource.loop = _beLooped;
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

    public void ScreamerStop()
    {
        AudioSource audioSource = AudioSourceObj.Find(A => A.clip == _screamerToPlay);
        audioSource.Stop();
    }

    public void AudioContinue()
    {
        AudioSource audioSource = AudioSourceObj.Find(A => A.clip == _audioClip);
        SaveLoad.Instance.Load();
        audioSource.time = GameData.MusicTime;
    }

    private void AudioSourceCreate(AudioClip audioClip, bool loop)
    {
        GameObject go = new GameObject();
        go.name = "Audio " + audioClip;
        go.tag = "Audio";

        AudioSource newAudioSource = go.AddComponent<AudioSource>();
        newAudioSource.clip = audioClip;
        newAudioSource.volume = GameData.Volume;
        newAudioSource.loop = loop;
        newAudioSource.Play();
        AudioSourceObj.Add(newAudioSource);
    }

    private void OnDisable()
    {
        if (_saveAudioTime)
        {
            AudioSource audioSource = AudioSourceObj.Find(A => A.clip == _audioClip);
            SaveMusicTime(audioSource.time);
        }

        if (_disableOnSceneChange && AudioSourceObj != null)
        {
            foreach (var a in AudioSourceObj)
            {
                if (a != null)
                {
                    a.clip = null;
                    a.time = 0;
                    a.Stop();
                }
            }
        }
    }
}
