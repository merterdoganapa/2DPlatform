using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthPotion : MonoBehaviour,ICollectable
{
    [SerializeField] private int healthAmount;
    [SerializeField] private AudioClip _clip;
    
    public void Collect()
    {
        Health.Instance.Heal(healthAmount);
        AudioSource.PlayClipAtPoint(_clip,transform.position,1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Collect();
    }
}

public interface ICollectable
{
    void Collect();
}

