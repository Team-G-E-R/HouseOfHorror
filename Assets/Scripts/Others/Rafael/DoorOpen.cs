using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void Activated()
    {
        animator.SetTrigger("Door");
        Debug.Log("Door Opened");
    }
}
