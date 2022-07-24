using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip clip;

    private void Collect()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position,1);
        GameController.Instance.UpdateCoinAmount(1,true);
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