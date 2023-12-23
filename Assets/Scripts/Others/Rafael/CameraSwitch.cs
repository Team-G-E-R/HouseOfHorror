using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] bool NotNeedsMainCam;
    private Collider switchCollider;
    [SerializeField] Camera[] cameras;
    private int CurrentCameraIndex;
    [SerializeField] int ThisCameraIndex;
    [SerializeField] int NextCameraIndex;
    private void Start()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
            Debug.Log("ZeroingCams");
        }
    }
    private void SetMainCam()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        cameras[0].gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CamChecker")
        {
            cameras[ThisCameraIndex].gameObject.SetActive(true);
            Debug.Log("Switching to Cam" + ThisCameraIndex);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "CamChecker")
        {
            cameras[ThisCameraIndex].gameObject.SetActive(false);
            Debug.Log("Exiting Cam" + ThisCameraIndex);
        }
    }
}
