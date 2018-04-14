using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhobiaCatcher : MonoBehaviour {

	[SerializeField] private List<string> PhobiasList;
	private ProjectileThrower projectileThrower;

	void Start () {
		projectileThrower = GetComponentInChildren<ProjectileThrower> ();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.CompareTag ("Phobia")) {
			foreach (string s in PhobiasList) {
				if (s == col.GetComponent<PhobiaAI> ().phobiaType) {
					Destroy (this.gameObject);
				}
			}
		}
	}

	void OnTriggerStay2D (Collider2D col) {
		if (col.CompareTag ("Phobia")) {
			if (this.gameObject && Input.GetButtonDown ("Fire" + projectileThrower.playerNumber)) {
				GameObject catchedProjectile = col.gameObject;
				projectileThrower.setProjectile (catchedProjectile);
			}
		}
	}

}
