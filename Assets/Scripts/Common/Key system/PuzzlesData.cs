using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class PuzzlesData : MonoBehaviour
{
    public DataKeys KeysData;
    private string _filePath => Application.streamingAssetsPath + "/keys.json";

    [ContextMenu("Save")]
    public void Save()
    {
        Debug.Log(KeysData.KeysDict["Key1"] && KeysData.KeysDict["Key2"]);
        KeyWin();
        File.WriteAllText(_filePath, JsonConvert.SerializeObject(KeysData.KeysDict));
    }

    [ContextMenu("Load")]
    public void Load()
    {
        KeysData = JsonUtility.FromJson<DataKeys>(File.ReadAllText(_filePath));
    }

    public void KeyWin()
    {
        KeysData = JsonUtility.FromJson<DataKeys>(File.ReadAllText(_filePath));
        Debug.Log(KeysData.KeysDict["Key1"] && KeysData.KeysDict["Key2"]);
        KeysData.KeysDict["Key2"] = true;
        Debug.Log(KeysData.KeysDict["Key1"] && KeysData.KeysDict["Key2"]);
    }

    [System.Serializable]
    public class DataKeys
    {
        public Dictionary<string, bool> KeysDict = new Dictionary<string, bool>()
        {
            { "Key1", false },
            { "Key2", false },
            { "Key3", false },
            { "Key4", false },
            { "Key5", false }
        };
    }
}
