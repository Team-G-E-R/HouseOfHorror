using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class PuzzlesData : MonoBehaviour
{
    public DataKeys KeysData;
    public string output;
    private string _filePath => Application.streamingAssetsPath + "/keys.json";

    [ContextMenu("Save")]
    public void Save()
    {
        KeyWin();
        output = JsonConvert.SerializeObject(KeysData.KeysDict);
        File.WriteAllText(_filePath, output);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        KeysData = JsonUtility.FromJson<DataKeys>(File.ReadAllText(_filePath));
    }

    public void KeyWin()
    {
        var json = File.ReadAllText(_filePath);
        Debug.Log(json);
        KeysData.KeysDict = JsonConvert.DeserializeObject<Dictionary<string, bool>>(json);

        KeysData.KeysDict["Key1"] = true; // NEED TO WORK
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
