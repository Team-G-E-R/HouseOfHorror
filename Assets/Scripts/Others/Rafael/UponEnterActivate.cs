using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class UponEnterActivate : MonoBehaviour
{
    [SerializeField] private Behaviour ComponentToActivate;

    
    private void OnTriggerEnter(Collider other)
    {
        ComponentToActivate.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        ComponentToActivate.enabled = false;
    }
}
