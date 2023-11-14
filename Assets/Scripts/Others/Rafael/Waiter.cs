using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour
{
    float timer = 0;
    bool timerReached = false;

    public void Update()
    {
        if (!timerReached)
            timer += Time.deltaTime;

        if (!timerReached && timer > 5)
        {
            Debug.Log("Done waiting");
            timerReached = true;
        }
    }

}
