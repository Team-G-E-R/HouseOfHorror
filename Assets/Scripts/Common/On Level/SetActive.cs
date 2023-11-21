using UnityEngine;

public class SetActive : MonoBehaviour
{
    [SerializeField] private GameObject[] _whatToSetActive;
    [SerializeField] private GameObject[] _whatToSetInactive;

    public void TurnOn()
    {
        Cursor.visible = true;
        foreach (var obj in _whatToSetActive)
        {
            obj.SetActive(true);
        }
    }
    
    public void TurnOff()
    {
        foreach (var obj in _whatToSetInactive)
        {
            obj.SetActive(false);
        }
    }
}
