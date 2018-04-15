using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager {
    public static int SPLASH_SCREEN = 0;
	public static int MAIN_MENU = 1;
    public static int PLAYER_1_WIN = 2;
    public static int PLAYER_2_WIN = 3;
    public static int FIRST_LEVEL_SCENE = 4;

    public static int last_scene = 0;

	public static void LoadMainMenu () {
		SceneManager.LoadScene (MAIN_MENU);
	}

    public static void LoadPlayer1Win() {
        SceneManager.LoadScene(PLAYER_1_WIN);
    }

    public static void LoadPlayer2Win() {
        SceneManager.LoadScene(PLAYER_2_WIN);
    }

    public static void LoadNextRandomLevel () {
		int scene;
		do {
			scene = GetNextLevelNumber ();
		} while (scene == last_scene);
        last_scene = scene;
		SceneManager.LoadScene (scene);
	}

	private static int GetNextLevelNumber () {
		return (int)Mathf.Round (Random.Range (FIRST_LEVEL_SCENE, SceneManager.sceneCountInBuildSettings));
	}
}
