using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace PlatformGame
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Animator _animator;
        [SerializeField] private float jumpForce;
        private bool _isTurnedLeft = false;
        private bool _isBlinking = false;
        private bool _canJump = true;

        private void Start()
        {
            
        
        }

        private void Update()
        {
            
            
            
            
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            _animator.SetFloat("Speed",Mathf.Abs(horizontalInput));
            if((horizontalInput < 0 && !_isTurnedLeft) || (horizontalInput > 0 && _isTurnedLeft))
                Flip();
            transform.Translate(new Vector3(horizontalInput * _movementSpeed,0,0) * Time.deltaTime);
            if(Input.GetKeyDown(KeyCode.Space))
                Jump();
            if (_isBlinking == false)
            {
                StartCoroutine(Blink());
            }
        }

        private IEnumerator Blink()
        {
            _isBlinking = true;
            _animator.SetBool("IsBlinking",true);
            yield return new WaitForSeconds(.5f);
            _animator.SetBool("IsBlinking",false);
            yield return new WaitForSeconds(4f);
            _isBlinking = false;
        }

        private void Flip()
        {
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            _isTurnedLeft = !_isTurnedLeft;
        }

        private void Jump()
        {
            if (!_canJump) return;
            _animator.SetBool("IsJumping",true);
            _rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            _canJump = false;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Ground"))
            {
                _animator.SetBool("IsJumping",false);
                if (!_canJump)
                    _canJump = true;
                
            }
        }
    }
}