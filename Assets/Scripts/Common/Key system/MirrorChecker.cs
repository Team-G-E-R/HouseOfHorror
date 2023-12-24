using UnityEngine;

public class MirrorChecker : MonoBehaviour
{
    [SerializeField] private PuzzlesData _puzzlesData;
    [SerializeField] private GameObject[] _mirrorPieces;
    [SerializeField] private GameObject _mirrorObj;
    bool key;

    public void PieceChecker()
    {
        _puzzlesData.Load();
        _mirrorObj.SetActive(true);
        for (int i = 0; i < _mirrorPieces.Length; i++)
        {
            if (_puzzlesData.KeysData.KeysDict.TryGetValue("MirrorKey" + i, out key) && key == true)
                _mirrorPieces[i].SetActive(true);
                Debug.Log(key);
        }
    }
}
