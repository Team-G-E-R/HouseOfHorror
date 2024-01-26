using UnityEngine;
using UnityEngine.Events;

public enum PartNumber
{
    First = 0,
    Second,
    Third,
    Fourth,
    Fifth,
    Six
}

public class MirrorPuzzle : MonoBehaviour
{
    [SerializeField] private UnityEvent _onWin;
    [SerializeField] private float _maxPositionDifferences = 10f;
    [SerializeField] private MirrorDragAndDrop[] _dynamicParts;
    [SerializeField] private MirrorConstant[] _constantParts;

    public static MirrorPuzzle Instance;

    private void Awake()
    {
        Instance = this;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CheckRelative(MirrorDragAndDrop mirrorDragAndDrop)
    {
        foreach (MirrorConstant constantPart in _constantParts)
            if (mirrorDragAndDrop.GetPartNumber() == constantPart.GetPartNumber() 
                && _maxPositionDifferences >= Vector3.Distance(mirrorDragAndDrop.transform.position, constantPart.transform.position))
            {
                constantPart.Completed = true;
                mirrorDragAndDrop.transform.position = constantPart.transform.position;
                mirrorDragAndDrop.SetLocked(true);
            }

        CheckWin();
    }

    public void CheckWin()
    {
        bool win = true;

        foreach (MirrorConstant constantPart in _constantParts)
            if (constantPart.Completed == false)
                win = false;

        if (win)
        {
            Cursor.visible = false;
            _onWin.Invoke();
        }
    }
}
