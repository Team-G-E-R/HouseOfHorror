using UnityEngine;

public interface IAudioService : IService
{
    public AudioSource AudioSource { get; }
}