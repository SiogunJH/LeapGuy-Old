using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagerLib;

public class PartolingKnightLogic : MonoBehaviour
{
    SpriteRenderer sr;
    Animator anim;
    [SerializeField] float walkingSpeed;
    [SerializeField] float partolRadius;
    [SerializeField] Vector2 startingPos;
    float attackStrength = 10;
    Direction facing = Direction.Right;
    bool isAttacking = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            Partol();
        }
    }

    void Partol()
    {
        // Stay withing borders
        if (facing == Direction.Right && transform.position.x > startingPos.x + partolRadius)
        {
            facing = Direction.Left;
            sr.flipX = true;

        }
        else if (facing == Direction.Left && transform.position.x < startingPos.x - partolRadius)
        {
            facing = Direction.Right;
            sr.flipX = false;
        }


        // Walk
        if (facing == Direction.Right)
        {
            transform.Translate(walkingSpeed * Time.deltaTime, 0, 0);
        }
        else if (facing == Direction.Left)
        {
            transform.Translate(-walkingSpeed * Time.deltaTime, 0, 0);
        }
    }

    void Attack(GameObject tartget)
    {
        isAttacking = true;
        anim.SetBool("Is Attacking", isAttacking);
        AudioManager.PlaySound("Bonk");

        if (tartget.transform.position.x < transform.position.x) // [TARGET] [PATROL KNIGHT]
        {
            tartget.GetComponent<Rigidbody2D>().velocity = new Vector2(-attackStrength, attackStrength * 0.5f);
        }
        else if (tartget.transform.position.x > transform.position.x) // [PATROL KNIGHT] [TARGET]
        {
            tartget.GetComponent<Rigidbody2D>().velocity = new Vector2(attackStrength, attackStrength * 0.5f);
        }
        Invoke("ResumePatrol", 0.3f);
    }

    void ResumePatrol()
    {
        isAttacking = false;
        anim.SetBool("Is Attacking", isAttacking);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (
            !isAttacking &&
            collision.gameObject.CompareTag("Player") && // colliding with player
            (
                (facing == Direction.Right && collision.gameObject.transform.position.x > transform.position.x) || // facing right and attacking to right
                (facing == Direction.Left && collision.gameObject.transform.position.x < transform.position.x) // facing left and attacking to left
            )
        )
        {
            Attack(collision.gameObject);
        }
    }
}
