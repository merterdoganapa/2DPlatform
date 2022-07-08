using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Deneme : MonoBehaviour
{
    private StepCounter _stepCounter;
    public Text deneme;

    private void Start()
    {
        _stepCounter = StepCounter.current;

        if (!Permission.HasUserAuthorizedPermission("android.permission.ACTIVITY_RECOGNITION"))
        {
            Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION");
        }
        
        InputSystem.EnableDevice(_stepCounter);
    }


    private void Update()
    {
        if (_stepCounter.enabled)
        {
            var currentSteps = _stepCounter.stepCounter.ReadValue();
            deneme.text = currentSteps.ToString();
        }
    }
}