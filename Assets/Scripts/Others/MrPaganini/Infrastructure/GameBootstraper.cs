using GameResources.SO;
using MrPaganini.UI.Windows;
using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    public string InitialLevel = "Level 1";
    private const string StartMenu = "StartMenu";

    private GameFactory _gameFactory;
    private AllServices _allServices;
    private AudioService _audioService;
    private IPersistentProgressService _progressService;
    private ISaveLoadService _saveLoadService;


    private void Awake()
    {
        _gameFactory = new GameFactory();
        _allServices = new AllServices();
        var sceneLoader = new SceneLoader();
        _progressService = new PersistentProgressService();

        _audioService = InstantiateAudioService();
        RegisterAudioService(_audioService);
        RegisterSettingsService();
        RegisterProgressService();
        RegisterSaveLoadService();


        sceneLoader.Load(StartMenu, OnLoaded);
        
        DontDestroyOnLoad(this);
    }

    private void OnLoaded()
    {
        LoadProgressOrInit();
        CreatStartMenu();
    }

    private void LoadProgressOrInit()
    {
        _progressService.PlayerProgress =
            _saveLoadService.LoadProgress()
            ??
            NewProgress();
    }

    private PlayerProgress NewProgress() =>
        _progressService.PlayerProgress = new PlayerProgress(InitialLevel);

    private void CreatStartMenu()
    {
        StartMenu startMenu = _gameFactory
            .CreateStartMenu()
            .GetComponent<StartMenu>();
        startMenu.Construct(_gameFactory, _audioService, _progressService);
    }

    private AudioService InstantiateAudioService()
    {
        return _gameFactory
            .Instantiate(AssetsPath.AudioService)
            .GetComponent<AudioService>();
    }

    private void RegisterSettingsService()
    {
        var settingsService = new SettingsService(
            _gameFactory.GetObjectForType<SettingsConfig>(AssetsPath.SettingsConfig));
        _allServices.RegisterSingle<ISettingsService>()
            .To(settingsService);
    }

    private void RegisterProgressService()
    {
        _progressService = new PersistentProgressService();
        _allServices.RegisterSingle<IPersistentProgressService>().To(_progressService);
    }
    private void RegisterSaveLoadService()
    {
        _saveLoadService = new SaveLoadService(_progressService, _gameFactory, _allServices); 
        _allServices.RegisterSingle<ISaveLoadService>()
            .To(_saveLoadService);
    }
    private void RegisterAudioService(AudioService audioService)
    {
        _allServices
            .RegisterSingle<IAudioService>()
            .To(audioService);
        audioService.AudioSource.transform.gameObject.tag = "Audio";
        audioService.AudioSource.loop = true;
    }
}