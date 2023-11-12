using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [Tooltip ("FOV when entering the trigger")]
    [SerializeField] private float _fovEnd;
    [Tooltip ("Recommended to use around 0.01")]
    [SerializeField] private float _timeToChangeFov;
    
    private Camera _camera;
    private bool _isLerp;
    private float _cameraFovEnter;

    private void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _cameraFovEnter = _camera.fieldOfView;
    }
    
    private void Update()
    {
        if (_isLerp) CameraBack();
        else CameraForward();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) _isLerp = true;
    }
    
    private void OnTriggerExit(Collider other)
    { 
        if(other.CompareTag("Player")) _isLerp = false;
    }
    
    private void CameraBack()
    {
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _fovEnd, _timeToChangeFov);
    }
    
    private void CameraForward()
    {
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _cameraFovEnter, _timeToChangeFov);
    }
}
