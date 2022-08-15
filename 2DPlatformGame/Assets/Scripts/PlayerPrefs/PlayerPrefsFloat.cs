using System;
using UnityEngine;

public class PlayerPrefsFloat : BasePlayerPrefs , IDigitalPlayerPrefs
{
    public PlayerPrefsFloat() : base(typeof(float))
    {
        
    }

    public override void SetValue(string key, object value) => PlayerPrefs.SetFloat(key, (float)value);

    public override object GetValue(string key) => PlayerPrefs.GetFloat(key);

    public void IncreaseValue(string key, object increaseAmount)
    {
        float currentValue = PlayerPrefs.GetFloat(key);
        PlayerPrefs.SetFloat(key, currentValue + (float) increaseAmount);
    }

    public void DecreaseValue(string key, object decreaseAmount)
    {
        float currentValue = PlayerPrefs.GetFloat(key);
        PlayerPrefs.SetFloat(key, currentValue - (float) decreaseAmount);
    }
}