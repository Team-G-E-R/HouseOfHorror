using UnityEngine;
using System.IO;

public class SettingsData : MonoBehaviour
{
    public DataSettings GameSettingsData;
    private string _filePath => Application.streamingAssetsPath + "/settings.json";

    [ContextMenu("Save")]
    public void Save()
    {
        File.WriteAllText(_filePath, JsonUtility.ToJson(GameSettingsData));
    }
    
    [ContextMenu("Load")]
    public void Load()
    {
        GameSettingsData = JsonUtility.FromJson<DataSettings>(File.ReadAllText(_filePath));
    }

    [System.Serializable]
    public class DataSettings
    {
        public float Volume;
        public float MusicTime;
    }
}
