using UnityEngine;
using UnityEngine.Events;

public class KeyChecker : PuzzlesData
{
    [Space(10)]
    [SerializeField] private UnityEvent _ifHaveKey;
    [SerializeField] private UnityEvent _ifDontHaveKey;
    public void CheckKey()
    {
        Load();
        var _key = KeysData.KeysDict[KeyToWin.ToString()];
        if (_key == true) _ifHaveKey.Invoke();
        else _ifDontHaveKey.Invoke(); 
    }
}
