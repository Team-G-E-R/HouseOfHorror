using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Event that will activate after interact/dialogue")]
    [Space(20)]
    public UnityEvent InteractAction;

    public void Interact()
    {
        InteractAction.Invoke();
    }
}