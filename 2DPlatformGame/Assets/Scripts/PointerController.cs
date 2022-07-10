using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class PointerController : MonoBehaviour
{
    [SerializeField] private UnityEvent OnHold;
    [SerializeField] private UnityEvent OnRelease;
    private bool isHolding = false;

    private void FixedUpdate()
    {
        if(isHolding) OnHold.Invoke();
    }

    public void OnPointerDown()
    {
        isHolding = true;
    }

    public void OnPointerUp()
    {
        isHolding = false;
        OnRelease.Invoke();
    }


}
