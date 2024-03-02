using System.Collections;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [Tooltip ("FOV when entering the trigger")]
    [SerializeField] private float _fovEnd;
    [Tooltip ("Recommended to use around 10")]
    [SerializeField] private float _timeToChangeFov;
    [SerializeField] private float _valueToChangeFovBack;
    [SerializeField] private float _valueToChangeFovEnter;
    
    private bool _isLerp = true;
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(CameraFOVEnter());
            foreach (var a in _allObjs)
            {
                a._isLerp = false;
            }   
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //_camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, this._fovEnd, _timeToChangeFov);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(CameraFOVBack());
            foreach (var a in _allObjs)
            {
                a._isLerp = true;
            }  
        }
    }

    private IEnumerator CameraFOVBack()
    {
        if (_cameraFovEnter < _fovEnd)
        {
            while (_camera.fieldOfView >= _cameraFovEnter)
            {
                print(gameObject.name + " out");
                _camera.fieldOfView -= _valueToChangeFovBack;
                yield return new WaitForSeconds(_timeToChangeFov);
            }
        }
        else if (_cameraFovEnter > _fovEnd)
        {
            while (_camera.fieldOfView <= _cameraFovEnter)
            {
                print(gameObject.name + " out");
                _camera.fieldOfView += _valueToChangeFovBack;
                yield return new WaitForSeconds(_timeToChangeFov);
            }
        }
    }

    private IEnumerator CameraFOVEnter()
    {
        print(_cameraFovEnter);
        print(_fovEnd);
        if (_cameraFovEnter < _fovEnd)
        {
            print("in if");
            while (_camera.fieldOfView >= _fovEnd)
            {
                print(gameObject.name + " entered");
                _camera.fieldOfView -= _valueToChangeFovEnter;
                yield return new WaitForSeconds(_timeToChangeFov);
            }
        }
        else if (_cameraFovEnter > _fovEnd)
        {
            print("in if 2");
            while (_camera.fieldOfView <= _fovEnd)
            {
                print(gameObject.name + " entered");
                _camera.fieldOfView -= _valueToChangeFovEnter;
                yield return new WaitForSeconds(_timeToChangeFov);
            }
        }
    }
}
