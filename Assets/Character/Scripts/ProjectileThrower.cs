using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrower : MonoBehaviour {

	public GameObject projectile;
	[SerializeField] private GameObject projectileHolder;
	[SerializeField] private GameObject scope;
	[SerializeField] private float angleSpeed;
	[SerializeField] private float force;
	[Range(1,2)]
	[SerializeField] private int playerNumber;
	private Rigidbody2D projectileRigidBody;
	private bool isGoingUp = true;

	void Start () {
		projectileRigidBody = projectile.GetComponent<Rigidbody2D> ();
		projectileRigidBody.isKinematic = true;
		scope.SetActive (false);
	}

	void Update () { //TODO Assign self player fire button
		if (projectile) {
			if (Input.GetButton("Fire" + playerNumber)) {
				AimShot ();
			} else if (Input.GetButtonUp("Fire" + playerNumber)) {
				scope.SetActive (false);
				ThrowProjectile ();
			}
		}

	}

	void AimShot () {
		scope.SetActive (true);
		if (isGoingUp) {
			if (scope.transform.rotation.eulerAngles.z < 90 || scope.transform.rotation.eulerAngles.z > 358) {
				scope.transform.rotation = Quaternion.Euler (0, 0, scope.transform.rotation.eulerAngles.z + angleSpeed);
			} else {
				isGoingUp = false;
			}
		} else {
			if (scope.transform.rotation.eulerAngles.z < 91) {
				scope.transform.rotation = Quaternion.Euler (0, 0, scope.transform.rotation.eulerAngles.z - angleSpeed);
			} else {

				isGoingUp = true;
			}
		}
	}

	void ThrowProjectile () {
		float ang = Mathf.Deg2Rad * scope.transform.rotation.eulerAngles.z;
		projectileRigidBody.isKinematic = false;
		projectileRigidBody.AddForce (new Vector2 (force * Mathf.Cos (ang), force * Mathf.Sin (ang)));
		projectile.transform.SetParent (null);
		projectile = null;
	}
}
