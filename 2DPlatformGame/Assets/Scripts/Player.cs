using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    
    public Health Health { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
        _healthSlider.maxValue = Health.MaxHealth;
        _healthSlider.value = Health.MaxHealth;
    }

    private void OnEnable()
    {
        Health.Damaged += OnTakeDamage;
    }

    private void OnDisable()
    {
        Health.Damaged -= OnTakeDamage;
    }

    private void OnTakeDamage(int damage)
    {
        _healthSlider.value = Health.CurrentHealth;
    }
}
