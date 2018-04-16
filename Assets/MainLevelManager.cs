using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelManager : MonoBehaviour {

	public AudioSource backgroundMusic;

	public void GoToPlay () {
		Debug.Log ("pasa");
		MenuMusic.mustBeDestroyed = true;
		GameSceneManager.LoadNextRandomLevel ();
	}

	public void GoToCredits () {
		GameSceneManager.LoadCredits ();
	}

	public void GoToControls () {
		GameSceneManager.LoadControls ();
	}

}
