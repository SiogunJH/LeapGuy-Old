using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    [SerializeField] GameObject particleGO;
    [SerializeField] AudioClip[] softLandingAudio;
    [SerializeField] AudioClip[] hardLandingAudio;
    [SerializeField] AudioClip[] wallBonkAudio;
    [SerializeField] AudioClip[] jumpAudio;
    AudioSource audioSource;
    ParticleSystem ps;
    bool isOnGround;
    float jumpHorizontal;
    float jumpVertical;
    float jumpStrength;
    Direction facing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        ps = particleGO.GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();

        facing = Direction.Right;
        jumpHorizontal = 6.5f;
        jumpVertical = 14.0f;
        jumpStrength = 0.1f;
        isOnGround = true;

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
        audioSource.clip = jumpAudio[Random.Range(0, jumpAudio.Length)];
        audioSource.Play();
    }

    // Set isOnGround and update animation variable
    void OnLanding(bool isLandingSoft)
    {
        // Set "Is on Ground"
        isOnGround = true;
        anim.SetBool("Is on Ground", isOnGround);

        if (!isLandingSoft)
        {
            // Generate particles
            particleGO.transform.position = new Vector2(transform.position.x, transform.position.y - 0.6f);
            ps.Play();

            // Play hard landing sound
            audioSource.clip = hardLandingAudio[Random.Range(0, hardLandingAudio.Length)];
            audioSource.Play();
        }
        else
        {
            // Play soft landing sound
            audioSource.clip = softLandingAudio[Random.Range(0, softLandingAudio.Length)];
            audioSource.Play();
        }
    }

    void OnBounce()
    {
        // Play wall bonk sound
        audioSource.clip = wallBonkAudio[Random.Range(0, wallBonkAudio.Length)];
        audioSource.Play();
    }

    // On Triggers
    void OnTriggerEnter2D(Collider2D other) { }
    void OnTriggerStay2D(Collider2D other) { }
    void OnTriggerExit2D(Collider2D other) { }

    // On Collisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Standable") && rb.velocity.magnitude < 0.01)
        {
            OnLanding(collision.relativeVelocity.y < 10);
        }
        else if (collision.gameObject.CompareTag("Bounceable"))
        {
            OnBounce();
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Standable") && !isOnGround && rb.velocity.magnitude < 0.01f)
        {
            OnLanding(false);
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
