using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Activator : MonoBehaviour
{
    
    private bool isInRange;
   public KeyCode InteractKey = KeyCode.E;
   /* public UnityEvent InteractAction; */
   private GameObject InteractItem;
    public bool key = false;
    
    [SerializeField] GameObject inputButtonImage;
   
   private void Start()
   {
     inputButtonImage.SetActive(false);
   }
   
   private void Update() 
   {    
        if((isInRange)&(Input.GetKeyDown(InteractKey)))
        {
           Debug.Log("1st stage");
           if ((InteractItem.TryGetComponent<Interactable>(out Interactable ob)))
           {
                ob.Interact();
           }
            
            /* InteractAction.Invoke(); */
        }
        if ((isInRange) & (Input.GetKeyDown(InteractKey)))
        {
            Debug.Log("2st stage");
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
            
            isInRange =true;
            inputButtonImage.SetActive(true);
            Debug.Log("4st stage");
            if (key == false)
            {
                InteractItem = other.gameObject;

            }
           
            else 
            {
                Debug.Log("нет ключа ");
            }
        }
            


            
       
        if ((other.tag == "key"))
        {

            isInRange = true;
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
               isInRange=false;  
        }
        if ((other.tag == "key"))
        {
            inputButtonImage.SetActive(false);
            InteractItem = null;
            isInRange = false;
        }
    }
}