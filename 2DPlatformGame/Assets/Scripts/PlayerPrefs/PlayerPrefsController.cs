using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;


public static class PlayerPrefsController
{
    private static List<Type> _types;
    private static List<BasePlayerPrefs> _createdPrefs;

    static PlayerPrefsController()
    {
        _types = new List<Type>();
        _createdPrefs = new List<BasePlayerPrefs>();
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(BasePlayerPrefs).IsAssignableFrom(p));
        foreach (var type in types)
            if (!type.IsAbstract) { 
                _types.Add(type);
            }
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    private static BasePlayerPrefs GetPlayerPrefsInstanceByType(Type type)
    {
        foreach (var createdInstance in _createdPrefs)
        {
            if (createdInstance.type == type)
            {
                return createdInstance;
            }
        }
        return null;
    }

    private static BasePlayerPrefs Get<T>()
    {
        var basePlayerPrefs = GetPlayerPrefsInstanceByType(typeof(T));
        if (basePlayerPrefs == null)
        {
            for (int i = 0; i < _types.Count; i++)
            {
                var type = _types[i];
                basePlayerPrefs = (BasePlayerPrefs)Activator.CreateInstance(type);
                if (basePlayerPrefs.type == typeof(T))
                {
                    _createdPrefs.Add(basePlayerPrefs);
                    break;
                }
            }
        }

        return basePlayerPrefs;

        /*for (int i = 0; i < _types.Count; i++)
        {
            var type = _types[i];
            var basePlayerPrefs = (BasePlayerPrefs)Activator.CreateInstance(type);
            
            if (typeof(T) == basePlayerPrefs.type)
                return basePlayerPrefs;
            GC.SuppressFinalize(basePlayerPrefs);
        }*/

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

    public static T IncreaseValue<T>(string key, T increseAmount) where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
    {
        var basePlayerPrefs = Get<T>();
        if (basePlayerPrefs != null)
        {
            ((IDigitalPlayerPrefs)basePlayerPrefs).IncreaseValue(key, increseAmount);
            return TryGetValue<T>(key);
        }
        return default(T);
    }

    public static T DecreaseValue<T>(string key, T decreaseAmount)
    {
        var basePlayerPrefs = Get<T>();
        if (basePlayerPrefs != null)
        {
            ((IDigitalPlayerPrefs)basePlayerPrefs).DecreaseValue(key, decreaseAmount);
            return TryGetValue<T>(key);
        }

        return default(T);
    }
}