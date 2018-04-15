using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public enum PlayerState {
		STAND,
		WALK,
		JUMP,
		GRAB,
		THROW,
		DIE,
		DEAD};

	[Range (1, 2)]
	[SerializeField]private int playerNumber = 1;
	[SerializeField]private float walkSpeed, jumpSpeed, minJumpForce, maxFallSpeed, gravityForce;

	private PlayerAudio audioMngr;
	private Vector2 velocity;
	private Vector3 playerScale;
	private bool onGround, pushingWallLeft, pushingWallRight, againstCeiling, isWalkingLeft, touchingCeiling;
	public bool isHolding;
	private PlayerState currentState;
	private Animator anim;

	// Use this for initializationF
	void Start () {
		velocity = Vector2.zero;
		currentState = PlayerState.STAND;
		onGround = false;
		pushingWallLeft = false;
		pushingWallRight = false;
		againstCeiling = false;
		isWalkingLeft = false;
		touchingCeiling = false;
		isHolding = false;
		anim = gameObject.GetComponent<Animator> ();
		audioMngr = GetComponent<PlayerAudio> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizontalDir = Input.GetAxis ("Horizontal" + playerNumber);
		VelocityUpdate (horizontalDir);
		setFacing (horizontalDir);
		if (playerNumber == 1) {
			Debug.Log (onGround);
		}
		GetComponent<Rigidbody2D> ().velocity = velocity;
	}

	void VelocityUpdate (float horizontalDir) {
		string animState;
		switch (currentState) {
		case PlayerState.STAND:
			velocity = Vector2.zero;
			animState = isHolding ? "Idle_Holding" : "Idle";
			anim.Play (animState);
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
			animState = isHolding ? "Walk_Holding" : "Walk";
			anim.Play (animState);
			if (horizontalDir == 0) {
				currentState = PlayerState.STAND;
				velocity = Vector2.zero;
				break;
			} else {
				velocity.x = SetVelocityX (horizontalDir);
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
			animState = isHolding ? "Jump_Holding" : "Jump";
			anim.Play (animState);
			velocity.y -= gravityForce * Time.deltaTime;
			velocity.y = Mathf.Max (velocity.y, -maxFallSpeed);
			if (horizontalDir == 0) {
				velocity.x = 0;
			} else {
				velocity.x = SetVelocityX (horizontalDir);
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
				break;
			}
			break;
		case PlayerState.DIE:
			velocity = Vector2.zero;
			anim.Play ("Die");
			break;
		case PlayerState.DEAD:
			anim.Play ("Dead");
			if (!IsInvoking ("nextScene")) {
				audioMngr.playRandomSound ();
				Invoke ("nextScene", 1.75f);
			}
			break;
		case PlayerState.GRAB:
			anim.Play ("Grab");
			if (!onGround) {
				velocity.y -= gravityForce * Time.deltaTime;
				velocity.y = Mathf.Max (velocity.y, -maxFallSpeed);
			}
			break;
		case PlayerState.THROW:
			anim.Play("Throw");
			if (!onGround) {
				velocity.y -= gravityForce * Time.deltaTime;
				velocity.y = Mathf.Max (velocity.y, -maxFallSpeed);
			}
			break;
		}
	}

	void setFacing (float horizontalDir) {
		Vector3 vScale = Vector3.one;
		vScale.x = isWalkingLeft ? -1 : 1;
		transform.localScale = vScale;
	}

	float SetVelocityX (float horizontalDir) {
		float targetSpeed = horizontalDir * walkSpeed;
		if (horizontalDir < 0f) {
			isWalkingLeft = true;
			return pushingWallLeft ? 0f : targetSpeed;
		} else if (horizontalDir > 0) {
			isWalkingLeft = false;
			return pushingWallRight ? 0f : targetSpeed;
		} else {
			return 0f;
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.CompareTag ("Floor")) {
			onGround = true;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.CompareTag ("Floor")) {
			onGround = false;
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.CompareTag ("Floor")) {
			velocity.y = 0;
		}
	}

	void OnCollisionExit2D (Collision2D col) {
		if (pushingWallRight) {
			pushingWallRight = false;
		}
		if (pushingWallLeft) {
			pushingWallLeft = false;
		}
		if (touchingCeiling) {
			touchingCeiling = false;
		}
	}

	public bool IsWalkingLeft () {
		return isWalkingLeft;
	}

	public void SetIsHolding (bool value) {
		isHolding = value;
	}

	public void SetCurrentState (PlayerState state) {
		currentState = state;
	}

	void ChangeDieToDead () {
		currentState = PlayerState.DEAD;
	}

	void nextScene () {
        if (ScoreManager.player1Score > 5) {
            GameSceneManager.LoadPlayer1Win();
        } else if (ScoreManager.player2Score > 5) {
            GameSceneManager.LoadPlayer2Win();
        } else { 
			GameSceneManager.LoadNextRandomLevel ();
		}
	}

	void Throw(){
		GetComponentInChildren<ProjectileThrower> ().ThrowProjectile ();
	}

	void PickNextStateThrowGrab(){
		if (!onGround) {
			currentState = PlayerState.JUMP;
		} else {
			currentState = PlayerState.STAND;
		}
	}
}
