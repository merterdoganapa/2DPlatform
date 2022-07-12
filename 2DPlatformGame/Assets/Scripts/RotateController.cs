using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    [SerializeField] private Transform _spriteTransform;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool angleLimits = true;
    [SerializeField] private float maxRotValue;
    private float timer = 0;
    
    private void Update()
    {
        if (!angleLimits)
        {
            _spriteTransform.Rotate(-Vector3.forward * _rotationSpeed);
        }
        else
        {
            float angle = Mathf.Sin(timer) * maxRotValue;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            timer += Time.deltaTime;
        }
    }

    public void InverseRotation()
    {
        _rotationSpeed *= -1;
    }
}
