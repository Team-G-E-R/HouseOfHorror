using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class CameraSwitch : MonoBehaviour
{
    private Collider switchCollider;
    [SerializeField] Camera[] cameras;
    private static int CurrentCameraIndex;
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
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(CamSwitcher(other));
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        //StartCoroutine(CamSwitcher(other));

    }
    private IEnumerator CamSwitcher(Collider other)
    {
        Debug.Log("Entered CamTrigger");
        if (other.gameObject.tag == "CamChecker")
        {
            CurrentCameraIndex = ThisCameraIndex;
            Debug.Log("Switching to Cam" + ThisCameraIndex);
            yield return new WaitForSeconds(0.1f);
            if (CurrentCameraIndex == ThisCameraIndex)
            {
                for (int i = 0; i < cameras.Length; i++)
                {
                    cameras[i].gameObject.SetActive(i == ThisCameraIndex);
                }
            }
        }

    }
}
