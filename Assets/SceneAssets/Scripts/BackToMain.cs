using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMain : MonoBehaviour {

	void Start () {
		InvokeRepeating ("GoToMain", 2, 1 * Time.deltaTime);
		ScoreManager.ResetScore ();
	}

	void GoToMain () {
		if (Input.GetButtonDown ("Submit")) {
			GameSceneManager.LoadMainMenu ();
		}
	}
}
