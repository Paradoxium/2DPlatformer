using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	GameObject player; 		  //the players game object
	Vector3 offset;    		  //offset for camera follow

	// happens first
	void Awake(){
		//finds the gameobject with the player tag in the scene and assigns it.
		player = GameObject.FindGameObjectWithTag ("Player");
		offset = transform.position - player.transform.position;
	}
	// happens every frame
	void Update () {
		//remember transform (lowercase) refers to the transform of the game object that this script is attached to
		transform.position = player.transform.position + offset;
	}
}