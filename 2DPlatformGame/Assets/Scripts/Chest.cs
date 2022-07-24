using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private SpriteAnimationManager _spriteAnimationManager;
    [SerializeField] private float rewardAmount;
    private bool isOpen;
    private bool isCollectable = false;
    private bool isRewardCollected = false;
    
    private void Update()
    {
        if (isCollectable)
        {
            if (isOpen && !isRewardCollected)
            {
                Collect();
            }
            else if(!_spriteAnimationManager.IsStarted)
            {
                _spriteAnimationManager.StartAnimation();
            }
        }
    }

    private void Collect()
    {
        Debug.Log("Reward Amount : " + rewardAmount);
        isRewardCollected = true;
    }

    public void Open()
    {
        isOpen = true;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isCollectable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isCollectable = false;
        }
    }
}
