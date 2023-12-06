using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DiaryData : MonoBehaviour
{
    public DataKeys KeysData;
    private string _filePath => Application.streamingAssetsPath + "/diary.json";

    public void Save()
    {
        string output = JsonConvert.SerializeObject(KeysData);
        File.WriteAllText(_filePath, output);
    }

    public void Load()
    {
        if (File.Exists(_filePath))
            KeysData = JsonConvert.DeserializeObject<DataKeys>(File.ReadAllText(_filePath));
    }

    [System.Serializable]
    public class DataKeys
    {
        public int Turn = 1;

        public Dictionary<int, string> Diary = new Dictionary<int, string>()
        {
            { -1, "" },
        };
    }
}
