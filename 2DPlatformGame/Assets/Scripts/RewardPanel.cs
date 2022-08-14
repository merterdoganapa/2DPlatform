using System;
using System.Collections;
using System.Collections.Generic;
using Brainclude.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class RewardPanel : PanelController,IRewardAdListener
{
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private TextMeshProUGUI rewardMultiplierText;
    private int multiplier = 2;
    private static RewardPanel _instance;

    public static RewardPanel Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<RewardPanel>(true);
            return _instance;
        }
    }

    public void Open(string rewardAmount,int multiplier)
    {
        base.Open();
        rewardText.text = rewardAmount;
        this.multiplier = multiplier;
        rewardMultiplierText.text = $"x{multiplier} Claim";
    }
    
    public void OnRewardButtonClick()
    {
        int rewardAmount = Convert.ToInt16(rewardText.text);
        CoinController.Instance.IncreaseCoinAmount(rewardAmount);
        Close();
    }

    public void OnMultiplierButtonClick()
    {
        AdsController.Instance.SetRewardListener(this);
        AdsController.Instance.ShowRewardedAd();
        Close();
    }

    public void OnRewardEarned()
    {
        int rewardAmount = Convert.ToInt16(rewardText.text) * multiplier;
        CoinController.Instance.IncreaseCoinAmount(rewardAmount);
    }
}
