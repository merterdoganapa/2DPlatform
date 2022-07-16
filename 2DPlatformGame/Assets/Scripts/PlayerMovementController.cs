using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

namespace PlatformGame
{
    public class PlayerMovementController : MonoBehaviour
    {
        [Header("Horizontal Movement")] [SerializeField]
        private float _movementSpeed;
        private float horizontal = 0;
        private bool _isFacingLeft = false;
        
        [Header("Vertical Movement")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpDelay = 0.25f;
        private bool canJump = true;
        
        [Header("Physics")]
        
        [SerializeField] private float _maxSpeed = 7f;
        [SerializeField] private float _linearDrag = 4f;
        [SerializeField] private float gravity = 1;
        [SerializeField] private float fallMultiplier = 5f;
        

        [Header("Components")] [SerializeField]
        private BoxCollider2D _boxCollider;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Animator _animator;

        [SerializeField] private LayerMask _platformLayerMask;
        private bool _isBlinking = false;
        private bool decrease = false;
        [Header("Collision")] public bool onGround = true;

        
        private void Update()
        {
            bool wasGround = onGround;
            onGround = IsGrounded();
            if (wasGround == false && onGround == true)
            {
                SetBoolJumpAnimation(false);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(TryJump());
            }
            if (_isBlinking == false)
            {
                StartCoroutine(Blink());
            }

            /*
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }
            else
            {
                Stop();
            }
            
            if (decrease && horizontal != 0)
            {
                if (horizontal < 0)
                {
                    IncreaseHorizontal();  
                }
                else
                {
                    horizontal += Time.deltaTime * 5;
                    horizontal = Mathf.Min(horizontal, 0);
                    //DecreaseHorizontal();
                }
            }
*/
            ModifyPhysics();
        }

        public void OnJumpButtonClick()
        {
            StartCoroutine(TryJump());
        }
        
        private IEnumerator TryJump()
        {
            if (!canJump) yield break;
            if (onGround)
            {
                canJump = false;
                Jump();
                yield return new WaitForSeconds(jumpDelay);
                canJump = true;
            }
        }
        
        IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds) {
            Vector3 originalSize = transform.localScale;
            Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
            float t = 0f;
            while (t <= 1.0) {
                t += Time.deltaTime / seconds;
                transform.localScale = Vector3.Lerp(originalSize, newSize, t);
                yield return null;
            }
            t = 0f;
            while (t <= 1.0) {
                t += Time.deltaTime / seconds;
                transform.localScale = Vector3.Lerp(newSize, originalSize, t);
                yield return null;
            }

        }

        public void MoveLeft()
        {
            decrease = false;
            if (horizontal > 0)
            {
                horizontal = 0;
            }

            DecreaseHorizontal();
            if (!_isFacingLeft)
            {
                Flip();
            }

            MoveCharacter();
        }

        public void MoveRight()
        {
            decrease = false;
            if (horizontal < 0)
            {
                horizontal = 0;
            }

            IncreaseHorizontal();
            if (_isFacingLeft)
            {
                Flip();
            }

            MoveCharacter();
        }

        private void IncreaseHorizontal()
        {
            horizontal += Time.deltaTime;
            horizontal = Mathf.Min(horizontal, 1);
        }

        private void DecreaseHorizontal()
        {
            horizontal -= Time.deltaTime;
            horizontal = Mathf.Max(horizontal, -1);
        }

        private void MoveCharacter()
        {
            //_rb.AddForce(Vector2.right * horizontal * _movementSpeed);
            _rb.velocity = new Vector2(horizontal * _movementSpeed,_rb.velocity.y);
            _animator.SetFloat("Speed", 1);
            if (Mathf.Abs(_rb.velocity.x) > _maxSpeed)
            {
                _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _maxSpeed, _rb.velocity.y);
            }
        }

        private void ModifyPhysics()
        {
            bool changingDirections = (horizontal > 0 && _rb.velocity.x < 0) || (horizontal < 0 && _rb.velocity.x > 0);
            if (onGround)
            {
                /*if (Mathf.Abs(horizontal) < 0.8f || changingDirections)
                {
                    _rb.drag = _linearDrag;
                }
                else
                {
                    _rb.drag = 10;
                }*/
                _rb.gravityScale = 0;
            }
            else
            {
                _rb.gravityScale = gravity;
                //_rb.drag = _linearDrag * 0.35f;
                if (_rb.velocity.y < 0)
                {
                    _rb.gravityScale = gravity * fallMultiplier;
                }
                else if (_rb.velocity.y > 0)
                {
                    _rb.gravityScale = gravity * (fallMultiplier / 2);
                }
            }
            
        }

        public void Stop()
        {
            decrease = true;
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            _animator.SetFloat("Speed", 0);
        }

        private IEnumerator Blink()
        {
            _isBlinking = true;
            _animator.SetBool("IsBlinking", true);
            yield return new WaitForSeconds(.5f);
            _animator.SetBool("IsBlinking", false);
            yield return new WaitForSeconds(4f);
            _isBlinking = false;
        }

        private void Flip()
        {
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            _isFacingLeft = !_isFacingLeft;
        }

        public void Jump()
        {
            SetBoolJumpAnimation(true);
            _rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }

        private void SetBoolJumpAnimation(bool value)
        {
            _animator.SetBool("IsJumping", value);
        }

        private bool IsGrounded()
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f,
                Vector2.down, 0.1f, _platformLayerMask);
            return raycastHit2D.collider != null;
        }


        private void OnCollisionEnter2D(Collision2D col)
        {
            /*if (col.gameObject.tag.Contains("Ground"))
            {
                SetBoolJumpAnimation(false);
            }*/
            
            if (col.gameObject.CompareTag("MovingGround"))
            {
                gameObject.transform.SetParent(col.gameObject.transform);
            }
        }
        
        private void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("MovingGround"))
            {
                gameObject.transform.SetParent(transform.parent.parent);
            }
        }
    }
}