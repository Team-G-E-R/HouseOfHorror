using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Event that will activate after interact/dialogue")]
    [Space(20)]
    public UnityEvent InteractAction;
    [Tooltip("Destroy this interactable after usage")]
    [SerializeField] private bool _oneUsage;

    public void Interact()
    {
        if (_oneUsage) Destroy(this);
        InteractAction.Invoke();
    }

    public void DestroyInteractable()
    {
        _oneUsage = true;
    }
}