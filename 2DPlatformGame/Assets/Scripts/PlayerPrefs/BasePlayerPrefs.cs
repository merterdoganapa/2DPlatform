using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerPrefs
{
    public Type type;

    protected BasePlayerPrefs(Type type) => this.type = type;

    public abstract void SetValue(string key, object value);
    public abstract object GetValue(string key);

    public abstract void IncreaseValue(string key, object increaseAmount);
    public abstract void DecreaseValue(string key, object decreaseAmount);
}

public interface IDigitalPlayerPrefs
{
    public abstract void IncreaseValue(string key, object increaseAmount);
    public abstract void DecreaseValue(string key, object decreaseAmount);
}