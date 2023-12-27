using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Physics")]
    public float moveSpeed;
    public float jumpForce;

    private Vector2 _curMoveInput;
    private Rigidbody2D _rigidbody2D;

    private bool _isGround = false;
    #region Unity Flow
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        move();
    }
    #endregion

    #region InputAction Method
    private void move()
    {
        Vector3 dir = transform.right * _curMoveInput.x;
        dir *= moveSpeed;
        _rigidbody2D.AddForce(dir, ForceMode2D.Impulse);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Debug.Log("2");
            _curMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            Debug.Log("3");
            _curMoveInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    #endregion


}
