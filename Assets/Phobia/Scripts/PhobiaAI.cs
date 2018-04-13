using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PhobiaAI : MonoBehaviour {

    [SerializeField] private float speed, minDistance, maxDistance, lifeTime;

    private bool isIdle, isOnGround;
    private Vector3 targetPos;

    // Use this for initialization
    void Start() {
        isIdle = false;
        isOnGround = true; // TODO Implement function
        targetPos = transform.position;
        // Invoke("SelfDestroy", lifeTime);
    }

    // Update is called once per frame
    void Update() {
        if (isOnGround) {
            if (isIdle) {
                GetNewTarget();
            }
            Move();
        }
        isIdle = CheckArrival();
        isOnGround = CheckGrounded();
    }

    // Assign new value to targetPos field
    private void GetNewTarget() {
        int direction = GetDirection();
        targetPos = transform.position + Vector3.right * direction * Random.Range(minDistance, maxDistance);
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
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private bool CheckArrival() {
        return transform.position == targetPos;
    }

    private bool CheckGrounded() {
        // TODO Implement method
        return true;
    }

    private void SelfDestroy() {
        Destroy(gameObject);
    }
}
