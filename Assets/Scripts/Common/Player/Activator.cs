using UnityEngine;

public class Activator : MonoBehaviour
{
    private bool _isInRange;
    public KeyCode InteractKey = KeyCode.E;
    private GameObject InteractItem;
    private bool key = false;
    
    [SerializeField] GameObject inputButtonImage;
   
   private void Awake()
   {
        inputButtonImage.SetActive(false);
   }
   
   private void Update() 
   {    
        if((_isInRange)&(Input.GetKeyUp(InteractKey)))
        {
            if ((InteractItem.TryGetComponent<Interactable>(out Interactable ob)))
            {
                ob.Interact();
                inputButtonImage.SetActive(false);
            }
        }
        
        if ((_isInRange) & (Input.GetKeyUp(InteractKey)))
        {
            if ((InteractItem.TryGetComponent<Key>(out Key ob1)))
            {
                ob1.Interact1();
                inputButtonImage.SetActive(false);
            }
        }
   }

   private void OnTriggerEnter(Collider other)
   {
        if ((other.tag == "Interactable"))
        {
            _isInRange =true;
            ShowPic();
            
            if (key == false) InteractItem = other.gameObject;
            else Debug.Log("нет ключа");
        }

        if ((other.tag == "Key"))
        {
            _isInRange = true;
            inputButtonImage.SetActive(true);
            InteractItem = other.gameObject;

            key = false;
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
        
        else if ((other.tag == "Key"))
        {
            inputButtonImage.SetActive(false);
            InteractItem = null;
            _isInRange = false;
        }
    }
}