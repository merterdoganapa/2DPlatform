using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationManager : MonoBehaviour
{
    [SerializeField] private int animationStartIndex;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] animationSprites;
    [SerializeField] private bool startOnAwake;
    [SerializeField] private float frameDelay;
    [SerializeField] private bool loop;
    
    private void Start()
    {
        if(startOnAwake) StartAnimation(loop);
    }
    
    public void StartAnimation(bool loop)
    {
        StartCoroutine(StartAnimationTask(loop));
    }

    private IEnumerator StartAnimationTask(bool loop)
    {
        var spriteIndex = animationStartIndex;
        while (this.loop || loop)
        {
            for (int i = spriteIndex ; i < animationSprites.Length ; i++)
            {
                _spriteRenderer.sprite = animationSprites[i];
                yield return new WaitForSeconds(frameDelay);
            }
            spriteIndex = 0;
        }
        yield return null;
    }
    
}
