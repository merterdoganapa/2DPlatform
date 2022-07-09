using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    [SerializeField] private Transform _spriteTransform;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3 _rotationDirection;
    
    private void Update()
    {
        _spriteTransform.Rotate(_rotationDirection * _rotationSpeed);
    }

    public void InverseRotationDirection()
    {
        Vector3 tmp = _rotationDirection;
        tmp.z *= -1;
        _rotationDirection = tmp;
    }
}
