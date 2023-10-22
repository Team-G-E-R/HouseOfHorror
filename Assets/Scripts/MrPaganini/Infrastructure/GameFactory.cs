using System.Collections.Generic;
using UnityEngine;

public class GameFactory
{
    public List<ISavedProgress> ProgressesWriter { get; set; }
    public List<ISavedProgressReader> ProgressesReader { get; set; }


    public GameFactory()
    {
        ProgressesWriter = new List<ISavedProgress>();
        ProgressesReader = new List<ISavedProgressReader>();
    }

    public GameObject CreateScreenLoading() => 
        Instantiate(AssetsPath.SceneCurtainPath);

    public GameObject CreatPlayer() => 
        Instantiate(AssetsPath.PlayerPath);

    public GameObject CreateStartMenu() => 
        Instantiate(AssetsPath.StartMenu);

    public GameObject Instantiate(string namePrefab)
    {
        var gameObject = Object.Instantiate(FindPrefab(namePrefab));
        foreach (var savedProgress in gameObject.GetComponentsInChildren<ISavedProgress>())
            Register(savedProgress);
        return gameObject;
    }

    public T GetObjectForType<T>(string path) where T : Object => 
        GetLoadedObject<T>(path);

    public GameObject CreateCamera() => 
        Instantiate(AssetsPath.Camera);

    private GameObject FindPrefab(string namePrefab) =>
        GetLoadedObject<GameObject>(namePrefab);

    public void Register(ISavedProgressReader progressReader)
    {
        if (progressReader is ISavedProgress savedProgress)
            ProgressesWriter.Add(savedProgress);

        ProgressesReader.Add(progressReader);
    }

    private static T GetLoadedObject<T>(string path) where T : Object => 
        Resources.Load<T>(path);
}