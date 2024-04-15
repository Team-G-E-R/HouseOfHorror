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
        Activator player = GameObject.FindWithTag("Player").GetComponent<Activator>();
        player.OneUsage = _oneUsage;
        if (_oneUsage)
        {
            InteractAction.Invoke();
            Destroy(this);
        }
        else InteractAction.Invoke();
    }
}