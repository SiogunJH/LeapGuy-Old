using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    public GameObject particleGO;
    ParticleSystem ps;
    public bool isOnGround;
    float jumpHorizontal;
    float jumpVertical;
    float jumpStrength;
    Direction facing;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        ps = particleGO.GetComponent<ParticleSystem>();

        facing = Direction.Right;
        jumpHorizontal = 6.5f;
        jumpVertical = 14.0f;
        jumpStrength = 0.1f;
        isOnGround = true;

        Physics2D.gravity = new Vector2(0.00f, -20.00f);
    }

    // Update is called once per frame
    void Update()
    {
        // Direction Inputs
        if (Input.GetKeyDown(KeyCode.A) && isOnGround && facing == Direction.Right)
        {
            facing = Direction.Left;
            sr.flipX = true;

        }
        else if (Input.GetKeyDown(KeyCode.D) && isOnGround && facing == Direction.Left)
        {
            facing = Direction.Right;
            sr.flipX = false;
        }

        // Jump Logic
        if (isOnGround)
        {
            if (Input.GetKeyUp(KeyCode.Space) || jumpStrength > 1)
            {
                Jump();
                jumpStrength = 0.1f;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                jumpStrength += Time.deltaTime;
            }
        }

        // Update Animation
        anim.SetFloat("Vertical Speed", rb.velocity.y);
    }

    void Jump()
    {
        if (facing == Direction.Left)
        {
            rb.velocity = new Vector2(-jumpHorizontal * jumpStrength, jumpVertical * jumpStrength);
        }
        else if (facing == Direction.Right)
        {
            rb.velocity = new Vector2(jumpHorizontal * jumpStrength, jumpVertical * jumpStrength);
        }
    }

    // On Triggers
    void OnTriggerEnter2D(Collider2D other) { }
    void OnTriggerStay2D(Collider2D other) { }
    void OnTriggerExit2D(Collider2D other) { }

    // On Collisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        // When the player hits the Standable object and stops moving
        if (collision.gameObject.CompareTag("Standable") && rb.velocity.magnitude < 0.01)
        {
            // Set "Is on Ground"
            isOnGround = true;
            anim.SetBool("Is on Ground", isOnGround);

            // Apply actions below only when player hits the Standable with 10+ velcity
            if (collision.relativeVelocity.y > 10)
            {
                particleGO.transform.position = new Vector2(transform.position.x, transform.position.y - 0.6f);
                ps.Play();
            }
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Standable") && !isOnGround && rb.velocity.magnitude < 0.01f)
        {
            isOnGround = true;
            anim.SetBool("Is on Ground", isOnGround);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Standable"))
        {
            isOnGround = false;
            anim.SetBool("Is on Ground", isOnGround);
        }
    }

}
