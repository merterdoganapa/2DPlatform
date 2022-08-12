using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private Transform spriteTransform;
    
    public void RotateBack()
    {
        Vector3 rotation = spriteTransform.eulerAngles;
        spriteTransform.eulerAngles = new Vector3(rotation.x, rotation.y, rotation.z * -1);
    }
}
