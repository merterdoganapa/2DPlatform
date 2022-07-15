using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame
{
    [RequireComponent(typeof(MovementPointManager))]
    public class MoveBetweenPoints : MonoBehaviour
    {
        [SerializeField] private Transform _movementPointsParent;
        [SerializeField] private float _movementSpeed;
        private Vector3[] _movementPoints;
        private int currentPointIndex = 0;
        private bool findNextPoint = true;
        private Vector3 direction = Vector3.zero;
        private MovementDirection _movementDirection;
        public UnityEvent OnReached;
        
        private void Awake()
        {
            if (_movementPointsParent == null) return;
            _movementDirection = MovementDirection.Next;
            var childCount = _movementPointsParent.childCount;
            _movementPoints = new Vector3[childCount];
            for (int i = 0 ; i < _movementPointsParent.childCount; i++)
            {
                var movementPoint = _movementPointsParent.GetChild(i);
                _movementPoints[i] = movementPoint.position;
            }
            
            Destroy(_movementPointsParent.gameObject);
        }
    
        private void FixedUpdate()
        {
            if (_movementPoints == null || _movementPoints.Length == 0) return; 
            if (findNextPoint)
            {
                direction = (_movementPoints[currentPointIndex] - transform.position).normalized;
                findNextPoint = false;
            }
            float distance = Vector3.Distance(transform.position, _movementPoints[currentPointIndex]);
            if (distance > 0.3f)
            {
                transform.position += direction * _movementSpeed;
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
                OnReached.Invoke();
            }
            else if (currentPointIndex == 0)
            {
                _movementDirection = MovementDirection.Next;
                OnReached.Invoke();
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
        
    }
}
