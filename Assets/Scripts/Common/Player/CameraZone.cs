using System.Collections;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [Tooltip ("FOV when entering the trigger")]
    [SerializeField] private float _fovEnd;
    [Tooltip ("Recommended to use around 0.001")]
    [SerializeField] private float _timeToChangeFov;
    [Tooltip ("Recommended to use around 0.05")]
    [SerializeField] private float _valueToChangeFovBack;
    [Tooltip ("Recommended to use around 0.05")]
    [SerializeField] private float _valueToChangeFovEnter;
    [SerializeField] private bool _needToChangeRotation;
    [SerializeField] private Vector3 _valueToFinalRotation;
    [Tooltip ("Recommended to use around 0.003-0.005")]
    [SerializeField] private float _timeToChangeRotation;

    private Camera _camera;
    private static float _cameraFovEnter;
    private Quaternion _cameraRotationStart;
    private Quaternion _valueToFinalRotationQuaternion;

    private void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _cameraFovEnter = _camera.fieldOfView;
        _cameraRotationStart = _camera.transform.rotation;
        _valueToFinalRotationQuaternion = Quaternion.
        Euler(_valueToFinalRotation.x, _valueToFinalRotation.y, _valueToFinalRotation.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(CameraRotation(true));
            StartCoroutine(CameraFOVEnter());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(CameraRotation(false));
            StartCoroutine(CameraFOVBack());
        }
    }

    private IEnumerator CameraFOVBack()
    {
        if (_cameraFovEnter < _fovEnd)
        {
            while (_camera.fieldOfView >= _cameraFovEnter)
            {
                _camera.fieldOfView -= _valueToChangeFovBack;
                yield return new WaitForSeconds(_timeToChangeFov);
            }
        }
        else if (_cameraFovEnter > _fovEnd)
        {
            while (_camera.fieldOfView <= _cameraFovEnter)
            {
                _camera.fieldOfView += _valueToChangeFovBack;
                yield return new WaitForSeconds(_timeToChangeFov);
            }
        }
    }

    private IEnumerator CameraFOVEnter()
    {
        if (_cameraFovEnter < _fovEnd)
        {
            while (_camera.fieldOfView <= _fovEnd)
            {
                _camera.fieldOfView += _valueToChangeFovEnter;
                yield return new WaitForSeconds(_timeToChangeFov);
            }
        }
        else if (_cameraFovEnter > _fovEnd)
        {
            while (_camera.fieldOfView >= _fovEnd)
            {
                _camera.fieldOfView -= _valueToChangeFovEnter;
                yield return new WaitForSeconds(_timeToChangeFov);
            }
        }
    }

    private IEnumerator CameraRotation(bool entered)
    {
        if (entered)
        {
            while (_camera.transform.rotation != _valueToFinalRotationQuaternion)
            {
                 _camera.transform.rotation = Quaternion.
                    Slerp(_camera.transform.rotation,
                        _valueToFinalRotationQuaternion, _timeToChangeRotation);
                yield return null;
            }
        }
        else if (!entered)
        {
            while (_camera.transform.rotation != _cameraRotationStart)
            {
                _camera.transform.rotation = Quaternion.
                    Slerp(_camera.transform.rotation, 
                        _cameraRotationStart, _timeToChangeRotation);
                yield return null;
            }   
        }
    }
}
