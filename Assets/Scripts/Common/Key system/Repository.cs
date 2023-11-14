using UnityEngine;
using System.IO;

public class Repository : MonoBehaviour
{
    public Key key;

    [ContextMenu("Load")]
    public void Load()
    {
        key = JsonUtility.FromJson<Key>(File.ReadAllText(Application.streamingAssetsPath + "/Keys.json"));
    }

    [ContextMenu("Save")]
    public void Save()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/Keys.json", JsonUtility.ToJson(key));
    }

    [System.Serializable]
    public class Key
    {
        public bool HasKey;
    }
}