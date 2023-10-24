using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Cell : MonoBehaviour
{
    public event System.Action<Cell, Vector3, Vector3> OnPositionChanged;

    [SerializeField]
    private TextMeshProUGUI text;

    private int number;

    private Vector3 start;

    private Vector3 end;

    private float startTime;

    private bool moving;
    
    public int Number
    {
        get
        {
            return number;
        }
        set
        {
            number = value;
            text.text = number.ToString();
        }
    }

    public void Move(float x,float y)
    {
        Debug.Log("3");
        start = transform.position;
        end = new Vector3(x, y, transform.position.z);
        startTime = Time.time;  
        moving = true;
    }
    void Start()
    {
        
    }

  
    void Update()
    {
        if (moving)
        {
            float t = (Time.time - startTime) / 0.25f;
            transform.position = Vector3.Lerp(start, end, t);
            Debug.Log("4");

            if (t > 1) 
            {
                moving = false;
                OnPositionChanged?.Invoke(this,start,end);
            }
        }
    }
}
