using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Newtonsoft.Json;

public class SaveLoad : MonoBehaviour
{
   public static SaveLoad Instance;
   public GameInfo PlayerData => _playerData;
   public GameDataDict PlayerDict => _playerDict;
   private GameInfo _playerData;
   private GameDataDict _playerDict;
   private const string _dataPath = "/Game Data.json";
   private const string _dataDictPath = "/Game Data Dict.json";
   private string _filePath => Application.streamingAssetsPath + _dataPath;
   private string _fileDictPath => Application.streamingAssetsPath + _dataDictPath;

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
      File.WriteAllText(_filePath, JsonUtility.ToJson(_playerData));
      File.WriteAllText(_fileDictPath, JsonConvert.SerializeObject(_playerDict));
   }

   [ContextMenu("Load")]
   public void Load()
   {
      _playerData = JsonUtility.FromJson<GameInfo>(File.ReadAllText(_filePath));
      _playerDict = JsonConvert.DeserializeObject<GameDataDict>(File.ReadAllText(_fileDictPath));
   }

   [ContextMenu("Reset all saves")]
   public void AllDataToZero()
   {
      _playerData = new GameInfo();
      _playerDict = new GameDataDict();
      File.WriteAllText(_filePath, JsonUtility.ToJson(_playerData));
      File.WriteAllText(_fileDictPath, JsonConvert.SerializeObject(_playerDict));
   }

   public void FindPlayerSettingsData() // Почему он тут ищет игрока? Убрать
    {
      _playerData.SceneIndex = SceneManager.GetActiveScene().buildIndex;
      _playerData.PlayerScenePos = GameObject.FindWithTag("Player").transform.position;
      _playerData.CameraPos = GameObject.FindWithTag("MainCamera").transform.position;
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
      public int SceneIndex;
      public Vector3 PlayerScenePos;
      public Vector3 CameraPos;

      // Diary Data
      public int Turn = 1;
      public int RequiredTurn = 1;

      public List<int> UnlockedPages = new();
      public List<int> AnimatedPages = new();

      public float Volume;
      public float MusicTime;
   }

   public class GameDataDict
   {
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

      public Dictionary<int, string> Diary = new Dictionary<int, string>()
         {
            { -1, "" },
         };
   }
}
