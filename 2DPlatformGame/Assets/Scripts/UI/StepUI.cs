using System.Collections;
using System.Collections.Generic;
using PlatformGame;
using UnityEngine;

public class StepUI : StatUI
{
    private void Start()
    {
        int stepAmount = StepController.Instance.GetStepAmount();
        UpdateStat(stepAmount);
    }

    public void OnEnable()
    {
        StepController.Instance.OnStepChanged += UpdateStat;
    }
    
    private void OnDisable()
    {
        StepController.Instance.OnStepChanged -= UpdateStat;
    }
}
