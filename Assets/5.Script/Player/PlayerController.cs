using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Physics")]
    public float moveSpeed;
    public float jumpForce;
    public float groundGravityScale;
    public float airGravityScale;

    [Header("Ray to Cheak Layer")]
    public LayerMask groundLayer;

    public PlayerControlState playerControlState;
    private Vector2 _curMoveInput;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    #region Unity Flow
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Physics2D.queriesStartInColliders = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (groundLayer.value == (groundLayer.value | 1 << collision.gameObject.layer))
        {
            _rigidbody2D.gravityScale = groundGravityScale;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (groundLayer.value == (groundLayer.value | 1 << collision.gameObject.layer))
        {
            _rigidbody2D.gravityScale = airGravityScale;
        }
    }
    #endregion

    #region  Method
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && playerControlState == PlayerControlState.Able)
        {
            _curMoveInput = context.ReadValue<Vector2>();
            if (_curMoveInput.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }
            _animator.SetBool("isWalk", true);
            
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _curMoveInput = Vector2.zero;
            _animator.SetBool("isWalk", false);
        }
    }

    private void Move()
    {
        Vector3 dir = transform.right * _curMoveInput.x;
        dir *= moveSpeed;
        _rigidbody2D.AddForce(dir, ForceMode2D.Impulse);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && playerControlState == PlayerControlState.Able)
        {
            if (IsGround())
            {
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private bool IsGround()
    {
        if (Physics2D.BoxCast(transform.position, transform.lossyScale, 0.2f, Vector2.down, transform.lossyScale.y / 2 + 0.1f, groundLayer))
        {
            return true;
        }
        return false;
    }
    #endregion
}
