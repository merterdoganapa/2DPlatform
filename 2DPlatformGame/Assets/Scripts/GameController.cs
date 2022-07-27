using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private StatUI _coinUIController;
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
        int coinAmount = CoinController.Instance.GetCoinAmount();
        _coinUIController.UpdateStat(coinAmount);
    }

    public void UpdateCoinAmount(int coinAmount,bool isIncreasing)
    {
        int updatedAmount = isIncreasing ? CoinController.Instance.IncreaseCoinAmount(coinAmount) : CoinController.Instance.DecreaseCoinAmount(coinAmount);
        _coinUIController.UpdateStat(updatedAmount);
    }
    
    public void OnPauseButtonClick()
    {
        SceneManager.LoadScene(0);
    }

}
