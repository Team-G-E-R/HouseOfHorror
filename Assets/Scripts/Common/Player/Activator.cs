using Common.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Activator : MonoBehaviour
{
     public bool OneUsage;
     public bool _isInRange = false;
     private static GameObject InteractItem;
     [SerializeField] GameObject inputButtonImage;
     private Animator _playerAnimator;
     private movement _playerMovement;

     private void Awake()
     {
          HidePic();
          _playerAnimator = gameObject.GetComponent<Animator>();
          _playerMovement = gameObject.GetComponent<movement>();
     }
   
     private void Update() 
     {
          if((_isInRange))
          {
               ShowPic();
               if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0)) & InteractItem.TryGetComponent<Interactable>(out Interactable ob))
               {
                    _isInRange = false;
                    HidePic();
                    ob.Interact();
                    _playerAnimator.SetFloat("Horizontal", _playerMovement._lastHorisontalInput / 2);
                    _playerAnimator.SetFloat("Vertical", _playerMovement._lastVerticalInput / 2);
                    if (ob._oneUsage)
                    {
                         OneUsage = true;
                         EndOfInteraction();
                    }
                    else
                    {
                         OneUsage = false;
                    }
               }
          }
          else
          {
               HidePic();
          }
     }

     private void OnTriggerEnter(Collider other)
     {
          if ((other.tag == "Interactable") & other.TryGetComponent<Interactable>(out Interactable ob))
          {
               _isInRange = true;
               InteractItem = other.gameObject;
          }
     }

     public void ShowPic()
     {
          _isInRange = true;
          inputButtonImage.SetActive(true);
     }
     public void HidePic()
     {
          inputButtonImage.SetActive(false);
     }

     private void OnTriggerExit(Collider other) 
     {
          if (other.tag == "Interactable")
          {
               EndOfInteraction();
          }   
     }

     public void EndOfInteraction()
     {
          InteractItem = null;
          _isInRange = false;
     }

     private void OnDisable()
     {
          _isInRange = false;
          inputButtonImage.SetActive(false);
     }
}