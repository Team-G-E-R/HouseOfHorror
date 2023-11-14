using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keydelete : MonoBehaviour
{
    
    public void Delete()
    {
        var player = GameObject.FindWithTag("Player").transform;
        if (player != null)
        {
            
            Destroy(gameObject);

        }
    }
}
