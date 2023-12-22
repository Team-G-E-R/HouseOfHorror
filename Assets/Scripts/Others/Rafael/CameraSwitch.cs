using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] GameObject cam1;
    [SerializeField] GameObject cam2;
    private Collider switchCollider;
    private void Start()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
        switchCollider = this.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("tries to switch");
        if (other.tag == "Player")
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            Debug.Log("switched");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Tries to exit");
        if (other.tag == "Player")
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
            Debug.Log("exited");
        }
    }
}
