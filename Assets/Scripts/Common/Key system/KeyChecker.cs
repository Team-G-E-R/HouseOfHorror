using UnityEngine;
using System.IO;

public class KeyChecker : PuzzlesData
{
    private bool _key;

    public void CheckKey()
    {
        _key = KeysData.KeysDict["Key1"];
        if (_key == true) GetComponent<SceneTransitions>().NextScene();
        else GameObject.FindWithTag("DialogueTrigger").GetComponent<DialogueTrigger>().ActivateDialogue();
    }
}
