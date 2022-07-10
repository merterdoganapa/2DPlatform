using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    [SerializeField] private Transform _spriteTransform;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3 _rotationDirection;
    [SerializeField] private bool loop = true;
    [SerializeField] private float maxRotValue;

    private void Update()
    {
        if(!loop)
        {
            var absRotValue = Mathf.Abs(maxRotValue);
            if (_spriteTransform.rotation.z < absRotValue * -1 || _spriteTransform.rotation.z > absRotValue)
            {
                InverseRotationDirection();
            }
        }
        _spriteTransform.Rotate(_rotationDirection * _rotationSpeed);
    }

    public void InverseRotationDirection()
    {
        Vector3 tmp = _rotationDirection;
        tmp.z *= -1;
        _rotationDirection = tmp;
    }
}