using System.Collections;
using System.Collections.Generic;
using Common.Scripts;
using UnityEngine;

public class PartTest : MonoBehaviour
{
    private bool _isMove;
    private Vector2 _mousePosition;
    private float _startPartPosX;
    private float _startPartPosY;
    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _isMove=true;
            _startPartPosX=_mousePosition.x-this.transform.localPosition.x;
            _startPartPosY=_mousePosition.y-this.transform.localPosition.y;
        }
    }

    private void Update() 
    {
        if(_isMove)
        {
            _mousePosition=Input.mousePosition;
            this.gameObject.transform.localPosition=new Vector2(_mousePosition.x/* -_startPartPosX */, _mousePosition.y/* -_startPartPosY */);

        }
    }
    
    
}
