using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using Object = System.Object;


public static class PlayerPrefsController
{
    private static List<Type> _types;

    static PlayerPrefsController()
    {
        _types = new List<Type>();
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(BasePlayerPrefs).IsAssignableFrom(p));
        foreach (var type in types)
            if (!type.IsAbstract)
                _types.Add(type);
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    private static BasePlayerPrefs Get<T>()
    {
        for (int i = 0; i < _types.Count; i++)
        {
            var type = _types[i];
            var basePlayerPrefs = (BasePlayerPrefs) Activator.CreateInstance(type);
            if (typeof(T) == basePlayerPrefs.type)
                return basePlayerPrefs;
            GC.SuppressFinalize(basePlayerPrefs);
        }

        return null;
    }

    public static void TryGenerateKey<T>(string key, T defaultValue)
    {
        if (PlayerPrefs.HasKey(key)) return;
        var iPlayerPrefs = Get<T>();
        iPlayerPrefs?.SetValue(key, defaultValue);
    }

    public static T TryGetValue<T>(string key)
    {
        var basePlayerPrefs = Get<T>();
        if (basePlayerPrefs != null)
        {
            return (T) basePlayerPrefs.GetValue(key);
        }

        return default(T);
    }

    public static void TrySetValue<T>(string key, T value)
    {
        var basePlayerPrefs = Get<T>();
        basePlayerPrefs?.SetValue(key, value);
    }

    public static T IncreaseValue<T>(string key, T increseAmount)
    {
        var basePlayerPrefs = Get<T>();
        if (basePlayerPrefs != null)
        {
            basePlayerPrefs.IncreaseValue(key, increseAmount);
            return TryGetValue<T>(key);
        }

        return default(T);
    }

    public static T DecreaseValue<T>(string key, T decreaseAmount)
    {
        var basePlayerPrefs = Get<T>();
        if (basePlayerPrefs != null)
        {
            basePlayerPrefs.DecreaseValue(key, decreaseAmount);
            return TryGetValue<T>(key);
        }

        return default(T);
    }
}