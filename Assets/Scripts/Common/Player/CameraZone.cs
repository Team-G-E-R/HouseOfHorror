using System.Collections;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [Tooltip ("FOV when entering the trigger")]
    [SerializeField] private float _fovEnd;
    [Tooltip ("Recommended to use around 0.001, less = faster")]
    [SerializeField] private float _timeToChangeFov;
    [Tooltip ("Recommended to use around 0.05")]
    [SerializeField] private float _valueToChangeFovBack;
    [Tooltip ("Recommended to use around 0.05")]
    [SerializeField] private float _valueToChangeFovEnter;
    [SerializeField] private bool _needToChangeRotation;
    [SerializeField] private Vector3 _valueToFinalRotation;
    [Tooltip ("Recommended to use around 0.001-0.005, more = faster")]
    [SerializeField] private float _timeToChangeRotation;

    private Camera _camera;
    private static float _cameraFovEnter;
    private static Quaternion _cameraRotationStart;
    private Quaternion _valueToFinalRotationQuaternion;
    static bool entered;

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
            entered = true;
            StartCoroutine(CameraRotation());
            StartCoroutine(CameraFOVEnter());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopAllCoroutines();
            entered = false;
            StartCoroutine(CameraRotation());
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

    private IEnumerator CameraRotation()
    {
        Quaternion b = _valueToFinalRotationQuaternion;
        Quaternion c = _cameraRotationStart;
        if (entered)
        {
            while (_camera.transform.rotation != _valueToFinalRotationQuaternion && entered)
            {
                Vector3 a = _camera.transform.rotation.eulerAngles;
                if (b.eulerAngles.x > a.x  ||
                b.eulerAngles.y > a.y  ||
                b.eulerAngles.z > a.z )
                {
                        if (c.eulerAngles.x < a.x || c.eulerAngles.y < a.y || c.eulerAngles.z < a.z) //меньше, хотя по факту больше :/
                        {
                            _camera.transform.rotation = c;
                        }
                        else
                        {
                            c = Quaternion.Slerp(c,
                                _valueToFinalRotationQuaternion, _timeToChangeRotation);
                            _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation,
                                _valueToFinalRotationQuaternion, _timeToChangeRotation);
                        }
                }
                else
                {
                    _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation,
                                _valueToFinalRotationQuaternion, _timeToChangeRotation);
                }
                yield return null;
            }
        }
        else if (!entered)
        {
            while (_camera.transform.rotation != _cameraRotationStart && !entered)
            {
                _camera.transform.rotation = Quaternion.
                    Slerp(_camera.transform.rotation, 
                        _cameraRotationStart, _timeToChangeRotation);
                yield return null;
            }   
        }
    }
}
