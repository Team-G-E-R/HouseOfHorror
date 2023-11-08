using MrPaganini.UI.Windows;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _audioToNextScene;
    [SerializeField] private AudioClip _startMenuSound;
    private string NextLevel = "";


    private LoadLevel _loadLevel;
    private AudioSource _audioSource;
    private IPersistentProgressService _progressService;

    public void Construct(GameFactory gameFactory,
        IAudioService audioService, IPersistentProgressService progressService)
    {
        _loadLevel = new LoadLevel(gameFactory, progressService, _audioToNextScene);
        _progressService = progressService;
        Debug.Log(progressService.PlayerProgress);
        NextLevel = _progressService.PlayerProgress.WorldData.PositionOnLevel.Level;
        StartMusic(audioService);
    }

    public void StartGame()
    {
        _loadLevel.EnterLevel(NextLevel);
    }

    public void ExitGame() => 
        Application.Quit();

    private void StartMusic(IAudioService audioService)
    {
        var _volumeSetting = AllServices.Singleton.Single<ISettingsService>().SettingsConfig;
        _audioSource = audioService.AudioSource;
        _audioSource.clip = _startMenuSound;
        _audioSource.Play();
        _audioSource.volume = _volumeSetting.Volume;
    }
}
