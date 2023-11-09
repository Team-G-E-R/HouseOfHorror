using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Activator : MonoBehaviour
{
    private bool _isInRange;
    public KeyCode InteractKey = KeyCode.E;
    /* public UnityEvent InteractAction; */
    private GameObject InteractItem;
    private bool key = false;
    
    [SerializeField] GameObject inputButtonImage;
   
   private void Awake()
   {
        inputButtonImage.SetActive(false);
   }
   
   private void Update() 
   {    
        if((_isInRange)&(Input.GetKeyDown(InteractKey)))
        {
            if ((InteractItem.TryGetComponent<Interactable>(out Interactable ob)))
            {
                 ob.Interact();
            }
            /* InteractAction.Invoke(); */
        }
        if ((_isInRange) & (Input.GetKeyDown(InteractKey)))
        {
            if ((InteractItem.TryGetComponent<Key>(out Key ob1)))
            {
                ob1.Interact1();
                inputButtonImage.SetActive(false);
            }
            /* InteractAction.Invoke(); */
        }


    }

   private void OnTriggerEnter(Collider other)
   {
        if ((other.tag == "Interactable"))
        {
            _isInRange =true;
            inputButtonImage.SetActive(true);
            if (key == false)
            {
                InteractItem = other.gameObject;
            }
           
            else 
            {
                Debug.Log("нет ключа");
            }
        }

        if ((other.tag == "Key"))
        {
            _isInRange = true;
            inputButtonImage.SetActive(true);
            InteractItem = other.gameObject;

            key = false;
        }
   }


   private void OnTriggerExit(Collider other) 
   {
        if ((other.tag == "Interactable"))
        {
               inputButtonImage.SetActive(false);
               InteractItem=null;
               _isInRange=false;  
        }
        else if ((other.tag == "Key"))
        {
            inputButtonImage.SetActive(false);
            InteractItem = null;
            _isInRange = false;
        }
    }
}