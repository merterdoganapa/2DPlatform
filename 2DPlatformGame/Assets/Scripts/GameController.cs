using System;
using System.Collections;
using System.Collections.Generic;
using PlatformGame;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<GameController>();
            return _instance;
        }
    }
    
    public void ConvertStepsToCoin()
    {
        int stepAmount = StepController.Instance.GetStepAmount();
        CoinController.Instance.IncreaseCoinAmount(stepAmount);
        StepController.Instance.DecreaseStepAmount(stepAmount);
    }
    
    public void OnPauseButtonClick()
    {
        SceneManager.LoadScene(0);
    }

}
