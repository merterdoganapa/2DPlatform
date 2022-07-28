using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statAmountText;
    public UnityEvent OnClick;
    public void UpdateStat(int statAmount)
    {
        statAmountText.text = statAmount.ToString();
    }

    public void OnStatButtonClick()
    {
        OnClick?.Invoke();
    }
    
    
}
