using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    private bool isGamePaused;

	// Use this for initialization
	void Start () {
        isGamePaused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause") || (Input.GetButtonDown("Cancel") && isGamePaused)) {
            UpdatePauseState();
        }
	}

    private void UpdatePauseState() {
        if (!isGamePaused) {
            Time.timeScale = 0;
            isGamePaused = true;
        } else {
            isGamePaused = false;
            Time.timeScale = 1;
        }
    }
}
