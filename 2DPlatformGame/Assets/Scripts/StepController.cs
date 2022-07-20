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
        public Text stepCount;
        [SerializeField] private int stepPeriod;
        private int remainingStepAmount;
        private int firstDetectedStepAmount = -1;
        private int previousStepAmount = 0;
        private int currentSteps = 0;
        
        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            remainingStepAmount = stepPeriod;
            if (StepCounter.current != null)
            {
                _stepCounter = StepCounter.current;
                _stepCounter.samplingFrequency = 16f;
                InputSystem.EnableDevice(_stepCounter);
            }
        }

        
        private void Update()
        {
            if (_stepCounter.enabled)
            {
                previousStepAmount = currentSteps; 
                currentSteps = _stepCounter.stepCounter.ReadValue();
                if (currentSteps <= 0)
                {
                    return;
                }
                if (firstDetectedStepAmount == -1)
                {
                    firstDetectedStepAmount = currentSteps;
                    GameObject.Find("AA").GetComponent<Text>().text = "İlk bulunan adım sayısı : " + currentSteps.ToString();
                }
                stepCount.text = "Toplam atılan adım sayısı : " + currentSteps.ToString();
                if (previousStepAmount != 0 && currentSteps > previousStepAmount)
                {
                    int delta = currentSteps - previousStepAmount;
                    remainingStepAmount -= delta;
                    //GameObject.Find("BB").GetComponent<Text>().text = $"Kalan Adım Sayısı : {remainingStepAmount}";
                    if (remainingStepAmount <= 0)
                    {
                        GameController.Instance.UpdateCoinAmount(10,true);
                        remainingStepAmount += stepPeriod;
                        //GameObject.Find("BB").GetComponent<Text>().text += " --- Guncellendi : " + remainingStepAmount;
                    }
                    GameObject.Find("BB").GetComponent<Text>().text = $"Kalan Adım Sayısı : {remainingStepAmount}";
                }
            }

            // if (_stepCounter.enabled == false)
            // {
            //     GameObject.Find("CC").GetComponent<Text>().text = "FALSE";
            // }
            // else
            // {
            //     GameObject.Find("CC").GetComponent<Text>().text = "TRUE";
            // }
        }
        
        

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                InputSystem.DisableDevice(_stepCounter);
            }
            else
            {
                Setup();
            }
            //true oldugunda adım sayısını kaydet
            //false oldugunda kaydedilen adım sayısıyla yeni deger karsılasıtır.
            
        }
    }
}