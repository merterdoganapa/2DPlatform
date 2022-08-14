using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    protected virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
