using UnityEngine;

public class PazzleManager : MonoBehaviour
{
    [SerializeField] private int TotalParts;
    public GameObject OnWinActive;
    private bool _isMove;
    private Vector2 _mousePosition;
    private float _startPartPosX;
    private float _startPartPosY;
    private GameObject _pazzlePart;
    private bool _partFinish;
    private Vector3 _movingPatrFinishPos;
    private int _solvedParts = 0;

    private void Start()
    {
        OnWinActive.SetActive(false);
    }

    public void StartMovingParts(GameObject part, Vector3 _finishPosition)
    {
        _partFinish = false;
        _movingPatrFinishPos = _finishPosition;
        _pazzlePart = part;
        _isMove = true;
        _startPartPosX = _mousePosition.x - _pazzlePart.transform.localPosition.x;
        _startPartPosY = _mousePosition.y - _pazzlePart.transform.localPosition.y;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0) || _isMove == true)
        {
            _isMove = false;
            Debug.Log(_solvedParts);

            if(Mathf.Abs(_pazzlePart.transform.position.x -_movingPatrFinishPos.x) <= 1f &&
            (_pazzlePart.transform.position.y -_movingPatrFinishPos.y) <= 1f)
            {
                _pazzlePart.GetComponent<PolygonCollider2D>().enabled = false;

                _pazzlePart.transform.position = new Vector3 (_movingPatrFinishPos.x,
                _movingPatrFinishPos.y,_movingPatrFinishPos.z);

                _partFinish = true;
                _solvedParts += 1;
                if (_solvedParts == TotalParts)
                    OnWinActive.SetActive(true);
            }
        }

        _mousePosition = Input.mousePosition;

        if(_isMove && !_partFinish)
            _pazzlePart.transform.localPosition = new Vector3(_mousePosition.x - _startPartPosX,
            _mousePosition.y - _startPartPosY,0);
    }
    
}
