using System.Collections;
using System.Collections.Generic;
using Common.Scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PazzlePartsMove : MonoBehaviour
{
    private float _startPartPosX;
    private float _startPartPosY;
    public GameObject FinishOb;
    private Vector3 _finishPosition;
    
    private void OnMouseDown() 
    {
        if(Input.GetMouseButtonDown(0)&&(this.gameObject.transform.position!=FinishOb.transform.position))
        {
            _finishPosition=new Vector3(FinishOb.transform.position.x,FinishOb.transform.position.y,FinishOb.transform.position.z);
            Debug.Log(_finishPosition);
            FindObjectOfType<PazzleManager>().StartMovingParts(this.gameObject, _finishPosition);
        }
    }

   
}
