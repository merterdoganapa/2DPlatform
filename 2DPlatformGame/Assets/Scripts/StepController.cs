using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;
using UnityEngine.UI;

namespace PlatformGame
{
    public class StepController : MonoBehaviour
    {
        private StepCounter _stepCounter;
        [SerializeField] private StatUI _stepStatUI;
        [SerializeField] private int stepPeriod;
        [SerializeField] private int rewardAmount = 10;
        private int remainingStepAmount;
        private int previousStepAmount = 0;
        private int currentSteps = 0;
        private bool stepDetected = false;
        private static string playerPrefsStepString = "step_amount";
        private static StepController _instance;

        public static StepController Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<StepController>();
                return _instance;
            }
        }
        
        
        private void Start()
        {
            PlayerPrefsController.TryGenerateKey(playerPrefsStepString, 0);
            Setup();
            int stepAmount = GetStepAmount();
            _stepStatUI.UpdateStat(stepAmount);
        }

        private void Setup()
        {
            currentSteps = 0;
            previousStepAmount = 0;
            if (StepCounter.current != null)
            {
                _stepCounter = StepCounter.current;
                _stepCounter.samplingFrequency = 16f;
                InputSystem.EnableDevice(_stepCounter);
            }
        }

        
        private void Update()
        {
            #if !UNITY_EDITOR
            if (_stepCounter.enabled)
            {
                previousStepAmount = currentSteps; 
                currentSteps = _stepCounter.stepCounter.ReadValue();
                if (currentSteps <= 0)
                {
                    return;
                }

                if (stepDetected == false)
                {
                    stepDetected = true;
                    previousStepAmount = currentSteps;
                    int stepAmount = GetStepAmount();
                    remainingStepAmount = stepPeriod - (stepAmount % stepPeriod);
                }
                
                if (previousStepAmount != 0 && currentSteps > previousStepAmount)
                {
                    int delta = currentSteps - previousStepAmount;
                    int currentStepAmount = IncreaseStepAmount(delta);
                    _stepStatUI.UpdateStat(currentStepAmount);
                }
            }
            #endif

        }

        public int GetStepAmount() => PlayerPrefsController.TryGetValue<int>(playerPrefsStepString);
        
        public int IncreaseStepAmount(int amount) 
        { 
            int newValue = PlayerPrefsController.IncreaseValue(playerPrefsStepString, amount);
            _stepStatUI.UpdateStat(newValue);
            return newValue;
        }
        public int DecreaseStepAmount(int amount) 
        { 
            int newValue = PlayerPrefsController.DecreaseValue(playerPrefsStepString, amount);
            _stepStatUI.UpdateStat(newValue);
            return newValue;
        }
        public bool IsHaveEnoughSteps(int desiredAmount)
        {
            int currentAmount = GetStepAmount();
            return currentAmount >= desiredAmount;
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            if (_stepCounter == null) return;
            stepDetected = false;
            if (pauseStatus)
            {
                InputSystem.DisableDevice(_stepCounter);
            }
            else
            {
                Setup();
            }
        }
        
    }
}
