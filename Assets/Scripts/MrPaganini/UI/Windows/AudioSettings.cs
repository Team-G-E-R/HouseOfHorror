using UnityEngine;
using UnityEngine.UI;

namespace MrPaganini.UI.Windows
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private AudioClip _startMenuSound;
        [SerializeField] private Slider _soundVolume;

        private AudioSource _audioSource;

        private void Start()
        {
            IAudioService audioService = AllServices.Singleton.Single<IAudioService>();
            var settingsService = AllServices.Singleton.Single<ISettingsService>().SettingsConfig;
            
            _audioSource = audioService.AudioSource;
            _soundVolume.value = _audioSource.volume;
            _soundVolume.onValueChanged.AddListener((v) => _audioSource.volume = v);
            settingsService.Volume = _audioSource.volume;
        }
    }
}