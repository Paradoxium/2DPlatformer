using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	//Variables
	Rigidbody2D rb;
	bool facingRight;
	bool isGrounded;
	Animator anim;

	public float moveSpeed;
	public float jumpSpeed;
	
	//for horizontal input
	float h;

	//for jumping
	float v;

	//setup
	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//uses the input manager in the unity editor to get the direction and assigns it to h
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis ("Vertical");
		move(h,v);
		flip(h);
	}

	/// <summary>
	/// Moves the player.
	/// </summary>
	/// <param name="h">The horizontal movement direciton taken from the input manager in unity</param>
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
	}

	/// <summary>
	/// Checks to make sure the player is facing the right direction while moving.
	/// </summary>
	/// <param name="h">The horizontal direction will be positive if moving right, negitive if moving left.</param>
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
		
	private void jump(){
		rb.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);

	}
		
	void OnCollisionEnter2D(Collision2D other){
		isGrounded = true;
	}
	//going to need to use raycasting method here i think...


	void OnCollisionExit2D(Collision2D other){
		isGrounded = false;
		anim.SetBool ("jumping", false);
	}


}
