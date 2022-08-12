using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour,ICollectable
{
    public AudioClip clip;

    public void Collect()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position,1);
        CoinController.Instance.IncreaseCoinAmount(1);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }
}