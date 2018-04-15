using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag ("Phobia")) {
			Destroy (col.gameObject);
		}
	}
}
