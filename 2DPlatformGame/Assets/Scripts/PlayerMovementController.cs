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
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                Jump();
            if (_isBlinking == false)
            {
                StartCoroutine(Blink());
            }
        }

        public void MoveLeft()
        {
            transform.Translate(new Vector3(-1f * _movementSpeed,0,0) * Time.deltaTime);
            if (!_isTurnedLeft)
            {
                Flip();
            }
            _animator.SetFloat("Speed",1);
        }

        public void MoveRight()
        {
            transform.Translate(new Vector3(_movementSpeed,0,0) * Time.deltaTime);
            if (_isTurnedLeft)
            {
                Flip();
            }
            _animator.SetFloat("Speed",1);
        }

        public void Stop()
        {
            _animator.SetFloat("Speed",0);
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

        public void Jump()
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

        private void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Ground"))
            {
                _canJump = false;
            }
        }
    }
}