using UnityEngine;
using System;

public class PlayerPrefsString : BasePlayerPrefs
{
    public PlayerPrefsString() : base(typeof(string))
    {
    }

    public override void SetValue(string key, object value) => PlayerPrefs.SetString(key, Convert.ToString(value));

    public override object GetValue(string key) => PlayerPrefs.GetString(key);

}