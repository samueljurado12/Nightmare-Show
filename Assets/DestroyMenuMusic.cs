using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMenuMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (GameObject.Find ("MenuMusic"));
	}
}
