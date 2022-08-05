using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float time;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void TryGiveDamage(GameObject targetObject)
    {
        Health health = targetObject.GetComponent<Health>();
        if (health == null) return;
        health.TakeDamage(damage);
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!(timer > time)) return;
        TryGiveDamage(collision.gameObject);
        timer = 0f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!(timer > time)) return;
        TryGiveDamage(collision.gameObject);
        timer = 0f;
    }
}
