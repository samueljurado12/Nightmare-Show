using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhobiaAI : MonoBehaviour {

	[SerializeField] private float speed, minTime, maxTime, lifeTime;
	private bool isIdle, isOnGround;
    private Rigidbody2D myRigidbody;
	private Vector3 targetPos;

    private int direction;
    private float timeSinceLastDecision = 0;
    private float decisionDurationTime = 0;

    public string phobiaType;

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (decisionDurationTime <= timeSinceLastDecision) {
            timeSinceLastDecision = 0;
            decisionDurationTime = Random.Range(minTime, maxTime);
            direction = GetDirection();
        }

        Move();
        timeSinceLastDecision += Time.deltaTime;
    }


    private static int GetDirection() {
        int direction;
        float prob = Random.value;
        if (prob < 0.3) {
            direction = -1;
        } else if (prob < 0.6) {
            direction = 1;
        } else {
            direction = 0;
        }

        return direction;
    }

    private void Move() {
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }
}
