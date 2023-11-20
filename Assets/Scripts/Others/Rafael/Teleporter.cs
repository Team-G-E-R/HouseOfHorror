using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject PlayerNewPos;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Camera;
    [SerializeField] GameObject CameraNewPos;
    public void Teleport()
    {
        Player.transform.position = PlayerNewPos.transform.position;
        Camera.transform.position = CameraNewPos.transform.position;
    }
}

