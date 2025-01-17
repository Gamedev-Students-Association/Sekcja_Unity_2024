using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    [SerializeField] private UIManager uiManager;

    private Rigidbody2D m_Rigidbody2D;
    private Vector2 _moveDirection; // leaving is as vec2 instead of axis bc i assume the y axis can be useful in other interactions
    private Vector2 _currentMove;
    private bool _isJumping; // REMEMBER the longer you press space the higher the jump (up until an upper limit)
    private float _currentJumpHeight = 0.0f;
    
	private bool m_Grounded;            // Whether or not the player is grounded.
    
    private int coins;

    public UnityEvent OnLandEvent;

    private void Awake() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        input.MoveEvent += HandleMove;
        input.JumpEvent += HandleJump;
        input.JumpCancelledEvent += HandleJumpCancelled;
    }

    private void OnDisable() {
        input.MoveEvent -= HandleMove;
        input.JumpEvent -= HandleJump;
        input.JumpCancelledEvent -= HandleJumpCancelled;
    }

    private void Update() {
        _currentMove += Move();
        _currentMove += Jump();
    }

    private void FixedUpdate() {
        m_Rigidbody2D.MovePosition(transform.position + (Vector3)_currentMove);
        _currentMove = Vector2.zero;
        GroundCheck();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            uiManager.UpdateCoinDisplay(coins);
            Destroy(other.gameObject);
        }
    }

    private void GroundCheck() {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject == gameObject)
                continue;
            
            m_Grounded = true;
            if (!wasGrounded)
                OnLandEvent.Invoke();
        }
    }

    private void HandleMove(Vector2 dir)
    {
        _moveDirection = dir;
    }
    
    private void HandleJump()
    {
        if (m_Grounded)
            _isJumping = true;
    }
    
    private void HandleJumpCancelled()
    {
        _isJumping = false;
    }
    
    private Vector2 Move()
    {
        return new Vector3(_moveDirection.x * speed * Time.deltaTime, 0, 0);
    }
    
    private Vector2 Jump()
    {
        if (_currentJumpHeight >= maxJumpHeight || _isJumping == false)
        {
            _isJumping = false;
            _currentJumpHeight = 0.0f;
        }
        
        if (_isJumping)
        {
            _currentJumpHeight += jumpSpeed * Time.deltaTime;
            return new Vector3(0, jumpSpeed * Time.deltaTime, 0);
        }
        
        if (!_isJumping && !m_Grounded) {
            return new Vector3(0, -jumpSpeed * Time.deltaTime, 0);
        }
        
        return Vector2.zero;
    }

    private void OnDrawGizmosSelected() {
        if (!m_GroundCheck)
            return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_GroundCheck.position, k_GroundedRadius);
    }
}
