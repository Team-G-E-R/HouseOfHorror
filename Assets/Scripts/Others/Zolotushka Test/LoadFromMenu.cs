using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadFromMenu : Finder
{
   private GameInfo _playerData;
   private Menu _menu;
   private string _filePath => Application.streamingAssetsPath + "/save.json";

   private void Awake()
   {
      DontDestroyOnLoad(this);
      FindObjs();
      _menu = gameObject.GetComponent<Menu>();
   }

   /*[ContextMenu("Save")]
   public void Save()
   {
      FindPlayerSettingsData();
      _playerData.SceneIndexJson = SceneIndex;
      _playerData.PlayerScenePosJson = PlayerScenePos;
      _playerData.CameraPosJson = CameraPos;
      File.WriteAllText(_filePath, JsonUtility.ToJson(_playerData));
   }*/

   [ContextMenu("Load")]
   public void Load()
   {
      _playerData = JsonUtility.FromJson<GameInfo>(File.ReadAllText(_filePath));
      StartCoroutine("AsyncLoad");
   }
   
   IEnumerator AsyncLoad()
   {
      var fade = GetComponent<FadeInOut>();
      _menu.AudioSourceStartBtn.volume = SettingsDataobj.GameSettingsData.Volume;
      _menu.AudioSourceStartBtn.Play();
      _menu.AudioSourceObj.Stop();
      AsyncOperation _asyncOperation;
      _asyncOperation = SceneManager.LoadSceneAsync(_playerData.SceneIndexJson);
      _asyncOperation.allowSceneActivation = false;
      fade.duration = _menu.AudioStartBtn.length;
      fade.FadeIn();
      yield return new WaitForSeconds(fade.duration + 0.5f);
      while (!_asyncOperation.isDone)
      {
         _asyncOperation.allowSceneActivation = true;
         yield return null;
      }
      _playerData = JsonUtility.FromJson<GameInfo>(File.ReadAllText(_filePath));
      GameObject.FindWithTag("Player").transform.position = _playerData.PlayerScenePosJson;
      GameObject.FindWithTag("MainCamera").transform.position = _playerData.CameraPosJson;
      Destroy(gameObject);
   }

   [ContextMenu("Reset player saves")]
   public void AllDataToZero()
   {
      File.WriteAllText(_filePath, JsonUtility.ToJson(_playerData));
   }

   [System.Serializable]
   public class GameInfo
   {
      public int SceneIndexJson;
      public Vector3 PlayerScenePosJson;
      public Vector3 CameraPosJson;
   }
}
