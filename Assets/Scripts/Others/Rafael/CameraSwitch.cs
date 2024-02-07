using System.Collections;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public static GameObject CurrentCamera;
    public static GameObject TempCurrentCamera;

    [SerializeField] private GameObject _enablingCamera;
    [SerializeField] private bool _main = false;

    private const float Delay = 0.5f;

	private void Awake()
	{
		if (_main)
			AttachCamera();
        else
            _enablingCamera.SetActive(false);
	}

	private void Update()
	{
		if (_main && (CurrentCamera == null || CurrentCamera.activeSelf == false))
        {
            AttachCamera();
        }

	}

	private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(CamSwitcher(other));
    }

    private IEnumerator CamSwitcher(Collider other)
    {
        if (other.gameObject.tag == "CamChecker")
        {
            TempCurrentCamera = _enablingCamera;
            yield return new WaitForSeconds(Delay);

            if (TempCurrentCamera == _enablingCamera)
                AttachCamera();
            else
            {
                if (AnyOpened())
				    _enablingCamera.SetActive(false);
			}
        }
    }

    private void AttachCamera()
    {
        if (CurrentCamera != null)
            CurrentCamera.SetActive(false);

		_enablingCamera.SetActive(true);
		TempCurrentCamera = _enablingCamera;
		CurrentCamera = _enablingCamera;
	}

    private bool AnyOpened()
    {
		CameraSwitch[] switches = FindObjectsByType<CameraSwitch>(FindObjectsInactive.Include, FindObjectsSortMode.None);

		foreach (CameraSwitch sw in switches)
		{
            if (sw.gameObject.activeSelf)
                return true;
		}

        return false;
	}
}
