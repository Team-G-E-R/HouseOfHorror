using UnityEditor;
using UnityEngine;

public class LoadLevel
{
    private AudioClip _audioToNextScene;
    private SceneLoader _sceneLoader;
    private GameFactory _gameFactory;
    private readonly IPersistentProgressService _persistentProgressService;
    private LoadingCurtain _loadingCurtain;

    private const string InitialPlayerPointTag = "InitialPoint";
    private const string InitialCameraPointTag = "InitialCameraPoint";

    public LoadLevel(GameFactory gameFactory, IPersistentProgressService persistentProgressService, AudioClip _audioClip)
    {
        _audioToNextScene = _audioClip;
        _gameFactory = gameFactory;
        _persistentProgressService = persistentProgressService;
        _sceneLoader = new SceneLoader();
    }


    public void EnterLevel(string nameScene)
    {
        _loadingCurtain = InstantiateLoadingCurtain();
        _loadingCurtain.Show();
        _sceneLoader.ChangeProgress += OnChangeProgress;
        _sceneLoader.Load(nameScene, OnLoaded);
    }

    private void OnChangeProgress(float progress) => 
        _loadingCurtain.UpdateProgress(progress);

    private void OnLoaded()
    {
        EnableMusic();
        InformProgressReaders();
        _loadingCurtain.Hide();
        _sceneLoader.ChangeProgress -= OnChangeProgress;
    }
    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressesReader)
            progressReader.LoadProgress(_persistentProgressService.PlayerProgress);
    }
    private void EnableMusic()
    {
        var audioService = AllServices.Singleton.Single<IAudioService>();
        /*audioService.AudioSource.clip = _audioToNextScene;
        audioService.AudioSource.Play();*/
    }


    private LoadingCurtain InstantiateLoadingCurtain()
    {
        GameObject loadingCurtainPrefab = _gameFactory.CreateScreenLoading();
        return loadingCurtainPrefab.GetComponent<LoadingCurtain>();
    }
}