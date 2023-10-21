using UnityEngine;

public class AudioService : MonoBehaviour, IAudioService
{
    public AudioSource AudioSource { get; private set; }

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
    }
}