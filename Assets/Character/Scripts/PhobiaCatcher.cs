using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhobiaCatcher : MonoBehaviour {

	[SerializeField] private List<string> PhobiasList;
	private ProjectileThrower projectileThrower;

	private bool hasAPlayerDie = false;
	private PlayerMovement playerMovement;
	public GameObject projectile;

	void Start () {
		projectileThrower = GetComponentInChildren<ProjectileThrower> ();
		playerMovement = GetComponent<PlayerMovement> ();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.CompareTag ("Phobia")) {
			GameObject projectile = col.transform.parent.gameObject;
			foreach (string phobiaType in PhobiasList) {
                PhobiaAI phobiaAI = projectile.GetComponent<PhobiaAI>();
                if (phobiaAI.CanKill(phobiaType)) {
					if (!hasAPlayerDie) {
						if (projectileThrower.playerNumber == 1) {
							ScoreManager.player2Score++;
						} else {
							ScoreManager.player1Score++;
						}
						projectileThrower.DropProjectile ();
						playerMovement.SetCurrentState (PlayerMovement.PlayerState.DIE);
					}
					hasAPlayerDie = true;
				}
			}
		}
	}

	void OnTriggerStay2D (Collider2D col) {
		if (col.CompareTag ("Phobia")) {
			projectile = col.transform.parent.gameObject;
			if (!hasAPlayerDie && Input.GetButtonDown ("Fire" + projectileThrower.playerNumber)) {
				projectileThrower.setProjectile (projectile);
				playerMovement.SetCurrentState (PlayerMovement.PlayerState.GRAB);
			}
		}
	}

	public void GrabPhobia() {
		projectileThrower.GrabPhobia();
	}
}
