using UnityEngine;
using System.IO;

public class KeyChecker : Repository
{
    private bool _key;

    public void CheckKey()
    {
        var keyjson = JsonUtility.FromJson<Key>(File.ReadAllText(Application.streamingAssetsPath + "/Keys.json"));
        _key = keyjson.HasKey;
        if (_key == true) GetComponent<SceneTransitions>().NextScene();
        else GameObject.FindWithTag("DialogueTrigger").GetComponent<DialogueTrigger>().ActivateDialogue();
    }
}
