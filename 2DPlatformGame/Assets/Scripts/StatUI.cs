using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statAmountText;

    public void UpdateStat(int statAmount)
    {
        statAmountText.text = statAmount.ToString();
    }
}
