using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    Rigidbody2D rb;
    public bool isOnGround;
    float jumpHorizontal;
    float jumpVertical;
    float jumpStrength;
    Direction facing;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        facing = Direction.Right;
        jumpHorizontal = 4.0f;
        jumpVertical = 9.0f;
        isOnGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Direction Inputs
        if (Input.GetKeyDown(KeyCode.A) && isOnGround && facing == Direction.Right)
        {
            facing = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && isOnGround && facing == Direction.Left)
        {
            facing = Direction.Right;
        }

        // Jump Logic
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpStrength = 0.1f;
        }
        else if (Input.GetKey(KeyCode.Space) && jumpStrength < 1)
        {
            jumpStrength += Time.deltaTime;
        }


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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Terrain"))
        {
            isOnGround = true;
        }
    }
    void OnTriggerStay2D(Collider2D other) { }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Terrain"))
        {
            isOnGround = false;
        }
    }

    // On Collisions
    void OnCollisionEnter2D(Collision2D collision) { }
    void OnCollisionStay2D(Collision2D collision) { }
    void OnCollisionExit2D(Collision2D collision) { }

}
