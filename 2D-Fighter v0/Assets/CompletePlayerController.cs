﻿
using UnityEngine;
using System.Collections;

public class CompletePlayerController : MonoBehaviour
{

    public float speed;                //Floating point variable to store the player's movement speed.
    public float jumpForce;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int extraJumps;

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private Animator anim;
    private SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Use the  float to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, 0);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(rb2d.velocity.y > 0){
            anim.SetBool("IsJumping", true);
            anim.SetBool("IsFalling", false);
        }
        if (rb2d.velocity.y < 0)
        {
            anim.SetBool("IsFalling", true);
            anim.SetBool("IsJumping", false);
        }
        if (rb2d.velocity.y < .0001 && rb2d.velocity.y > -.0001)
        {
            anim.SetBool("IsFalling", false);
            anim.SetBool("IsJumping", false);
        }

        if (moveHorizontal == 0)
        {
            anim.SetBool("IsRunning", false);
        } 
        else
        {
            anim.SetBool("IsRunning", true);
        }

        if(moveHorizontal < 0)
        {
            sprite.flipX = true;
        } 
        if(moveHorizontal > 0)
        {
            sprite.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            
            anim.SetBool("IsLattacking",true);
           


            }


     






    }
    bool AnimatorIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).length >
               anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
    void Update()
    {



        if (isGrounded == true)
        {
            extraJumps = 2;
        }

        if (Input.GetKeyDown(KeyCode.W) && extraJumps > 0)
        {
            rb2d.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded == true)
        {
            rb2d.velocity = Vector2.up * jumpForce;
        }
    }

}