using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class xd : MonoBehaviour
{
    public LayerMask LayerMask;

    public BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        var checkPos = transform.position - new Vector3(0, 0.1f, 0);
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, .6f, LayerMask);
        var slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
        Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
        Debug.DrawRay(hit.point, hit.normal, Color.red);
    }
}
