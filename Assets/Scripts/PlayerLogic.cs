using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using AudioManagerLib;

public class PlayerLogic : MonoBehaviour
{
    [Header("Particle Systems\n")]
    [SerializeField] GameObject dirtSplashGO;
    [SerializeField] GameObject goldenFountainGO;
    ParticleSystem dirtSplash;
    ParticleSystem goldenFountain;
    bool isOnGround;
    bool hasFinished;
    float jumpHorizontal;
    float jumpVertical;
    float jumpStrength;
    Direction facing;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        dirtSplash = dirtSplashGO.GetComponent<ParticleSystem>();
        goldenFountain = goldenFountainGO.GetComponent<ParticleSystem>();

        facing = Direction.Right;
        jumpHorizontal = 6.5f;
        jumpVertical = 14.0f;
        jumpStrength = 0.1f;
        isOnGround = true;
        hasFinished = false;

        Physics2D.gravity = new Vector2(0.00f, -20.00f);
    }

    void Update()
    {
        UpdateDirection(); // Direction Inputs        
        JumpLogic(); // Jump Logic

        anim.SetFloat("Vertical Speed", rb.velocity.y); // Update Animation
    }

    // Listen for LEFT and RIGHT inputs, and change player Direction accordingly
    void UpdateDirection()
    {
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
    }

    // When on ground, listen for JUMP input
    // Holding down JUMP input results in higher jumpStrength, but it cannot exceed 1
    // Minimum jumpStrength is 0.1
    void JumpLogic()
    {
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
        else
        {
            jumpStrength = 0.1f;
        }
    }

    // Jump in predetermined direction, based on player Direction
    void Jump()
    {
        // Jump in a direction
        if (facing == Direction.Left)
        {
            rb.velocity = new Vector2(-jumpHorizontal * jumpStrength, jumpVertical * jumpStrength);
        }
        else if (facing == Direction.Right)
        {
            rb.velocity = new Vector2(jumpHorizontal * jumpStrength, jumpVertical * jumpStrength);
        }

        // Play jump sound
        AudioManager.PlaySound("Jump");
    }

    // Actions to do on landing
    void OnLanding(bool isLandingSoft)
    {
        // Set "Is on Ground"
        isOnGround = true;
        anim.SetBool("Is on Ground", isOnGround);

        if (!isLandingSoft)
        {
            // Generate particles
            dirtSplashGO.transform.position = new Vector2(transform.position.x, transform.position.y - 0.6f);
            dirtSplash.Play();

            // Play hard landing sound
            AudioManager.PlaySound("Hard Landing");
        }
        else
        {
            // Play soft landing sound
            AudioManager.PlaySound("Soft Landing");
        }
    }

    // Actions to do on bonking your stupid fookin head on a wall
    void OnBounce()
    {
        // Play wall bonk sound
        AudioManager.PlaySound("Wall Bounce");
    }

    void OnSlide()
    {
        AudioManager.PlaySound("Slide");
    }
    void OnFinish()
    {
        UiLogic.LoadNextLevel();
    }

    // On Triggers
    void OnTriggerEnter2D(Collider2D other)
    {

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Finish") && isOnGround && !hasFinished)
        {
            // Prevent looping
            hasFinished = true;

            // Generate particles
            goldenFountainGO.transform.position = new Vector2(transform.position.x, transform.position.y - 0.7f);
            goldenFountain.Play();

            // Play sound
            AudioManager.PlaySound("Finish Dingle");

            // Load new level in 2 seconds
            Invoke("OnFinish", 2);
        }
    }
    void OnTriggerExit2D(Collider2D other) { }

    // On Collisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Standable") && !isOnGround && rb.velocity.magnitude < 0.01)
        {
            OnLanding(collision.relativeVelocity.y < 10);
        }
        else if (collision.gameObject.CompareTag("Bounceable"))
        {
            OnBounce();
        }
        else if (collision.gameObject.CompareTag("Slideable"))
        {
            OnSlide();
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Standable") && !isOnGround && rb.velocity.magnitude < 0.01f)
        {
            OnLanding(true);
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
