using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 6;

	private Rigidbody2D myRigidbody;
	private Camera viewCamera;
	private Vector2 velocity;


	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		viewCamera = Camera.main;
	}
	
	void Update () {
		Vector3 pointToLook = viewCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = new Vector2(
			pointToLook.x - transform.position.x,
			pointToLook.y - transform.position.y
		);
		transform.up = direction; 
		velocity = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
	}

	void FixedUpdate() {
		myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
	}
	
}
