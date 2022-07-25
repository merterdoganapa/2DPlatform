using System;
using System.Collections;
using System.Collections.Generic;
using Brainclude.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class RewardPanel : MonoBehaviour,IRewardAdListener
{
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private TextMeshProUGUI rewardMultiplierText;
    private int multiplier = 2;

    public void Open(string rewardAmount,int multiplier)
    {
        gameObject.SetActive(true);
        rewardText.text = rewardAmount;
        this.multiplier = multiplier;
        rewardMultiplierText.text = "x" + multiplier;
    }
    
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnRewardButtonClick()
    {
        int rewardAmount = Convert.ToInt16(rewardText.text);
        GameController.Instance.UpdateCoinAmount(rewardAmount,true);
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
        GameController.Instance.UpdateCoinAmount(rewardAmount,true);
    }
}
