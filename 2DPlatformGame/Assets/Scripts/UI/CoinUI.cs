using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUI : StatUI
{
    private void Start()
    {
        int coinAmount = CoinController.Instance.GetCoinAmount();
        UpdateStat(coinAmount);
    }

    public void OnEnable()
    {
        CoinController.Instance.OnCoinChanged += UpdateStat;
    }

    private void OnDisable()
    {
        CoinController.Instance.OnCoinChanged -= UpdateStat;
    }

}
