using UnityEngine;
using UnityEngine.Events;

public class LeverMaster : MonoBehaviour
{
    [SerializeField] private Lever[] _levers;
    [SerializeField] private bool _canCompleteMultipleTimes = false;

    private bool _completed = false;

    public UnityEvent OnCompleted = new();

    private void Awake()
    {
        for (int i = 0; i < _levers.Length; i++)
            _levers[i].OnStateChanged.AddListener(UpdatePazzleState);
    }

    public void UpdatePazzleState()
    {
        if (_completed && _canCompleteMultipleTimes == false)
            return;

        bool success = true;

        for (int i = 0; i < _levers.Length; i++)
            if (_levers[i].Success == false)
                success = false;

        if (success)
        {
            _completed = true;
            OnCompleted.Invoke();
        }
    }

}
