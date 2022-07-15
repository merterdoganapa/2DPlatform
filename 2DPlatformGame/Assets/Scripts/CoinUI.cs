using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinAmountText;

    public void UpdateCoin(int coinAmount)
    {
        coinAmountText.text = coinAmount.ToString();
    }
}
