﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhobiaCatcher : MonoBehaviour {

	[SerializeField] private List<string> PhobiasList;
	private ProjectileThrower projectileThrower;

	private bool hasAPlayerDie = false;
	private PlayerMovement player;

	void Start () {
		projectileThrower = GetComponentInChildren<ProjectileThrower> ();
		player = GetComponent<PlayerMovement> ();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.CompareTag ("Phobia")) {
			GameObject projectile = col.transform.parent.gameObject;
			foreach (string s in PhobiasList) {
				if (s == projectile.GetComponent<PhobiaAI> ().phobiaType) {
					if (!hasAPlayerDie) {
						if (projectileThrower.playerNumber == 1) {
							ScoreManager.player2Score++;
						} else {
							ScoreManager.player1Score++;
						}
						player.SetCurrentState (PlayerMovement.PlayerState.DIE);
					}
					hasAPlayerDie = true;
				}
			}
		}
	}

	void OnTriggerStay2D (Collider2D col) {
		if (col.CompareTag ("Phobia")) {
			GameObject projectile = col.transform.parent.gameObject;
			if (this.gameObject && Input.GetButtonDown ("Fire" + projectileThrower.playerNumber)) {
				player.SetCurrentState (PlayerMovement.PlayerState.GRAB);
				GameObject catchedProjectile = projectile;
				projectileThrower.setProjectile (catchedProjectile);
			}
		}
	}

}
