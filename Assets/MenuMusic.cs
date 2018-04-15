using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour {
	private static MenuMusic menuInstance;
	public static bool mustBeDestroyed = false;

	void Start () {
/*		Debug.Log ("vivo");
		if (mustBeDestroyed) {
			Debug.Log ("adnjhbdnasjk");
			DestroyObject (gameObject);
		}
		DontDestroyOnLoad (this);

		if (menuInstance == null) {
			menuInstance = this;
		} else {
			DestroyObject (gameObject);
		}
*/			
		GameObject compareObject = FindObjectOfType<MenuMusic> ().gameObject;
		if (!compareObject.Equals (gameObject)) {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
		}

	}

	public void DestroyMenuMusic () {
		Destroy (gameObject);
	}
}
