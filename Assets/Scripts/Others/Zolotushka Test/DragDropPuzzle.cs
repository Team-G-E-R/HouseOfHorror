using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using System;
using Unity.VisualScripting;

public class DragDropPuzzle : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField] private UnityEngine.UI.Image[] _images;
    public GameObject SelectedPiece;
    public Vector3 _startedPos;
    private Canvas _canvas;

    void Start()
    {
        _canvas = gameObject.GetComponent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SelectedPiece = eventData.pointerCurrentRaycast.gameObject;
        _startedPos = Input.mousePosition - SelectedPiece.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        SelectedPiece.transform.position = Input.mousePosition - _startedPos;
    }
}
