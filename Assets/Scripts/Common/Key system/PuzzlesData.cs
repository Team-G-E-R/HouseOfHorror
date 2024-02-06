using System.Collections.Generic;
using UnityEngine;

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
    MirrorDone = 9
}
public class PuzzlesData : MonoBehaviour
{
    public SaveLoad.GameInfo GameData => SaveLoad.Instance.PlayerData;
    public Dictionary<string, bool> KeysData = new(){};
    public KeyWin KeyToWin;

    [ContextMenu("Save")]
    public void Save()
    {
        KeysData[KeyToWin.ToString()] = true;
        SaveLoad.Instance.Save();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        KeysData = GameData.KeysDict;
    }
}
