using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        GameController.Instance.LoadNextLevel();
    }
}
