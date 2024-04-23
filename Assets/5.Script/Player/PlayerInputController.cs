using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerPhysicsHandler _physicsController;
    private PlayerInteractHandler _interactHandler;

    private void Start()
    {
        _physicsController = GetComponent<PlayerPhysicsHandler>();
        _interactHandler = GetComponent<PlayerInteractHandler>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _physicsController.CurMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _physicsController.CurMoveInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _physicsController.CurJumpInput = true;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (_physicsController.playerControlState == PlayerControlState.Disable)
            {
                return;
            }

            if (_interactHandler._interact != null)
            {
                _interactHandler._interact.InteractAction();
            }
        }
    }
}
