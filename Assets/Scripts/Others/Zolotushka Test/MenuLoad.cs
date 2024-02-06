using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuLoad : MonoBehaviour
{
    public Menu Menu;
    public GameObject UiElements;
    public GameObject ZeroSavesUi;
    private SaveLoad.GameInfo _gameInfo => SaveLoad.Instance.PlayerData;

    public void LoadLevel()
    {
        if (_gameInfo == null && ZeroSavesUi != null) ZeroSavesUi.SetActive(true);
        else
        {
            DontDestroyOnLoad(this);
            Menu.SaveSettingsData();
            StartCoroutine("AsyncLoad");
        }
    }

    IEnumerator AsyncLoad()
    {
        AsyncOperation _asyncOperation;
        var fade = GetComponent<FadeInOut>();
        Menu.StartBtnPlay();
        _asyncOperation = SceneManager.LoadSceneAsync(_gameInfo.SceneIndexJson);
        _asyncOperation.allowSceneActivation = false;
        fade.duration = Menu.AudioStartBtn.length;
        fade.FadeIn();
        yield return new WaitForSeconds(fade.duration + 0.5f);
        while (!_asyncOperation.isDone)
        {
            _asyncOperation.allowSceneActivation = true;
            yield return null;
        }
        Destroy(UiElements);
        GameObject.FindWithTag("Player").transform.position = _gameInfo.PlayerScenePosJson;
        GameObject.FindWithTag("MainCamera").transform.position = _gameInfo.CameraPosJson;
        Destroy(gameObject);
    }

    public void BackUi()
    {
        UiElements.SetActive(true);
        ZeroSavesUi.SetActive(false);
    }
}
