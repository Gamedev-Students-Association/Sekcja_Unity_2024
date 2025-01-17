using System;
using UnityEngine;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, PlayerInput.IGameplayActions
{
    private PlayerInput _playerInput;

    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();
            _playerInput.Gameplay.SetCallbacks(this);
            
            SetGameplay();
        }
    }

    private void OnDisable() {
        DisableGameplay();
    }

    public void SetGameplay()
    {
        _playerInput.Gameplay.Enable();
        // + disable other input, like an UI one, if present
        // analogous function needed for the other input system
    }

    public void DisableGameplay()
    {
        _playerInput.Gameplay.Disable();
        // temp solution bc Unity screams at me about memory leaks
    }

    public event Action<Vector2> MoveEvent;
    public event Action JumpEvent;
    public event Action JumpCancelledEvent;

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // Debug.Log($"Phase: {context.phase}, Value: {context.ReadValue<Vector2>()}");
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }
        if (context.phase == UnityEngine.InputSystem.InputActionPhase.Canceled)
        {
            JumpCancelledEvent?.Invoke();
        }
    }

}
