using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerMovement : MonoBehaviour {

    [Range(1, 2)]
    public int playerNumber = 1;
    public bool isHolding;

    [SerializeField] private float walkSpeed, jumpSpeed, minJumpForce, maxFallSpeed, gravityForce;
    private bool onGround, pushingWallLeft, pushingWallRight, isWalkingLeft, touchingCeiling;
    private PlayerAudio audioMngr;
    private Vector2 velocity;
    private Vector3 playerScale;
    private PlayerState currentState;
    private Animator anim;
    private ProjectileThrower projectileThrower;

    // Use this for initialization
    void Start() {
        velocity = Vector2.zero;
        currentState = PlayerState.STAND;
        onGround = false;
        pushingWallLeft = false;
        pushingWallRight = false;
        isWalkingLeft = false;
        touchingCeiling = false;
        isHolding = false;
        anim = gameObject.GetComponent<Animator>();
        audioMngr = GetComponent<PlayerAudio>();
        projectileThrower = GetComponentInChildren<ProjectileThrower>();

    }

    // Update is called once per frame
    void FixedUpdate() {
        float horizontalDir = Input.GetAxis("Horizontal" + playerNumber);
        MovementStateMachine(horizontalDir);
        setFacing(horizontalDir);
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    void MovementStateMachine(float horizontalDir) {
        string animState; //Current state for the animation

        switch (currentState) {
            case PlayerState.STAND:
                velocity = Vector2.zero;
                animState = isHolding ? "Idle_Holding" : "Idle";
                anim.Play(animState);
                if (!onGround) {
                    currentState = PlayerState.JUMP;
                    break;
                }
                if (horizontalDir != 0) {
                    currentState = PlayerState.WALK;
                    break;
                } else if (Input.GetButton("Jump" + playerNumber)) {
                    velocity.y = jumpSpeed;
                    currentState = PlayerState.JUMP;
                    break;
                } else if (Input.GetButton("Crouch" + playerNumber)) {
                    currentState = PlayerState.CROUCH;
                }
                break;

            case PlayerState.WALK:
                animState = isHolding ? "Walk_Holding" : "Walk";
                anim.Play(animState);
                if (horizontalDir == 0) {
                    currentState = PlayerState.STAND;
                    velocity = Vector2.zero;
                    break;
                } else {
                    velocity.x = SetVelocityX(horizontalDir);
                }

                if (Input.GetButton("Jump" + playerNumber)) {
                    velocity.y = jumpSpeed;
                    //TODO Add audio(?)
                    currentState = PlayerState.JUMP;
                    break;
                } else if (!onGround) {
                    currentState = PlayerState.JUMP;
                    break;
                } else if (Input.GetButton("Crouch" + playerNumber)) {
                    velocity.x = 0;
                    currentState = PlayerState.CROUCH;
                }
                break;

            case PlayerState.JUMP:
                animState = isHolding ? "Jump_Holding" : "Jump";
                anim.Play(animState);
                Fall();
                if (horizontalDir == 0) {
                    velocity.x = 0;
                } else {
                    velocity.x = SetVelocityX(horizontalDir);
                }
                if (!Input.GetButton("Jump" + playerNumber) && velocity.y > 0.0f)
                    velocity.y = Mathf.Min(velocity.y, minJumpForce);
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
                anim.Play("Die");
                break;

            case PlayerState.DEAD:
                anim.Play("Dead");
                if (!IsInvoking("nextScene")) {
                    audioMngr.playRandomSound();
                    Invoke("nextScene", 1.75f);
                }
                break;

            case PlayerState.GRAB:
                anim.Play("Grab");
                if (!onGround) {
                    Fall();
                }
                break;

            case PlayerState.THROW:
                anim.Play("Throw");
                if (!onGround) {
                    Fall();
                }
                break;

            case PlayerState.CROUCH:
                velocity.x = 0;
                anim.Play("Crouch");
                projectileThrower.DropProjectile();
                isHolding = false;
                if (!Input.GetButton("Crouch" + playerNumber)) {
                    currentState = PlayerState.STAND;
                }
                break;
        }
    }

    private void Fall() {
        velocity.y -= gravityForce * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, -maxFallSpeed);
    }

    void setFacing(float horizontalDir) {
        Vector3 vScale = Vector3.one;
        vScale.x = isWalkingLeft ? -1 : 1;
        transform.localScale = vScale;
    }

    float SetVelocityX(float horizontalDir) {
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

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.CompareTag("Floor")) {
            onGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag("Floor")) {
            onGround = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Floor")) {
            velocity.y = 0;
        }
    }

    void OnCollisionExit2D(Collision2D col) {
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

    public bool IsWalkingLeft() {
        return isWalkingLeft;
    }

    public void SetIsHolding(bool value) {
        isHolding = value;
    }

    public void SetCurrentState(PlayerState state) {
        currentState = state;
    }

    void ChangeDieToDead() {
        currentState = PlayerState.DEAD;
    }


    void Throw() {
        GetComponentInChildren<ProjectileThrower>().ThrowProjectile();
    }

    void PickNextStateThrowGrab() {
        if (!onGround) {
            currentState = PlayerState.JUMP;
        } else {
            currentState = PlayerState.STAND;
        }
    }

    void LoadNextScene() {
        ScoreManager.NextScene();
    }
}
