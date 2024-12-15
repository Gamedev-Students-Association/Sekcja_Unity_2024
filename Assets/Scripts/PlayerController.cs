using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;

    private Rigidbody2D m_Rigidbody2D;
    private Vector2 _moveDirection; // leaving is as vec2 instead of axis bc i assume the y axis can be useful in other interactions
    private bool _isJumping; // REMEMBER the longer you press space the higher the jump (up until an upper limit)
    private float _currentJumpHeight = 0.0f;
    
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.

    public UnityEvent OnLandEvent;
    private void Start()
    {
        input.MoveEvent += HandleMove;
        input.JumpEvent += HandleJump;
        input.JumpCancelledEvent += HandleJumpCancelled;

        m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
        {
			OnLandEvent = new UnityEvent();
        }
    }
    private void Update()
    {
        Move();
        Jump();

        if (_isJumping)
        {
            _currentJumpHeight += jumpSpeed * Time.deltaTime;
        }

        if (_currentJumpHeight >= maxJumpHeight || _isJumping == false)
        {
            _isJumping = false;
            _currentJumpHeight = 0.0f;
        }

        bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
    }
    private void HandleMove(Vector2 dir)
    {
        _moveDirection = dir;
    }
    private void HandleJump()
    {
        _isJumping = true;
    }
    private void HandleJumpCancelled()
    {
        _isJumping = false;
    }
    private void Move()
    {
        if (_moveDirection == Vector2.zero)
        {
            return;
        }
        transform.position += new Vector3(_moveDirection.x, 0, 0) * (speed * Time.deltaTime);
    }
    private void Jump()
    {
        if (_isJumping)
        {
            transform.position += new Vector3(0, 1, 0) * (jumpSpeed * Time.deltaTime);
        }
    }
}
