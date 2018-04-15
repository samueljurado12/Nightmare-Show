using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMain : MonoBehaviour {

	void Start () {
		InvokeRepeating ("GoToMain", 5, 1 * Time.deltaTime);
	}

	void GoToMain () {
		if (Input.GetButtonDown ("Submit")) {
			GameSceneManager.LoadMainMenu ();
		}
	}
}
