using UnityEngine;

public class GameFactory
{
    public GameObject CreateScreenLoading() => 
        Instantiate(AssetsPath.SceneCurtainPath);

    public GameObject CreatPlayer() => 
        Instantiate(AssetsPath.PlayerPath);

    public GameObject CreateStartMenu() => 
        Instantiate(AssetsPath.StartMenu);

    public GameObject Instantiate(string namePrefab) => 
        Object.Instantiate(FindPrefab(namePrefab));

    public T GetObjectForType<T>(string path) where T : Object => 
        GetLoadedObject<T>(path);

    public GameObject CreateCamera() => 
        Instantiate(AssetsPath.Camera);

    private GameObject FindPrefab(string namePrefab) =>
        GetLoadedObject<GameObject>(namePrefab);

    private static T GetLoadedObject<T>(string path) where T : Object => 
        Resources.Load<T>(path);
}