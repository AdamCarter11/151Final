using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //inspector variables
    [SerializeField] private float speed, jumpForce, checkRadius;
    [SerializeField] private int resetJumps;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask whatIsGround;

    //hidden variables
    private float moveInput;
    private Rigidbody2D rb;
    private bool isGrounded;
    private int extraJumps;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = resetJumps;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(isGrounded){
            extraJumps = resetJumps;
        }
        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0){
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded){
            rb.velocity = Vector2.up * jumpForce;
        }

        if(Input.GetKey(KeyCode.S)){
            rb.gravityScale = 7f;
        }
        else{
            rb.gravityScale = 3f;
        }

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }
}
