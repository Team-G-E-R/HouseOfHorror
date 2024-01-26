using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Event that will activate after interact/dialogue")]
    [Space(20)]
    public UnityEvent InteractAction;
    [Tooltip("Destroy this object after using")]
    public bool _oneUsage;

    public void Interact()
    {
        if (_oneUsage)
        {
            InteractAction.Invoke();
            Destroy(this);
            //DestroyInteractable();
        }
        else InteractAction.Invoke();
    }

    public void DestroyInteractable()
    {
        Destroy(gameObject);
    }
}