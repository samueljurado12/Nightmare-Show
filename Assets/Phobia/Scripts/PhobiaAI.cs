using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhobiaAI : MonoBehaviour {

	public string phobiaType;
	public  bool canMove = true;

    [SerializeField] private float speed, minTime, maxTime, lifeTime;
    [SerializeField] private AudioClip[] walkingSounds;
    [SerializeField] private AudioClip[] throwSounds;
	private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private AudioSource myAudioSource;
	private Vector3 targetPos;
    private bool isIdle;
	private int direction;
	private float timeSinceLastDecision = 0;
	private float decisionDurationTime = 0;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		if (decisionDurationTime <= timeSinceLastDecision) {
			timeSinceLastDecision = 0;
			decisionDurationTime = Random.Range (minTime, maxTime);
			direction = GetDirection ();
		}
        UpdateAnimation();
        Debug.Log(isOnGround());
        if (canMove && isOnGround()) {
			Move ();
		}
		timeSinceLastDecision += Time.deltaTime;
	}

    private void UpdateAnimation() {
        if (!isOnGround()) {
            myAnimator.Play("Jump");
        } else if (direction == 0 || !canMove) {
            myAnimator.Play("Idle");
        } else if (direction == -1) {
            myAnimator.Play("Walk");
            transform.localScale = Vector3.one;
        } else if (direction == 1) {
            myAnimator.Play("Walk");
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private int GetDirection () {
		int direction;
		float prob = Random.value;
		if (prob < 0.3) {
            PlayWalkSound();
            direction = -1;
		} else if (prob < 0.6) {
            PlayWalkSound();
            direction = 1;
		} else {
			direction = 0;
		}

		return direction;
	}

	private void Move () {
		transform.Translate (Vector3.right * direction * speed * Time.deltaTime);
	}

    private bool isOnGround() {
       return myRigidbody.velocity.y == 0;
    }

    public void PlayThrowSound() {
        int index = Random.Range(0, throwSounds.Length);
        myAudioSource.clip = throwSounds[index];
        myAudioSource.Play();
    }

    public void PlayWalkSound() {
        int index = Random.Range(0, walkingSounds.Length);
        myAudioSource.clip = walkingSounds[index];
        myAudioSource.Play();
    }
}
