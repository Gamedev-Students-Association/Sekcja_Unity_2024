using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;

    private Vector2 _moveDirection;
    private bool _isJumping;

    private void Start()
    {
        input.MoveEvent += HandleMove;
        input.JumpEvent += HandleJump;
        input.JumpCancelledEvent += HandleJumpCancelled;
    }
    private void Update()
    {
        Move();
        Jump();
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
        transform.position += new Vector2(_moveDirection.x, 0) * (speed * Time.deltaTime);
    }
    private void Jump()
    {
        if (_isJumping)
        {
            transform.position += new Vector2(0, 1) * (jumpSpeed * Time.deltaTime);
        }
    }
}
