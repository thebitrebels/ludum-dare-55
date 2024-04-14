using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float movementSpeed = 32f;
    public Vector2 direction;
    private bool _facingLeft;

    [Header("Vertical Movement")]
    public float jumpSpeed = 15f;
    public float jumpDelay = 0.25f;
    private float _jumpTimer;

    [Header("Components")]
    public LayerMask groundLayerMask;
    private Rigidbody2D _rigidbody2d;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 1.01f;
    public Vector3 colliderOffset;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        var wasOnGround = onGround;
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayerMask) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayerMask);

        if (!wasOnGround && onGround)
        {
            // squeeze
        }

        if (Input.GetButtonDown("Jump"))
        {
            _jumpTimer = Time.time + jumpDelay;
        }

        _animator.SetBool("onGround", onGround);
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    // Frame-rate independent update cycle
    private void FixedUpdate()
    {
        MoveCharacter(direction.x);

        if (_jumpTimer > Time.time && onGround)
        {
            Jump();
        }

        ModifyPhysics();
    }

    private void MoveCharacter(float horizontal)
    {
        _rigidbody2d.AddForce(Vector2.right * horizontal * movementSpeed);

        if ((horizontal > 0 && _facingLeft) || (horizontal < 0 && !_facingLeft))
        {
            Flip();
        }

        if (Mathf.Abs(_rigidbody2d.velocity.x) > maxSpeed)
        {
            _rigidbody2d.velocity = new Vector2(Mathf.Sign(_rigidbody2d.velocity.x) * maxSpeed, _rigidbody2d.velocity.y);
        }

        Debug.Log(Mathf.Abs(_rigidbody2d.velocity.x));
        _animator.SetFloat("horizontal", Mathf.Abs(_rigidbody2d.velocity.x));
        _animator.SetFloat("vertical", _rigidbody2d.velocity.y);
    }

    private void Jump()
    {
        _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, 0);
        _rigidbody2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        _jumpTimer = 0;

        // squeeze
    }

    private void ModifyPhysics()
    {
        var changingDirections = (direction.x > 0 && _rigidbody2d.velocity.x < 0) || (direction.x < 0 && _rigidbody2d.velocity.x > 0);

        if (onGround)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                _rigidbody2d.drag = linearDrag;
            }
            else
            {
                _rigidbody2d.drag = 0f;
            }
            _rigidbody2d.gravityScale = 0;
        }
        else
        {
            _rigidbody2d.gravityScale = gravity;
            _rigidbody2d.drag = linearDrag * 0.15f;
            if (_rigidbody2d.velocity.y < 0)
            {
                _rigidbody2d.gravityScale = gravity * fallMultiplier;
            }
            else if (_rigidbody2d.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                _rigidbody2d.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

    private void Flip()
    {
        _facingLeft = !_facingLeft;
        _spriteRenderer.flipX = _facingLeft;
    }
}
