using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{

    public UnityEvent InteractAction;


    public void Interact1()
    {
        Debug.Log("3st stage");
        InteractAction.Invoke();

    }
}
