using UnityEngine;
using System.IO;

public class SaveTrigger : Finder
{
   public LoadFromMenu.GameInfo PlayerInfo;
   private string _filePath => Application.streamingAssetsPath + "/save.json";
   private void OnTriggerEnter(Collider other)
   {
      Save();
      Debug.Log("Saved");
      Destroy(gameObject);
   }
   
   [ContextMenu("Save")]
   public void Save()
   {
      FindPlayerSettingsData();
      PlayerInfo.SceneIndexJson = SceneIndex;
      PlayerInfo.PlayerScenePosJson = PlayerScenePos;
      PlayerInfo.CameraPosJson = CameraPos;
      File.WriteAllText(_filePath, JsonUtility.ToJson(PlayerInfo));
   }
}
