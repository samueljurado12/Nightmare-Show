using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager {

	public static int MAIN_MENU = 0;
	public static int END_SCENE = 1;
	public static int last_scene = 0;

	public static void LoadMainMenu () {
		SceneManager.LoadScene (MAIN_MENU);
	}

	public static void LoadEndScene () {
		SceneManager.LoadScene (END_SCENE);
	}

	public static void LoadNextRandomLevel () {
		int scene;
		do {
			scene = GetNextLevelNumber ();
		} while (scene == last_scene);
		SceneManager.LoadScene (scene);
	}

	private static int GetNextLevelNumber () {
		return (int)Mathf.Round (Random.Range (2, SceneManager.sceneCountInBuildSettings));
	}
}
