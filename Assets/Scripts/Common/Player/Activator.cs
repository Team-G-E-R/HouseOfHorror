using Common.Scripts;
using UnityEngine;

public class Activator : MonoBehaviour
{
    private bool _isInRange = false;
    private GameObject InteractItem;
    private bool key = false;
    
    [SerializeField] GameObject inputButtonImage;

    private void Awake()
   {
        inputButtonImage.SetActive(false);
   }
   
   private void Update() 
   {    
        if((_isInRange)&(Input.GetKeyDown(KeyCode.E)))
        {
            if ((InteractItem.TryGetComponent<Interactable>(out Interactable ob)))
            {
                ob.Interact();
                inputButtonImage.SetActive(false);
            }
        }
   }

   private void OnTriggerEnter(Collider other)
   {
        if ((other.tag == "Interactable"))
        {
            _isInRange = true;
            ShowPic();

            if (key == false) InteractItem = other.gameObject;
        }
   }

   public void ShowPic()
   {
       inputButtonImage.SetActive(true);
   }

   private void OnTriggerExit(Collider other) 
   {
        if ((other.tag == "Interactable"))
        {
               inputButtonImage.SetActive(false);
               InteractItem=null;
               _isInRange=false;  
        }
   }
}