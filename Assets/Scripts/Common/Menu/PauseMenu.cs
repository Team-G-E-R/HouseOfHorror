using GameResources.SO;
using MrPaganini.UI.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isMenuActive = false;
    [SerializeField] KeyCode KeyToActivateMenu;
    [SerializeField] int mainMenuSceneIndex;
    [SerializeField] private Slider _soundVolume;
    private SettingsConfig _volumeSetting;
    
    private AudioSource _audioSource;
    
    private void Start()
    {
        pauseMenu.SetActive(false);
        if (AllServices.Singleton != null)
        {
            _volumeSetting = AllServices.Singleton.Single<ISettingsService>().SettingsConfig;
            _audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
            _audioSource.volume = _volumeSetting.Volume;
            _soundVolume.value = _volumeSetting.Volume;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyToActivateMenu))
        {
            var audioService = FindObjectOfType<AudioService>();
            if (audioService != null) MenuActive(audioService);
            else MenuActive2();
        }
    }

    public void OnValueSound()
    {
        _volumeSetting.Volume = _soundVolume.value;
        _audioSource.volume = _soundVolume.value;
    }

    private void MenuActive(IAudioService audioService)
    {
        isMenuActive=!isMenuActive;

        if (isMenuActive)
        {
            _audioSource = audioService.AudioSource;
            _audioSource.volume = _volumeSetting.Volume;

            Cursor.visible = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
        }
    }
    
    private void MenuActive2()
    {
        isMenuActive=!isMenuActive;

        if (isMenuActive)
        {
            _soundVolume.onValueChanged.AddListener((v) => _audioSource.volume = v);
            if (AllServices.Singleton != null) _audioSource.volume = _volumeSetting.Volume;

            Cursor.visible = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
        }
    }

    public void continueButton()
    {
        var audioService = FindObjectOfType<AudioService>();
        if (audioService != null) MenuActive(audioService);
        else MenuActive2();
    }
    
    public void mainMenuButton()
    {
        var audioService = FindObjectOfType<AudioService>();
        if (audioService != null) Destroy(audioService.gameObject);
        SceneManager.LoadScene(mainMenuSceneIndex);
        Time.timeScale = 1f;
    }
}
