using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private const string ProgressKey = "ProgressKey";
    
    private readonly IPersistentProgressService _progressService;
    private readonly GameFactory _gameFactory;
    private readonly AllServices _allServices;

    public SaveLoadService(IPersistentProgressService progressService, GameFactory gameFactory,
        AllServices allServices)
    {
        _progressService = progressService;
        _gameFactory = gameFactory;
        _allServices = allServices;
    }

    public void SaveProgress()
    {
        foreach(ISavedProgress progressWriter in _gameFactory.ProgressesWriter)
            progressWriter.UpdateProgress(_progressService.PlayerProgress);
        
        foreach (ISavedProgress progressWriter in _allServices.SavedProgressesServices)
            progressWriter.UpdateProgress(_progressService.PlayerProgress);

        PlayerPrefs.SetString(ProgressKey, _progressService.PlayerProgress.ToJson());
    }

    public PlayerProgress LoadProgress() =>
        PlayerPrefs.GetString(ProgressKey)?
            .ToDeserialized<PlayerProgress>();

    public void Update()
    {
        
    }
}