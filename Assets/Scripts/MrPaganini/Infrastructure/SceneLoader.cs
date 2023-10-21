using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public event Action<float> ChangeProgress;
        
    public async void Load(string sceneName, Action onLoaded = null) => 
        await LoadSceneAsync(sceneName, onLoaded);

    private async Task LoadSceneAsync(string nameScene, Action onLoaded = null)
    {
        if (SceneManager.GetActiveScene().name == nameScene)
        {
            onLoaded?.Invoke();
            return;
        }
            
        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nameScene);

        while (waitNextScene.isDone == false)
        {
            ChangeProgress?.Invoke(waitNextScene.progress);
            await Task.Yield();
        }
        onLoaded?.Invoke();
    }

}
