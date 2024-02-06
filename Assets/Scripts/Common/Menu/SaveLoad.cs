using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SaveLoad : MonoBehaviour
{
   Dictionary<SaveKeys, string> SaveTypes = new() 
   {
      [SaveKeys.save] = "save",
      [SaveKeys.keys] = "keys",
      [SaveKeys.settings] = "settings",
      [SaveKeys.diary] = "diary"
   };
   public enum SaveKeys : int
   {
      save = 0,
      keys,
      settings,
      diary
   }
   public Menu Menu;
   public GameObject UiElements;
   public GameObject ZeroSavesUi;
   public SaveKeys WhatToSave;
   private GameInfo _playerData;
   private string _filePath => Application.streamingAssetsPath + "/" + SaveTypes[WhatToSave] + ".json";

   private void Awake()
   {
      _playerData = new GameInfo();
   }

   [ContextMenu("Save")]
   public void Save()
   {
      FindPlayerSettingsData();
      File.WriteAllText(_filePath, JsonUtility.ToJson(_playerData));
   }

   [ContextMenu("Load")]
   public void Load()
   {
      if (WhatToSave == SaveKeys.save)
      {
         LevelDataChecker();  
      }
   }

   public void FindPlayerSettingsData()
    {
      _playerData.SceneIndexJson = SceneManager.GetActiveScene().buildIndex;
      _playerData.PlayerScenePosJson = GameObject.FindWithTag("Player").transform.position;
      _playerData.CameraPosJson = GameObject.FindWithTag("MainCamera").transform.position;
    }
   
   private void LevelDataChecker()
   {
      _playerData = JsonUtility.FromJson<GameInfo>(File.ReadAllText(_filePath));
      if (_playerData == null && ZeroSavesUi != null) ZeroSavesUi.SetActive(true);
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
      GameObject.FindWithTag("Player").transform.position = _playerData.PlayerScenePosJson;
      GameObject.FindWithTag("MainCamera").transform.position = _playerData.CameraPosJson;
      Destroy(gameObject);
   }

   public void BackUi()
   {
      UiElements.SetActive(true);
      ZeroSavesUi.SetActive(false);
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
