using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _smoothTime = 0.3f;
    private Vector3 _offset;
    private Vector3 _velocity = Vector3.zero;
    
    void Start()
    {
        _offset = transform.position - _playerTransform.position;
    }
    
    private void LateUpdate()
    {
        Vector3 targetPosition = _playerTransform.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);

    }
}


