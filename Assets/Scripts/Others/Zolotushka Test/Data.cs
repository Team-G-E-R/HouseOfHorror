using UnityEngine;
using System.IO;

public class Data : MonoBehaviour
{
    public DataSettings GameData;
    private string filePath => Application.streamingAssetsPath + "/settings.json";

    [ContextMenu("Save")]
    public void Save()
    {
        File.WriteAllText(filePath, JsonUtility.ToJson(GameData));
    }
    
    [ContextMenu("Load")]
    public void Load()
    {
        GameData = JsonUtility.FromJson<DataSettings>(File.ReadAllText(filePath));
    }

    [System.Serializable]
    public class DataSettings
    {
        public float Volume;
    }
}
