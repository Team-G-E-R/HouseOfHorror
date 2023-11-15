using UnityEngine;
using System.IO;

public class SavedDataScript : MonoBehaviour
{
    public Data _data;

    public void Save()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/settings.json", JsonUtility.ToJson(_data));
    }
    
    public void Load()
    {
        _data = JsonUtility.FromJson<Data>(File.ReadAllText(Application.streamingAssetsPath + "/settings.json"));
    }

    [System.Serializable]
    public class Data
    {
        public float Volume;
    }
}
