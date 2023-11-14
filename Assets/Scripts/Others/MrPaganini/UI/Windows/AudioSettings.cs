using UnityEngine;
using GameResources.SO;
using UnityEngine.UI;

namespace MrPaganini.UI.Windows
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private AudioClip _startMenuSound;
        [SerializeField] private Slider _soundVolume;

        private AudioSource _audioSource;
        private SettingsConfig _volumeSetting;
        
        private void Start()
        {
            IAudioService audioService = AllServices.Singleton.Single<IAudioService>();
            _volumeSetting = AllServices.Singleton.Single<ISettingsService>().SettingsConfig;
            
            _audioSource = audioService.AudioSource;
            _soundVolume.value = _audioSource.volume;
            _soundVolume.onValueChanged.AddListener((v) => _audioSource.volume = v);
            _audioSource.volume = _volumeSetting.Volume;
        }
        public void OnChangeVolume()
        {
            _volumeSetting.Volume = _audioSource.volume;
        }
    }
}