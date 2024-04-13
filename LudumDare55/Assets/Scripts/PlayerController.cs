using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D _rigidbody2d;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 _velocity = new Vector2(0f, 0f);
    public float velocityX = 0.1f;

    public float thrust = 1f;
    public bool isGrounded = false;
    private bool _isFlipped;
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
        if (Input.GetKeyDown("d"))
        {
            _velocity.x += velocityX;
        }
        else if (Input.GetKeyUp("d"))
        {
            _velocity.x -= velocityX;
        }

        if (Input.GetKeyDown("a"))
        {
            _velocity.x -= velocityX;
        }
        else if (Input.GetKeyUp("a"))
        {
            _velocity.x += velocityX;
        }

        if ((Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            _rigidbody2d.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    // Frame-rate independent update cycle
    private void FixedUpdate()
    {
        _animator.SetBool(IsWalking, _velocity.sqrMagnitude > 0f);
        if (_velocity.x != 0f)
        {
            _spriteRenderer.flipX = _velocity.x < 0f;
        }

        gameObject.transform.Translate(_velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if the collsion is happening with a game object with "ground" tag.
        if (collision.collider.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //check if the collsion is happening with a game object with "ground" tag.
        if (collision.collider.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }
}
