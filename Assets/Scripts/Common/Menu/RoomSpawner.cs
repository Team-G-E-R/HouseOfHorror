using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private float _distanceBetweenRoom;
    [SerializeField] private int _countRoom;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _cameraSpeed;
    [SerializeField] private List<GameObject> _poolRoom = new List<GameObject>();

    [SerializeField] private Vector3 _lastPosition;

    private void Awake()
    {
        _lastPosition = new Vector3(0, 0, -_distanceBetweenRoom);
        for (int i = 0; i < _countRoom; i++)
        {
            var room = Instantiate(Resources.Load<GameObject>("Room"),
                _lastPosition,
                Quaternion.identity);
            _poolRoom.Add(room);
            _lastPosition = room.transform.position;
            _lastPosition.z += _distanceBetweenRoom;
        }

        _lastPosition.z -= _distanceBetweenRoom;
    }

    private void FixedUpdate()
    {
        _camera.transform.position += new Vector3(0, 0, _cameraSpeed * Time.deltaTime);
        if (Math.Abs(_camera.transform.position.z - _lastPosition.z) 
            < Math.Abs(_poolRoom[0].transform.position.z - _lastPosition.z)/2)
        {
            print(_lastPosition);
            print(_camera.transform.position.z);
            print(_poolRoom[0].transform.position.z);
            for (int i = 0; i < _poolRoom.Count / 4; i++)
            {
                Destroy(_poolRoom[i]);
                var room = Instantiate(Resources.Load<GameObject>("Room"));
                _lastPosition.z += _distanceBetweenRoom;
                room.transform.position = _lastPosition;
                _poolRoom.Add(room);
            }
            _poolRoom.RemoveRange(0,  _poolRoom.Count /4);
        }
    }
}