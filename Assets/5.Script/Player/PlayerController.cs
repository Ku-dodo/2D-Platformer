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

    private Vector2 _curMoveInput;
    private Rigidbody2D _rigidbody2D;

    #region Unity Flow
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, transform.lossyScale);

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.lossyScale, 0.2f, Vector2.down, transform.lossyScale.y / 2 + 0.1f, groundLayer);

        if (hit.collider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(hit.point, transform.lossyScale);
        }
    }
    #endregion

    #region  Method
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _curMoveInput = context.ReadValue<Vector2>();
            Debug.Log(transform.right);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _curMoveInput = Vector2.zero;
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
        if (context.phase == InputActionPhase.Started)
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
