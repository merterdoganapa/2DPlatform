using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    public event Action<int> Damaged = delegate { };
    public event Action<int> Healed = delegate { };
    public event Action Killed = delegate { };

    [SerializeField] private PlayerData _playerData;

    int _maxHealth;
    public int MaxHealth => _playerData.maxHealth;

    int _currentHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if(value > _maxHealth)
            {
                value = _maxHealth;
            }
            _currentHealth = value;
        }
    }

    private static Health _instance;

    public static Health Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<Health>();
            return _instance;
        }
    }
    
    private void Awake()
    {
        _maxHealth = _playerData.maxHealth;
        CurrentHealth = _playerData.maxHealth;
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        Healed.Invoke(amount);
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        Damaged.Invoke(amount);

        if(CurrentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Killed.Invoke();    
        gameObject.SetActive(false);
    }
}
