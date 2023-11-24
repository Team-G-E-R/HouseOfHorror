using UnityEngine;
using System.IO;

public class PlayerRepository : Finder
{
   public GameInfo PlayerData;
   private string _filePath => Application.streamingAssetsPath + "/save.json";

   public void Update() //delete after tests
   {
      if (Input.GetKeyDown(KeyCode.V))
      {
         Save();
      }

      if (Input.GetKeyDown(KeyCode.C))
      {
         Load();
      } 
   }

   [ContextMenu("Save")]
   public void Save()
   {
      FindPlayerSettingsData();
      PlayerData.SceneIndexJson = SceneIndex;
      PlayerData.PlayerScenePosJson = PlayerScenePos;
      PlayerData.CameraPosJson = CameraPos;
      File.WriteAllText(_filePath, JsonUtility.ToJson(PlayerData));
   }

   [ContextMenu("Load")]
   public void Load()
   {
      PlayerData = JsonUtility.FromJson<GameInfo>(File.ReadAllText(_filePath));
      GameObject.FindWithTag("Player").transform.position = PlayerData.PlayerScenePosJson;
      GameObject.FindWithTag("MainCamera").transform.position = PlayerData.CameraPosJson;
   }

   [System.Serializable]
   public class GameInfo
   {
      public int SceneIndexJson;
      public Vector3 PlayerScenePosJson;
      public Vector3 CameraPosJson;
      public bool HasKey1;
   }
}
