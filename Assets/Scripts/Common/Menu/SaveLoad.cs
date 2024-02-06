using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SaveLoad : MonoBehaviour
{
   public static SaveLoad Instance;
   public GameInfo PlayerData => _playerData;
   private GameInfo _playerData;
   private const string _dataPath = "/Game Data.json";
   private string _filePath => Application.streamingAssetsPath + _dataPath;

   private void Awake()
   {
      _playerData = new GameInfo();
      Load();
      Instance = this;
      DontDestroyOnLoad(this);
   }

   [ContextMenu("Save")]
   public void Save()
   {
      //FindPlayerSettingsData();
      File.WriteAllText(_filePath, JsonUtility.ToJson(PlayerData));
   }

   [ContextMenu("Load")]
   public void Load()
   {
      _playerData = JsonUtility.FromJson<GameInfo>(File.ReadAllText(_filePath));
   }

   [ContextMenu("Reset selected saves")]
   public void AllDataToZero()
   {
      File.WriteAllText(_filePath, JsonUtility.ToJson(new GameInfo()));
   }

   public void FindPlayerSettingsData()
    {
      _playerData.SceneIndexJson = SceneManager.GetActiveScene().buildIndex;
      _playerData.PlayerScenePosJson = GameObject.FindWithTag("Player").transform.position;
      _playerData.CameraPosJson = GameObject.FindWithTag("MainCamera").transform.position;
    }

   [RuntimeInitializeOnLoadMethod]
   public static void CreateInstance()
   {
      GameObject data = new GameObject("Game Data");
      data.AddComponent<SaveLoad>();
   }

   [System.Serializable]
   public class GameInfo
   {
      // Level Data
      public int SceneIndexJson;
      public Vector3 PlayerScenePosJson;
      public Vector3 CameraPosJson;

      // Diary Data
      public int Turn = 1;
      public int RequiredTurn = 1;

      public Dictionary<int, string> Diary = new Dictionary<int, string>()
      {
         { -1, "" },
      };

      public List<int> UnlockedPages = new();
      public List<int> AnimatedPages = new();

      // Key Data
      public Dictionary<string, bool> KeysDict = new Dictionary<string, bool>()
        {
            { "Key1", false },
            { "Key2", false },
            { "Key3", false },
            { "Vision", false },
            { "Knife", false },
            { "MirrorKey0", false},
            { "MirrorKey1", false},
            { "MirrorKey2", false},
            { "MirrorDone", false}
        };

      public float Volume;
      public float MusicTime;
   }
}
