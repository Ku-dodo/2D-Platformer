using UnityEngine;

public class PlayerPhysicsHandler : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundGravityScale;
    [SerializeField] private float airGravityScale;

    [Header("Ray to Cheak Layer")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask waterLayer;

    [HideInInspector] public PlayerControlState playerControlState;

    [HideInInspector] public Vector2 CurMoveInput = Vector2.zero;
    [HideInInspector] public bool CurJumpInput = false;

    private Rigidbody2D _rigidbody2D;

    #region Unity Flow
    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        setGravity();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (waterLayer.value == (waterLayer.value | 1 << collision.gameObject.layer))
        {
            ResetPosition();
        }
    }

    #endregion

    #region Other Method

    private void Move()
    {
        Vector3 direction = transform.right * CurMoveInput.x;
        direction *= playerControlState == PlayerControlState.Able ? moveSpeed : 0;
        _rigidbody2D.AddForce(direction, ForceMode2D.Impulse);
    }

    private void Jump()
    {
        if (IsGround() && CurJumpInput)
        {
            float force = playerControlState == PlayerControlState.Able ? jumpForce : 0;
            _rigidbody2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
        CurJumpInput = false;
    }

    private bool IsGround()
    {
        if (Physics2D.BoxCast(transform.position, transform.localScale, 0f, Vector2.down, 0.1f))
        {
            return true;
        }
        return false;
    }

    private void setGravity()
    {
        if (IsGround())
        {
            _rigidbody2D.gravityScale = groundGravityScale;
        }
        else
        {
            _rigidbody2D.gravityScale = airGravityScale;
        }
    }

    private void ResetPosition()
    {
        transform.position = new Vector2(-4, -2.5f);
    }

    #endregion
}
