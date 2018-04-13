using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]private float speed, jumpForce;
	[Range(1,2)]
	[SerializeField]private int playerNumber;

	private bool onGround;

	// Use this for initialization
	void Start () {
		onGround = false;
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move (){
		transform.Translate(Input.GetAxis("Horizontal" + playerNumber) * speed * Time.deltaTime * Vector3.right);
	}
}
