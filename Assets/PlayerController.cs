﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	//Variables
	Rigidbody2D rb;            //refrences the rigidbody2d on the Game Object, used to move the player.
	bool facingRight;		   //used to changes the players facing direction
	bool isGrounded;		   //toggled is the player is on the ground
	Animator anim;			   //references the animator component on the GameObject, used to call animations
	RaycastHit2D hitGround;    //used to dectect if the player is grounded
	float groundBuffer = 0.1f; //how far off the ground before considered grounded.

	public float moveSpeed; 
	public float jumpSpeed;
	
	//for horizontal & Vertical input
	float h;
	float v;

	//setup
	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		//uses the input manager in the unity editor to get the direction and assigns it to h
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis ("Vertical");
		move(h,v);
		flip(h);
	}

	// Moves the player. The horizontal movement direciton taken from the input manager in unity.
	private void move(float h, float v) {
		if (Input.GetKeyDown("space") && isGrounded) {
			jump ();
			anim.SetBool ("jumping", true);
		} else {
			// takes the value h and applies it to the horizontal axis
			rb.velocity = new Vector2(h * moveSpeed * Time.deltaTime, rb.velocity.y);
		}
		//changes animation parameter for running, used to transition between moving animation and idle animation
		if (h == 0) {
			anim.SetBool ("running", false);
		} else {
			anim.SetBool ("running", true);
		}
		//if gorund check is true, then the player is on the ground and jumping anim parameter is off, if false they are jumping, jump anim parameter is on
		if (groundCheck () && !isGrounded) {
			isGrounded = true;
			anim.SetBool ("jumping", false);
		} else {
			isGrounded = false;
		}
	}
		
	//Checks to make sure the player is facing the right direction while moving. The horizontal direction will be positive if moving right, negitive if moving left.
	private void flip(float h)
	{
		//if the horizontal is negitive AND not facing right OR if the horizontal is positive and facing right. 
		if(h < 0 && !facingRight || h > 0 && facingRight)
		{
			//flips the value of boolean
			facingRight = !facingRight;

			//grab the scale value from the players transform
			Vector3 tScale = transform.localScale;

			//change from positive to negitive or negitive to positive
			tScale.x = tScale.x * -1;
			 
			//apply to scale to flip
			transform.localScale = tScale;
		}
	}

	//applies upward force to make the player jump
	private void jump(){
		rb.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
		anim.SetBool ("jumping", true);
	}

	//checks to see if the player is within the groundBuffer range, if so return true, if false return false
	private bool groundCheck(){
		return Physics2D.Raycast (transform.position, -Vector2.up, groundBuffer + 3f);
	}
}