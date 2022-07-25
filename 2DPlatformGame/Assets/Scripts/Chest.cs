using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using Brainclude.Common;
using UnityEngine;

public class Chest : MonoBehaviour 
{
    [SerializeField] private SpriteAnimationManager _spriteAnimationManager;
    [SerializeField] private RewardPanel rewardPanel;
    [SerializeField] private int rewardAmount;
    [SerializeField] private int rewardMultiplier;
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
        rewardPanel.Open(rewardAmount.ToString() , rewardMultiplier);
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
