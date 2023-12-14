using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float speed = 12f;
    private float _horizontal;
    private float _vertical;
    private float _scrollWheel;
    private float _zoomingSpeed = 24f;
    
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            _horizontal = Input.GetAxis("Mouse X") * speed;
            _vertical = Input.GetAxis("Mouse Y") * speed;

            transform.Translate(_horizontal, 0, _vertical);
        }
        _scrollWheel = Input.GetAxis("Mouse ScrollWheel") * _zoomingSpeed;
        transform.Translate(0, _scrollWheel, 0);

    }
}
