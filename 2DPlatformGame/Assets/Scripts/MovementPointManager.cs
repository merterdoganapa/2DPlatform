using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MovementPointManager : MonoBehaviour
{
    private Vector3[] _movementPoints;
    private int currentPointIndex = 0;
    private bool findNextPoint = true;
    private Vector3 direction = Vector3.zero;
    private MovementDirection _movementDirection;
    private void Awake()
    {
        _movementDirection = MovementDirection.Next;
        var movementPointParent = GetMovementPointParent();
        var childCount = movementPointParent.childCount;
        _movementPoints = new Vector3[childCount];
        for (int i = 0 ; i < movementPointParent.childCount; i++)
        {
            var movementPoint = movementPointParent.GetChild(i);
            _movementPoints[i] = movementPoint.position;
        }
        
        Destroy(movementPointParent.gameObject);
    }

    private void FixedUpdate()
    {
        if (findNextPoint)
        {
            direction = (_movementPoints[currentPointIndex] - transform.position).normalized;
            findNextPoint = false;
        }
        float distance = Vector3.Distance(transform.position, _movementPoints[currentPointIndex]);
        if (distance > 0.3f)
        {
            transform.position += direction * 0.5f;
        }
        else
        {
            UpdatePointIndex();
            findNextPoint = true;
        }
    }

    private void UpdatePointIndex()
    {
        if (currentPointIndex + 1 == _movementPoints.Length)
        {
            _movementDirection = MovementDirection.Previous;
        }
        else if (currentPointIndex == 0)
        {
            _movementDirection = MovementDirection.Next;
        }

        if (_movementDirection == MovementDirection.Next)
        {
            currentPointIndex++;
        }
        else if (_movementDirection == MovementDirection.Previous)
        {
            currentPointIndex--;
        }
        
    }

    private Transform GetMovementPointParent() => transform.Find("MovementPointParent");
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying) return;
        Transform movementPointParent = GetMovementPointParent();
        for (int i = 0 ; i < movementPointParent.childCount ; ++i)
        {
            var child = movementPointParent.GetChild(i);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(child.position,.65f);            
        }
        
        for (int i = 0 ; i < movementPointParent.childCount - 1; ++i)
        {
            var child = movementPointParent.GetChild(i);
            var nextChild = movementPointParent.GetChild(i + 1);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(child.position,nextChild.position);            
        }
    }
#endif
    
}

public enum MovementDirection
{
    Next,
    Previous,
}


#if UNITY_EDITOR
[CustomEditor(typeof(MovementPointManager))]
[System.Serializable]
class MovementPointEditor : Editor
{
    private   SerializedProperty myGameObject;
    public override void OnInspectorGUI()
    {
        MovementPointManager manager = (MovementPointManager) target;
        if (GUILayout.Button("Create Movement Point"))
        {
            GameObject movementPoint = new GameObject();
            Transform movementPointTransform = movementPoint.transform;
            movementPoint.name = "Movement Point";
            Transform movementPointParent = manager.transform.Find("MovementPointParent");
            movementPointTransform.SetParent(movementPointParent);
            movementPointTransform.position = manager.transform.position;
        }
    }
}
#endif
