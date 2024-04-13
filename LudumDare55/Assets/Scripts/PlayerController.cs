using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rigidBody2d;

    private Vector2 velocity = new Vector2(0f, 0f);
    public float velocityX = 0.1f;

    public float thrust = 1f;
    public bool isGrounded = false;

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("d"))
        {
            velocity.x += velocityX;
        }
        else if (Input.GetKeyUp("d"))
        {
            velocity.x -= velocityX;
        }

        if (Input.GetKeyDown("a"))
        {
            velocity.x -= velocityX;
        }
        else if (Input.GetKeyUp("a"))
        {
            velocity.x += velocityX;
        }

        if (Input.GetKeyDown("w") && isGrounded)
        {
            rigidBody2d.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    // Frame-rate independent update cycle
    private void FixedUpdate()
    {
        gameObject.transform.Translate(velocity);
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
