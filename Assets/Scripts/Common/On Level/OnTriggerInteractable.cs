using UnityEngine;

public class OnTriggerInteractable : Interactable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Interact();
        }
    }
}
