using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	//Variables
	Rigidbody2D rb;
	bool facingRight;


	public float speed;
	
	//for horizontal input
	float h;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//uses the input manager in the unity editor to get the direction and assigns it to h
		h = Input.GetAxis("Horizontal");

		//be sure to pass h into move flip function
		move(h);
		flip(h);


	}

	void move(float h) {
		// takes the value h and applies it to the horizontal axis
		rb.velocity = new Vector2(h * speed * Time.deltaTime, rb.velocity.y);

		//flips player when changing direction
	}

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


}
