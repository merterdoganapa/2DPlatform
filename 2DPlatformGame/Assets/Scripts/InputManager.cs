using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public float Horizontal { get; set; }
    public event Action OnJumpButtonPressed = delegate { };
    public event Action OnMovementButtonPressed = delegate { };
    public event Action OnMovementButtonReleased = delegate { };
    private bool _isPressingHorizontalMovementButton = false;

    private void Update()
    {
        if (!_isPressingHorizontalMovementButton && Horizontal != 0)
        {
            if (Horizontal < 0)
            {
                IncreaseHorizontal();
            }
            else
            {
                DecreaseHorizontal();
            }
        }
    }

    public void OnJumpButtonClick()
    {
        OnJumpButtonPressed?.Invoke();
    }

    public void OnLeftButtonClick()
    {
        _isPressingHorizontalMovementButton = true;
        if (Horizontal > 0)
        {
            Horizontal = 0;
        }
        DecreaseHorizontal();
        OnMovementButtonPressed?.Invoke();
    }

    public void OnRightButtonClick()
    {
        _isPressingHorizontalMovementButton = true;
        if (Horizontal < 0)
        {
            Horizontal = 0;
        }
        IncreaseHorizontal();
        OnMovementButtonPressed?.Invoke();
    }

    public void StopHorizontalMovement()
    {
        _isPressingHorizontalMovementButton = false;
        OnMovementButtonReleased?.Invoke();
    }

    private void IncreaseHorizontal()
    {
        Horizontal += Time.deltaTime;
        Horizontal = Mathf.Min(Horizontal, 1);
    }

    private void DecreaseHorizontal()
    {
        Horizontal -= Time.deltaTime;
        Horizontal = Mathf.Max(Horizontal, -1);
    }
}