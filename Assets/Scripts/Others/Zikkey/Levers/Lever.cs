using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Interactable))]
public class Lever : MonoBehaviour
{
    [SerializeField] private bool _current = false;
    [SerializeField] private bool _needed = true;

    public UnityEvent OnStateChanged = new UnityEvent();
    public bool Success => _current == _needed;

    private void Start()
    {
        Interactable interactable = GetComponent<Interactable>();
        interactable.InteractAction.AddListener(ToggleState);
    }

    public void ToggleState()
    {
        ChangeState(!_current);
    }

    public void ChangeState(bool state)
    {
        _current = state;
        OnStateChanged.Invoke();
    }
}
