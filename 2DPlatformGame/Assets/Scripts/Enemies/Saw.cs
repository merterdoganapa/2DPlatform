using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Transform spriteTransform;


    private void Start()
    {
    }

    private void Update()
    {
        spriteTransform.Rotate(Vector3.forward);
    }
}


