using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnCollisionEnter2D(Collision2D col)
    {
        TakeDamage takeDamage = col.gameObject.GetComponent<TakeDamage>();
        if (takeDamage == null) return;
        takeDamage.Take(damage);
    }
}
