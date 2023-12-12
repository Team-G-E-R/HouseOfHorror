using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadFromMenu : Finder
{
   public Menu Menu;
   public GameObject UiElements;
   private GameInfo _playerData;
   private string _filePath => Application.streamingAssetsPath + "/save.json";

   private void Awake()
   {
      FindObjs();
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
      DontDestroyOnLoad(this);
      _playerData = JsonUtility.FromJson<GameInfo>(File.ReadAllText(_filePath));
      StartCoroutine("AsyncLoad");
   }
   
   IEnumerator AsyncLoad()
   {
      var fade = GetComponent<FadeInOut>();
      Menu.AudioSourceStartBtn.volume = SettingsDataobj.GameSettingsData.Volume;
      Menu.AudioSourceStartBtn.Play();
      Menu.AudioSourceObj.Stop();
      AsyncOperation _asyncOperation;
      _asyncOperation = SceneManager.LoadSceneAsync(_playerData.SceneIndexJson);
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
