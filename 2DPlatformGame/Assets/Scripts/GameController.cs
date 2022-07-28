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

    private void Start()
    {
        
    }

    public void UpdateCoinAmount(int coinAmount,bool isIncreasing)
    {
        int updatedAmount = isIncreasing ? CoinController.Instance.IncreaseCoinAmount(coinAmount) : CoinController.Instance.DecreaseCoinAmount(coinAmount);
        
    }

    public void ConvertStepsToCoin()
    {
        int stepAmount = StepController.Instance.GetStepAmount();
        UpdateCoinAmount(stepAmount, true);
        StepController.Instance.DecreaseStepAmount(stepAmount);

    }
    
    public void OnPauseButtonClick()
    {
        SceneManager.LoadScene(0);
    }

}
