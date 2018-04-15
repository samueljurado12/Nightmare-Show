using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelManager : MonoBehaviour {

	public void GoToPlay () {
		GameSceneManager.LoadNextRandomLevel ();
	}

	public void GoToCredits () {
		GameSceneManager.LoadCredits ();
	}

	public void GoToControls () {
		GameSceneManager.LoadControls ();
	}
}
