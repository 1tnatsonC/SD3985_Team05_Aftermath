using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{

    Rigidbody2D rb;
    [SerializeField] float speed = 1;
    float horizontalValue;

    Animator animator;
    [SerializeField] Transform groundCheckCollider;
    const float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    bool facingRight = true;
   [SerializeField] bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      
        horizontalValue = Input.GetAxisRaw("Horizontal");

        
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue);
    }

    void GroundCheck()
    {
        isGrounded = false;
        //Check if the GroundCheckObject is colliding with other 2D colliders that are in "Ground" Layer
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            isGrounded = true;
    }

    void Move(float dir)
    {
        
        float xVal = dir * speed * 100 * Time.fixedDeltaTime; //Set value of x using dir & speed
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y); //Create Vec2 for velocity
        rb.velocity = targetVelocity; //Set player'velocity


        if(facingRight && dir < 0) //if looking right & clicked left (flip to left)
        {
            transform.localScale = new Vector3 (-1,1,1);
            facingRight = false;
        }

        else if(!facingRight && dir > 0 ) //if looking left & clicked right (flip to right)
        {
            transform.localScale = new Vector3 (1,1,1);
            facingRight = true;
        }

        Debug.Log(rb.velocity.x); // 0 = Idle ; 6 = Walking

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x)); //Set the float xVelocity according to x value of RigidBody2D velocity

    }
}
