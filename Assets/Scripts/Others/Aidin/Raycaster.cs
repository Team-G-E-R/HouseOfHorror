using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public event System.Action<Cell> OnCellHit;

    [SerializeField]
    private Camera _camera;

    public bool Locked {  get; set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Locked)
            return;
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition); 
            
            if (Physics.Raycast(ray, out hit))
            {
                Cell cell = hit.transform.GetComponentInParent<Cell>();
                if (cell != null)
                {
                    OnCellHit?.Invoke(cell);
                }
            }
        }
    }
}
