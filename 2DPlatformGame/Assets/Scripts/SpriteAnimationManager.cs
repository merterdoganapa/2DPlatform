using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteAnimationManager : MonoBehaviour
{
    [SerializeField] private int animationStartIndex;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] animationSprites;
    [SerializeField] private bool startOnAwake;
    [SerializeField] private float frameDelay;
    [SerializeField] private bool loop;
    [SerializeField] private UnityEvent OnCompleted;
    private bool isStarted;

    private void Start()
    {
        if(startOnAwake) StartAnimation();
    }

    public bool IsStarted
    {
        get
        {
            return isStarted;
        }
        set
        {
            isStarted = value;
        }
    } 
    
    public void StartAnimation()
    {
        StartCoroutine(StartAnimationTask());
    }

    private IEnumerator StartAnimationTask()
    {
        isStarted = true;
        var spriteIndex = animationStartIndex;
        do
        {
            for (int i = spriteIndex ; i < animationSprites.Length ; i++)
            {
                _spriteRenderer.sprite = animationSprites[i];
                yield return new WaitForSeconds(frameDelay);
            }
            spriteIndex = 0;
        } while (loop);
        OnCompleted?.Invoke();
        yield return null;
    }
    
}
