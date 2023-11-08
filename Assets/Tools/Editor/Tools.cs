using UnityEditor;
using UnityEngine;

public class Tools
{
    [MenuItem("Tools/Clear prefs")]
    public static void CleatPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}