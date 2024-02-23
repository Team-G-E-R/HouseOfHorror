using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void Activated()
    {
        animator.SetTrigger("OPEN_THE_DOOR");
    }
}
