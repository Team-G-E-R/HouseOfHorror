using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Interactable))]
public class Lever : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _stateInfo;
    [SerializeField] [Multiline] private string _textPreset = "Рычаг:\n{}";
    [SerializeField] private string[] _activationsPreset = new string[] { "поднят", "опущен" };

    [SerializeField] private bool _current = false;
    [SerializeField] private bool _needed = true;

    public UnityEvent OnStateChanged = new();
    public bool Success => _current == _needed;

    private void Start()
    {
        UpdateInfo();
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
        UpdateInfo();
        OnStateChanged.Invoke();
    }

    private void UpdateInfo()
    {
        _stateInfo.text = _textPreset.Replace("{}", _current ? _activationsPreset[0] : _activationsPreset[1]);
    }
}
