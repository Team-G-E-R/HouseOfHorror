using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PazzleManager : MonoBehaviour
{
    private bool _isMove;
    private Vector2 _mousePosition;
    private float _startPartPosX;
    private float _startPartPosY;
    private GameObject _pazzlePart;
    private bool _partFinish;
    private Vector3 _movingPatrFinishPos;
    private int _solvedParts=0;
    [SerializeField] int TotalParts;
    public GameObject WinButton;

    private void Start() 
    {
        WinButton.SetActive(false);
    }
    public void StartMovingParts(GameObject part, Vector3 _finishPosition)
    {   
        _partFinish=false;
        _movingPatrFinishPos=_finishPosition;
        _pazzlePart=part;
        _isMove=true;
        _startPartPosX=_mousePosition.x-_pazzlePart.transform.localPosition.x;
        _startPartPosY=_mousePosition.y-_pazzlePart.transform.localPosition.y;
    } 
    private void Update() 
    {
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            _isMove=false;
            if(Mathf.Abs(_pazzlePart.transform.localPosition.x -_movingPatrFinishPos.x)<=15f&&
            (_pazzlePart.transform.localPosition.y -_movingPatrFinishPos.y)<=15f)
            {
                _pazzlePart.transform.position=new Vector3 (_movingPatrFinishPos.x,_movingPatrFinishPos.y,_movingPatrFinishPos.z);
                _partFinish=true;
                _solvedParts+=1;
                if (_solvedParts==TotalParts)
                {
                    WinButton.SetActive(true);
                }
                Debug.Log(_movingPatrFinishPos);
            }
        }
        _mousePosition=Input.mousePosition;
        if(_isMove&& !_partFinish)
        {
            _pazzlePart.transform.localPosition=new Vector3(_mousePosition.x-_startPartPosX,_mousePosition.y-_startPartPosY,0);
            Debug.Log(_mousePosition);
        }
    }
    
}
