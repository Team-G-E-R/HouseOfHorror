using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_text : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject text;
    public void ShowText()
    {
        text.SetActive(true);
        Invoke("HideText", 2);
    }
    public void HideText()
    {
        text.SetActive(false);
    }
}
