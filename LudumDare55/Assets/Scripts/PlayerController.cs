using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Spawn")] public AudioClip spawnSound;
    
    [Header("Horizontal Movement")]
    public float movementSpeed = 32f;
    public Vector2 direction;
    public bool freezePlayer = false;
    private bool _facingLeft;

    [Header("Vertical Movement")]
    public float jumpSpeed = 15f;
    public float jumpDelay = 0.25f;
    public AudioClip jumpSound;
    public AudioClip landingSound;
    private float _jumpTimer;

    [Header("Components")]
    public LayerMask groundLayerMask;
    private Rigidbody2D _rigidbody2d;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 1.1f;
    public Vector3 colliderOffset;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(spawnSound);
    }

    // Update is called once per frame
    private void Update()
    {
        var wasOnGround = onGround;
        var invertedColliderOffsetVertical = new Vector3(colliderOffset.x, - colliderOffset.y, 0f);

        var firstHit = Physics2D.Raycast(transform.position + invertedColliderOffsetVertical, Vector2.down, groundLength,
            groundLayerMask);
        var secondHit = Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayerMask);
        OnPossibleLiftEnter(firstHit, secondHit);
        onGround = firstHit || secondHit;
        Debug.DrawRay(transform.position + invertedColliderOffsetVertical, groundLength * Vector2.down, Color.blue);
        Debug.DrawRay(transform.position - colliderOffset, groundLength * Vector2.down, Color.blue);
        if (!wasOnGround && onGround)
        {
            // squeeze
            _audioSource.PlayOneShot(landingSound);
        }

        if (Input.GetButtonDown("Jump") && !freezePlayer)
        {
            _jumpTimer = Time.time + jumpDelay;
        }

        _animator.SetBool("onGround", onGround);
        if (freezePlayer)
        {
            direction = Vector2.zero;
        }
        else
        {
            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        UpdateHotkeys();
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

        _animator.SetFloat("horizontal", Mathf.Abs(_rigidbody2d.velocity.x));
        _animator.SetFloat("vertical", _rigidbody2d.velocity.y);
    }

    private void OnPossibleLiftEnter(RaycastHit2D left, RaycastHit2D right)
    {
        if (right.collider != null && right.collider.gameObject.GetComponent<Lift>() && onGround)
        {
            transform.parent = right.collider.transform;
            return;
        }
        if (left.collider != null && left.collider.gameObject.GetComponent<Lift>() && onGround)
        {
            transform.parent = left.collider.transform;
            return;
        }
        transform.parent = null;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Lift>())
        {
            transform.parent = null;
        }
    }

    private void Jump()
    {
        _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, 0);
        _rigidbody2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        _jumpTimer = 0;
        _audioSource.PlayOneShot(jumpSound);
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

    private void UpdateHotkeys()
    {
        if (Input.GetKeyUp("r"))
        {
            var currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
        }
    }
}
