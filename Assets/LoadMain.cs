using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMain : MonoBehaviour {
	public int timeToChangeLevel = 3;

	void Start () {
		Invoke ("GoToMain", timeToChangeLevel);
	}

	void GoToMain () {
		GameSceneManager.LoadMainMenu ();
	}
}
