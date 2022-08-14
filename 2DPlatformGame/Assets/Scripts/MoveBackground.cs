using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using DG.Tweening;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject[] layers;
    private Vector3 prevPosition;
    private bool isFirstFrame = true;

    private void Update()
    {
        if (isFirstFrame)
        {
            prevPosition = target.position;
            isFirstFrame = false;
            return;
        }

        Vector3 targetPosition = target.position;
        
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].transform.position += (targetPosition.x - prevPosition.x) * Vector3.left * (i + 1) * horizontalSpeed * Time.deltaTime;
            layers[i].transform.position += (targetPosition.y - prevPosition.y) * Vector3.down * (i + 1) *
                                           verticalSpeed * Time.deltaTime;
        }
        prevPosition = target.position;
    }
}