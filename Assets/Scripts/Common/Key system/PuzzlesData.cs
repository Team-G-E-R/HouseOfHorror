using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public enum KeyWin
{
    Key1 = 1,
    Key2 = 2,
    Key3 = 3,
    Vision = 4,
    Knife = 5,
    MirrorKey0 = 6,
    MirrorKey1 = 7,
    MirrorKey2 = 8,
    MirrorAll = 9
}
public class PuzzlesData : MonoBehaviour
{
    public DataKeys KeysData;
    public KeyWin KeyToWin;
    private string output;
    private string _filePath => Application.streamingAssetsPath + "/keys.json";

    [ContextMenu("Save")]
    public void Save()
    {
        KeyWin();
        output = JsonConvert.SerializeObject(KeysData);
        File.WriteAllText(_filePath, output);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        var json = File.ReadAllText(_filePath);
        KeysData = JsonConvert.DeserializeObject<DataKeys>(json);
    }

    [ContextMenu("Reset keys values")]
    public void AllDataToZero()
    {
        output = JsonConvert.SerializeObject(KeysData);
        File.WriteAllText(_filePath, output);
    }

    public void KeyWin()
    {
        var json = File.ReadAllText(_filePath);
        KeysData = JsonConvert.DeserializeObject<DataKeys>(json);

        KeysData.KeysDict[KeyToWin.ToString()] = true;
    }

    [System.Serializable]
    public class DataKeys
    {
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
            { "MirrorAll", false}
        };
    }
}
