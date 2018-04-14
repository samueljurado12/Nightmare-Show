using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	enum PlayerState {STAND, WALK, JUMP};
	[Range (1, 2)]
	[SerializeField]private int playerNumber = 1;
	[SerializeField]private float walkSpeed, jumpSpeed, minJumpForce, maxFallSpeed, gravityForce;

	private Vector2 velocity;
	private Vector3 scale;
	private bool onGround, pushingWallLeft, pushingWallRight, againstCeiling;
	private PlayerState currentState;

	// Use this for initialization
	void Start () {
		velocity = Vector2.zero;
		currentState = PlayerState.STAND;
		onGround = false;
		pushingWallLeft = false;
		pushingWallRight = false;
		againstCeiling = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizontalDir = Input.GetAxis ("Horizontal" + playerNumber);
		switch (currentState) {
		case PlayerState.STAND:
			velocity = Vector2.zero;
				//TODO Insert animations
			if (!onGround) {
				currentState = PlayerState.JUMP;
				break;
			}
			if (horizontalDir != 0) {
				currentState = PlayerState.WALK;
				break;
			} else if (Input.GetButton ("Jump" + playerNumber)) {
				velocity.y = jumpSpeed;
				currentState = PlayerState.JUMP;
				break;
			}
			break;

		case PlayerState.WALK:
			//TODO Insert animations
			if (horizontalDir == 0) {
				currentState = PlayerState.STAND;
				velocity = Vector2.zero;
				break;
			} else {
				velocity.x = (pushingWallLeft && horizontalDir < 0f||
				pushingWallRight && horizontalDir > 0f) ? 
					0f : horizontalDir * walkSpeed;
				//TODO Add scale to reverse sprite
			}

			if (Input.GetButton ("Jump" + playerNumber)) {
				velocity.y = jumpSpeed;
				//TODO Add audio(?)
				currentState = PlayerState.JUMP;
				break;
			} else if (!onGround) {
				currentState = PlayerState.JUMP;
				break;
			}
			break;
		case PlayerState.JUMP:
			//TODO Insert animations
			velocity.y -= gravityForce * Time.deltaTime;
			velocity.y = Mathf.Max (velocity.y, -maxFallSpeed);
			if (horizontalDir == 0) {
				velocity.x = 0;
			} else {
				velocity.x = (pushingWallLeft && horizontalDir < 0f ||
				pushingWallRight && horizontalDir > 0f) ? 
					0f : horizontalDir * walkSpeed;
				//TODO Add scaling to reverse sprite
			}
			if (!Input.GetButton ("Jump" + playerNumber) && velocity.y > 0.0f)
				velocity.y = Mathf.Min (velocity.y, minJumpForce);
			if (onGround) {
				if (horizontalDir == 0) {
					currentState = PlayerState.STAND;
					velocity = Vector2.zero;
				} else {
					currentState = PlayerState.WALK;
					velocity.y = 0;
				}
				//TODO Insert audio(?)
				break;
			}
			break;
		}
		Debug.Log (velocity);
		transform.Translate (velocity * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Floor")) {
			onGround = true;
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.CompareTag ("Floor")) {
			onGround = false;
		}
	}
}
