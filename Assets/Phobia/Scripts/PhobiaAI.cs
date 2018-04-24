using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhobiaAI : MonoBehaviour {
	public string phobiaType;
    [SerializeField] private float speed, minTime, maxTime;
    [SerializeField] private AudioClip[] walkingSounds;
    [SerializeField] private AudioClip[] throwSounds;
    private bool isGrabbed;
	private int direction;
	private float timeSinceLastDecision = 0, decisionDurationTime= 0;
    private AudioSource myAudioSource;
	private Rigidbody2D myRigidbody;
    private Animator myAnimator;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
        StartCoroutine(DecideNewMovement());
	}

	// Update is called once per frame
	void Update () {
        if (CanMove()) {
			Move ();
		}        
        UpdateAnimation();
	}

    IEnumerator DecideNewMovement() {
        while(true) {
            decisionDurationTime = Random.Range (minTime, maxTime);
			direction = GetDirection ();
            yield return new WaitForSecondsRealtime(decisionDurationTime);
        }
    }

    private void UpdateAnimation() {
        if (!IsOnGround()) {
            myAnimator.Play("Jump");
        } else if (direction == 0 || !CanMove()) {
            myAnimator.Play("Idle");
        } else if (direction == -1) {
            myAnimator.Play("Walk");
            //Rotate left
            transform.localScale = Vector3.one;
        } else if (direction == 1) {
            myAnimator.Play("Walk");
            //Rotate right
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

    public bool CanMove() {
        return !isGrabbed && IsOnGround();
    }

    private bool IsOnGround() {
       return myRigidbody.velocity.y == 0;
    }

    public bool CanKill(string phobia) {
        return !isGrabbed && phobia == phobiaType;
    }
    public void DropPhobia() {
        ThrowPhobia(Vector2.zero);
    }

    public void GrabPhobia() {
        isGrabbed = true;
        myRigidbody.isKinematic = true;
    }

    public void ThrowPhobia(Vector2 force) {
        isGrabbed = false;
        myRigidbody.isKinematic = false;
        myRigidbody.AddForce(force);
        transform.parent = null;
    }
}
