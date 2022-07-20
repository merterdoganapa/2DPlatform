using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{ 
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetBool(string parameter,bool value)
    {
        _animator.SetBool(parameter,value);
    }

    public bool GetBool(string parameter)
    {
        return _animator.GetBool(parameter);
    }

    public void SetFloat(string parameter,float value)
    {
        _animator.SetFloat(parameter,value);
    }
    
}
