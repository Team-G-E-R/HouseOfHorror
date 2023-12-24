using UnityEngine;

public class MirrorDragAndDrop : MonoBehaviour
{
    [SerializeField] private PartNumber _partNumber;

    public PartNumber GetPartNumber() => _partNumber;

    private bool _dragging = false;
    private bool _locked = false;

    private Vector2 _offset = Vector2.zero;

    public void SetLocked(bool locked) => _locked = locked;

    private void OnMouseDown()
    {
        if (_locked)
        {
            _dragging = false;
            return;
        }

        _offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _dragging = true;
    }

    private void OnMouseUp()
    {
        if (_locked)
        {
            _dragging = false;
            return;
        }

        MirrorPuzzle.Instance.CheckRelative(this);
        _dragging = false;
    }

    private void Update()
    {
        if (_dragging == false || _locked)
            return;

        Vector2 mousePoisiton = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePoisiton - _offset);
    }
}
