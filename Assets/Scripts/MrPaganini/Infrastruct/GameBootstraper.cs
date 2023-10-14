using GameResources.SO;
using MrPaganini.UI.Windows;
using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    private const string StartMenu = "StartMenu";
    
    private GameFactory _gameFactory;
    private AllServices _allServices;
    private AudioService _audioService;


    private void Awake()
    {
        _gameFactory = new GameFactory();
        _allServices = new AllServices();
        var sceneLoader = new SceneLoader();

        _audioService = InstantiateAudioService();
        RegisterAudioService(_audioService);
        RegisterSettingsService();
        
        
        sceneLoader.Load(StartMenu, OnLoaded);
        
        DontDestroyOnLoad(this);
    }

    private void OnLoaded()
    {
        StartMenu startMenu = _gameFactory
            .CreateStartMenu()
            .GetComponent<StartMenu>();
        startMenu.Construct(_gameFactory, _audioService);
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
    private void RegisterAudioService(AudioService audioService)
    {
        _allServices
            .RegisterSingle<IAudioService>()
            .To<AudioService>(audioService);
    }
}