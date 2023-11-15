using UnityEngine;

public class OnTriggerInteractable : Interactable
{
    private void OnTriggerEnter(Collider other)
    {
        Interact();
    }
}
