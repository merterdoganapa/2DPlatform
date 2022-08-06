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

        [Header("Vertical Movement")] [SerializeField]
        private float jumpForce;

        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private float jumpDelay = 0.25f;
        [SerializeField] private float slopeCheckDistance;
        private bool isOnAir = false;
        private bool checkSlope = true;
        
        [Header("Physics")] [SerializeField] private float _maxSpeed = 7f;
        [SerializeField] private float _linearDrag = 4f;
        [SerializeField] private float gravity = 1;
        [SerializeField] private float fallMultiplier = 5f;
        [SerializeField] private PhysicsMaterial2D noFriction;
        [SerializeField] private PhysicsMaterial2D fullFriction;

        [Header("Components")] [SerializeField]
        private BoxCollider2D _boxCollider;

        [SerializeField] private Rigidbody2D _rb;

        [SerializeField] private LayerMask _platformLayerMask;
        private bool isPressingMovementButton = false;
        [Header("Collision")] public bool onGround = true;
        private Transform firstParent;

        [Header("Movement Animation")] [SerializeField]
        private AnimatorController _animatorController;

        private bool climbingLeftDirection = false;
        private bool climbingRightDirection = false;

        private bool _isBlinking = false;
        private bool isOnSlope = false;
        private float fallSpeed = 0;
        [SerializeField] private PlayerData data;
        [SerializeField] private InputManager _inputManager;


        private void OnEnable()
        {
            _inputManager.OnJumpButtonPressed += HandlePlayerJump;
            _inputManager.OnMovementButtonPressed += HandlePlayerMovement;
            _inputManager.OnMovementButtonReleased += Stop;
        }

        private void OnDisable()
        {
            _inputManager.OnJumpButtonPressed -= HandlePlayerJump;
            _inputManager.OnMovementButtonPressed -= HandlePlayerMovement;
            _inputManager.OnMovementButtonReleased -= Stop;
        }

        private void Start()
        {
            firstParent = transform.parent;
            _maxSpeed = data.maxMovementSpeed;
            jumpForce = data.jumpForce;
        }

        private void HandlePlayerMovement()
        {
            isPressingMovementButton = true;
            var horizontalInput = _inputManager.Horizontal;
            if ((horizontalInput > 0 && _isFacingLeft) || (horizontalInput < 0 && !_isFacingLeft))
            {
                Flip();
            }
            
            _animatorController.SetFloat("Speed", 1);

            _rb.velocity = IsGrounded() ? new Vector2(horizontalInput * _movementSpeed, _rb.velocity.y) : new Vector2(horizontalInput * (_movementSpeed / 1.5f) , _rb.velocity.y);
            
            if (Mathf.Abs(_rb.velocity.x) > _maxSpeed)
            {
                _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _maxSpeed, _rb.velocity.y);   
            }
        }

        private bool CanJump() => onGround && !isOnSlope;

        private void HandlePlayerJump()
        {
            if (!CanJump()) return;
            checkSlope = false;
            _animatorController.SetBool("IsJumping", true);
            _rb.velocity = Vector2.up * jumpForce;
        }

        private void Update()
        {
            bool wasGround = onGround;
            onGround = IsGrounded();
            if (checkSlope)
            {
                SlopeCheck();
            }

            #region JumpAnimation

            if (onGround)
            {
                if (!wasGround)
                {
                    _animatorController.SetBool("IsFalling", false);
                }

                isOnAir = false;
            }
            else
            {
                isOnAir = true;
                if (_animatorController.GetBool("IsJumping") == false && _rb.velocity.y > 0)
                {
                    _animatorController.SetBool("IsJumping", true);
                }
                else if (_animatorController.GetBool("IsFalling") == false && _rb.velocity.y < 0)
                {
                    _animatorController.SetBool("IsJumping", false);
                    _animatorController.SetBool("IsFalling", true);
                }
            }

            #endregion

            if (Input.GetKeyDown(KeyCode.Space))
            {
                HandlePlayerJump();
            }

            if (_isBlinking == false)
            {
                StartCoroutine(Blink());
            }

            ModifyPhysics();
        }

        private void SlopeCheck()
        {
            Vector2 slopeCheckPosition =
                new Vector2(transform.position.x, transform.position.y - _boxCollider.size.y / 3);
            RaycastHit2D slopeHitFront =
                Physics2D.Raycast(slopeCheckPosition,
                    Vector2.right, slopeCheckDistance, _platformLayerMask);
            RaycastHit2D slopeHitBack =
                Physics2D.Raycast(slopeCheckPosition,
                    Vector2.left, slopeCheckDistance, _platformLayerMask);
            //Debug.DrawLine(slopeCheckPosition,slopeCheckPosition + Vector2.right * 1 ,Color.red);
            //Debug.DrawLine(slopeCheckPosition,slopeCheckPosition + Vector2.left * 1,Color.blue);
            climbingRightDirection = false;
            climbingLeftDirection = false;
            if (slopeHitFront)
            {
                float angle = Vector3.Angle(slopeHitFront.normal, transform.up * -1);
                isOnSlope = angle > 130;
                climbingRightDirection = Mathf.Sign(transform.localScale.x) > 0;
                //Debug.Log("Left");
            }
            else if (slopeHitBack)
            {
                float angle = Vector3.Angle(slopeHitBack.normal, transform.up * -1);
                isOnSlope = angle > 130;
                climbingLeftDirection = Mathf.Sign(transform.localScale.x) < 0;
                //Debug.Log("Right");
            }
            else
            {
                isOnSlope = false;
            }
        }

        private void ModifyPhysics()
        {
            #region Jump Gravity

            if (!onGround)
            {
                if (_rb.velocity.y < 0)
                {
                    _rb.gravityScale = gravity * fallMultiplier;
                }
                else
                {
                    _rb.gravityScale = gravity;
                }
            }

            #endregion

            if (isOnSlope)
            {
                if (checkSlope && (climbingLeftDirection || climbingRightDirection))
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, 0);
                    _rb.gravityScale = gravity;
                }
                else
                {
                    _rb.gravityScale = 10;
                }

                _rb.sharedMaterial = !isPressingMovementButton ? fullFriction : noFriction;
            }
            else if (onGround)
            {
                _rb.sharedMaterial = !isPressingMovementButton ? fullFriction : noFriction;
                _rb.gravityScale = gravity;
            }
        }

        public void Stop()
        {
            isPressingMovementButton = false;
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            _animatorController.SetFloat("Speed", 0);
        }

        private IEnumerator Blink()
        {
            _isBlinking = true;
            _animatorController.SetBool("IsBlinking", true);
            yield return new WaitForSeconds(.5f);
            _animatorController.SetBool("IsBlinking", false);
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

        private bool IsGrounded()
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f,
                Vector2.down, 0.1f, _platformLayerMask);
            return raycastHit2D.collider != null;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("MovingGround"))
            {
                gameObject.transform.SetParent(col.gameObject.transform);
                if (IsGrounded())
                    _rb.velocity = new Vector2(0, _rb.velocity.y);
            }
            else if (col.gameObject.tag.Contains("Ground"))
            {
                checkSlope = true;
            }
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("MovingGround"))
            {
                gameObject.transform.SetParent(firstParent);
            }
        }
    }
}