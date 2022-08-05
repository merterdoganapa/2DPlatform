using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PlatformGame
{
    public class MovementPointManager : MonoBehaviour
    {
        public GameObject pointsParentPrefab;
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Application.isPlaying) return;
            Transform movementPointParent = transform.Find("MovementPointParent");
            if (movementPointParent == null) return;
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
    [Serializable]
    class MovementPointEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            MovementPointManager manager = (MovementPointManager) target;
            if (GUILayout.Button("Create Movement Point"))
            {
                GameObject movementPoint = new GameObject();
                Transform movementPointTransform = movementPoint.transform;
                movementPoint.name = "Movement Point";
                Transform movementPointParent = manager.transform.Find("MovementPointParent");
                if (movementPointParent == null)
                {
                    movementPointParent = new GameObject().transform;
                    movementPointParent.name = "MovementPointParent";
                    movementPointParent.SetParent(manager.transform);
                }
                movementPointTransform.SetParent(movementPointParent);
                movementPointTransform.position = manager.transform.position;
            }
        }
    }
    #endif
}
