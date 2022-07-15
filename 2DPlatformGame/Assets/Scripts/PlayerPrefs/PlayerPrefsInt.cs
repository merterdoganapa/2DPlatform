using System;
using UnityEngine;

public class PlayerPrefsInt : BasePlayerPrefs
{
    public PlayerPrefsInt() : base(typeof(int))
    {
    }

    public override void SetValue(string key, object value) => PlayerPrefs.SetInt(key, Convert.ToInt32(value));

    public override object GetValue(string key) => PlayerPrefs.GetInt(key);

    public override void IncreaseValue(string key, object increaseAmount)
    {
        int currentValue = PlayerPrefs.GetInt(key);
        PlayerPrefs.SetInt(key, currentValue + (int) increaseAmount);
    }

    public override void DecreaseValue(string key, object decreaseAmount)
    {
        int currentValue = PlayerPrefs.GetInt(key);
        PlayerPrefs.SetInt(key, currentValue - (int) decreaseAmount);
    }
}