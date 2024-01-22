using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject PlayerNewPos;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Camera;
    [SerializeField] GameObject CameraNewPos;
    [SerializeField] private bool _rotateCamera;
    [SerializeField] private int camX;
    [SerializeField] private int camY;
    [SerializeField] private int camZ;
    public void Teleport()
    {
        if (_rotateCamera)
        {
            Camera.transform.rotation = Quaternion.Euler(camX, camY, camZ);
        }
        else if (!_rotateCamera)
        {
            Camera.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        Player.transform.position = PlayerNewPos.transform.position;
        Camera.transform.position = CameraNewPos.transform.position;
    }
}

