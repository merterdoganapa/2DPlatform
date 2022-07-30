using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinController : MonoBehaviour
{
    [SerializeField] private StatUI _coinStatUI;
    private static CoinController _instance;
    private static string playerPrefsCoinString = "coin_amount";

    public static CoinController Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<CoinController>();
            return _instance;
        }
    }

    private void Awake()
    {
        //DontDestroyOnLoad(this);
        PlayerPrefsController.TryGenerateKey(playerPrefsCoinString, 0);
    }

    private void Start()
    {
        int coinAmount = GetCoinAmount();
        _coinStatUI.UpdateStat(coinAmount);
    }

    public static void ResetCoin() => PlayerPrefsController.TrySetValue(playerPrefsCoinString, 0);

    public int GetCoinAmount() => PlayerPrefsController.TryGetValue<int>(playerPrefsCoinString);

    public int IncreaseCoinAmount(int amount)
    {
        int newValue = PlayerPrefsController.IncreaseValue(playerPrefsCoinString, amount);
        _coinStatUI.UpdateStat(newValue);
        return newValue;
    }

    public int DecreaseCoinAmount(int amount)
    {
        int newValue = PlayerPrefsController.DecreaseValue(playerPrefsCoinString, amount);
        _coinStatUI.UpdateStat(newValue);
        return newValue;
    }

    public bool IsHaveEnoughCoin(int desiredAmount)
    {
        int currentAmount = GetCoinAmount();
        return currentAmount >= desiredAmount;
    }
}