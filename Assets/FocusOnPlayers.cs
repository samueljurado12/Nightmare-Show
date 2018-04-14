using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusOnPlayers : MonoBehaviour {

	private Camera myCamera;
	public GameObject player1;
	public GameObject player2;
	public float minSize = 8;


	// Use this for initialization
	void Start () {
		myCamera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		float sizeY = Mathf.Clamp (Vector3.Distance (player1.transform.position, player2.transform.position) / 2, minSize, 14);
		myCamera.orthographicSize = sizeY;
		float sizeX = (16 * sizeY) / 9;

		transform.position = (player1.transform.position + player2.transform.position) / 2;
		float yPos = Mathf.Clamp (transform.position.y, sizeY - 14, 14 - sizeY);
		float xPos = Mathf.Clamp (transform.position.x, sizeX - 25, 25 - sizeX);
		transform.position = new Vector3 (xPos, yPos, -10);
	}
}
