using System;
using System.Collections;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [Tooltip ("FOV when entering the trigger")]
    [SerializeField] private float _fovEnd;
    [Tooltip ("Recommended to use around 10")]
    [SerializeField] private float _timeToChangeFov;
    
    private bool _isLerp = false;
    private Camera _camera;
    private float _cameraFovEnter;
    private CameraZone[] _allObjs;

    private void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _cameraFovEnter = _camera.fieldOfView;
        _allObjs = FindObjectsOfType<CameraZone>();
    }

    private void Update()
    {
        if (_isLerp) _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _cameraFovEnter, _timeToChangeFov);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var a in _allObjs)
        {
            a._isLerp = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _fovEnd, _timeToChangeFov);
    }
    
    private void OnTriggerExit(Collider other)
    {
        _isLerp = true;
    }
}
