using UnityEngine;
using System.IO;

public class Data : MonoBehaviour
{
    public DataSettings GameData;
    private string _filePath => Application.streamingAssetsPath + "/settings.json";

    [ContextMenu("Save")]
    public void Save()
    {
        File.WriteAllText(_filePath, JsonUtility.ToJson(GameData));
    }
    
    [ContextMenu("Load")]
    public void Load()
    {
        GameData = JsonUtility.FromJson<DataSettings>(File.ReadAllText(_filePath));
    }

    [System.Serializable]
    public class DataSettings
    {
        public float Volume;
    }
}
