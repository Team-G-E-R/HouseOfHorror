using UnityEngine;
using System.IO;

public class PuzzlesData : Finder
{
    public DataKeys KeysData;
    private string _filePath => Application.streamingAssetsPath + "/keys.json";

    [ContextMenu("Save")]
    public void Save()
    {
        KeysData.HasKey1 = true;
        File.WriteAllText(_filePath, JsonUtility.ToJson(KeysData));
    }

    [ContextMenu("Load")]
    public void Load()
    {
        KeysData = JsonUtility.FromJson<DataKeys>(File.ReadAllText(_filePath));
    }

    [System.Serializable]
    public class DataKeys
    {
        public bool HasKey1;
    }
}
