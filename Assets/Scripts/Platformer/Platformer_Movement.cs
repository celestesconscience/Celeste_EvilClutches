using NUnit.Framework;
using Unity.ProjectAuditor.Editor.Core;
using UnityEngine;

public class Platformer_Movement : MonoBehaviour
{
    // Movement Variables
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float moveDir = 0f;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    public Transform groundCheck;
    public float groundCheckRadius = .2f;
    public LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow)) moveDir= -1f;
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow)) moveDir = 1f;
        if(
            (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) ||
            (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) 
        ) {

            moveDir = 0;
        }

        rb.linearVelocity = new Vector2(moveDir * moveSpeed, rb.linearVelocity.y);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    }
}
