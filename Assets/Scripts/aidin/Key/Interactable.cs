using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent InteractAction;
    public Animator animator;
    // Update is called once per frameS
    public void Interact()
    {
        
        InteractAction.Invoke();
        animator.SetTrigger("DoorOpened");
    }
}