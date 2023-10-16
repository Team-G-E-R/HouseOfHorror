using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screamertrigger : MonoBehaviour
{
    public AudioSource scream;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            scream.Play();
        }
    }
}
