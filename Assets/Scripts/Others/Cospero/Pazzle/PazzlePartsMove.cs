using UnityEngine;

public class PazzlePartsMove : MonoBehaviour
{
    public GameObject FinishOb;
    private Vector3 _finishPosition;
    
    private void OnMouseDown() 
    {
        if(Input.GetMouseButtonDown(0) && (this.gameObject.transform.position != 
        FinishOb.transform.position))
        {
            _finishPosition = new Vector3(FinishOb.transform.position.x,
            FinishOb.transform.position.y, FinishOb.transform.position.z);
            FindObjectOfType<PazzleManager>().StartMovingParts(this.gameObject, _finishPosition);
        }
    }
}
