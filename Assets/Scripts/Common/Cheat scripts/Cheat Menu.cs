using System.Collections.Generic;
using UnityEngine;

public class CheatMenu : MonoBehaviour
{
    public SaveLoad GameData => SaveLoad.Instance;
    public SaveLoad.GameDataDict GameData2 => SaveLoad.Instance.PlayerDict;

    public void DeleteSaves()
    {
        GameData.AllDataToZero();
    }

    public void AllKeysGet()
    {
        List<string> keys = new List<string>(GameData2.KeysDict.Keys);
        foreach (var key in keys)
        {
            GameData2.KeysDict[key] = true;
        }
        GameData.Save();
    }
}
