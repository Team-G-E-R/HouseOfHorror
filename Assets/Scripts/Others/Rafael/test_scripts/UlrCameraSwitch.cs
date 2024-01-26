using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UlrCameraSwitch : MonoBehaviour
{
    [SerializeField] GameObject cam1;
    [SerializeField] GameObject cam2;
    [SerializeField] GameObject UltDomain;
    [SerializeField] GameObject Spell;
    private void Start()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            UltDomain.SetActive(true);
            Spell.SetActive(true);
        }
    }


}
