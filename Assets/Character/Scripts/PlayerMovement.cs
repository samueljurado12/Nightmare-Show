using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]private float speed, jumpForce, gravityForce;
	[Range(1,2)]
	[SerializeField]private int playerNumber;

	private bool onGround;

	// Use this for initialization
	void Start () {
		onGround = false;
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalDir = Input.GetAxis ("Horizontal" + playerNumber);
		if (!onGround) {
			Gravity ();
		} else {
			Jump (horizontalDir);
			Move (horizontalDir);
		}
	}

	void Move (float direction){
		transform.Translate(direction * speed * Time.deltaTime * Vector3.right);
	}

	void Jump (float direction) {
		if (Input.GetButtonDown ("Jump" + playerNumber)) {
			onGround = false;
			transform.Translate (Vector2.up * jumpForce * Time.deltaTime);
		}
	}

	void Gravity () {
		if (Input.GetButton ("Jump" + playerNumber)) {
			transform.Translate (Vector2.up * jumpForce * Time.deltaTime);
		}
		transform.Translate (Vector2.down * gravityForce * Time.deltaTime);
	}

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.CompareTag ("Floor")) {
			onGround = true;
		}
	}
}
