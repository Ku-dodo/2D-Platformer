using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimHandler : MonoBehaviour
{
    private PlayerPhysicsHandler _physicsController;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private void Start()
    {
        _physicsController = GetComponent<PlayerPhysicsHandler>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetMoveAnim();
    }

    private void SetMoveAnim()
    {
        if (_physicsController.playerControlState == PlayerControlState.Disable)
        {
            _animator.SetBool("isWalk", false);
            return;
        }

        if (_physicsController.CurMoveInput == Vector2.zero)
        {
            _animator.SetBool("isWalk", false);
            return;
        }
        else
        {
            _animator.SetBool("isWalk", true);
        }

        if (_physicsController.CurMoveInput.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_physicsController.CurMoveInput.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

}
