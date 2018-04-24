using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrower : MonoBehaviour {

	public GameObject projectile;
	[SerializeField] public GameObject projectileHolder;
	[SerializeField] private GameObject scope;
	[SerializeField] private float angleSpeed;
	[SerializeField] private float force;
	[Range (1, 2)]
	public int playerNumber;
	private PlayerMovement playerMovement;
	private Rigidbody2D projectileRigidBody;
	private bool isGoingUp = true;
	public bool hasReleaseProjectileCatcherButton = false;

	void Start () {
		playerMovement = transform.parent.gameObject.GetComponent<PlayerMovement> ();
		scope.SetActive (false);
	}

	void Update () { 
		if (projectile) {
			if (hasReleaseProjectileCatcherButton) {
				if (Input.GetButton ("Fire" + playerNumber)) {
					AimShot ();
				} else if (Input.GetButtonUp ("Fire" + playerNumber)) {
					hasReleaseProjectileCatcherButton = false;
					playerMovement.SetCurrentState (PlayerMovement.PlayerState.THROW);
				}
			} else if (Input.GetButtonUp ("Fire" + playerNumber)) {
				hasReleaseProjectileCatcherButton = true;
			}
		}
	}

	void AimShot () {
		scope.SetActive (true);
		if (isGoingUp) {
			if (scope.transform.localRotation.eulerAngles.z < 90 || scope.transform.localRotation.eulerAngles.z > 315) {
				scope.transform.localRotation = Quaternion.Euler (0, 0, scope.transform.localRotation.eulerAngles.z + angleSpeed);
			} else {
				isGoingUp = false;
			}
		} else {
			if (scope.transform.localRotation.eulerAngles.z < 93 || scope.transform.localRotation.eulerAngles.z > 320) {
				scope.transform.localRotation = Quaternion.Euler (0, 0, scope.transform.localRotation.eulerAngles.z - angleSpeed);
			} else {
				isGoingUp = true;
			}
		}
	}

	public void setProjectile (GameObject proj) {
		if (!projectile) {
			projectile = proj;
			projectileRigidBody = projectile.GetComponent<Rigidbody2D> ();
			projectileRigidBody.velocity = Vector2.zero;
			PhobiaAI phobiaAI = projectile.GetComponent<PhobiaAI> ();
			phobiaAI.PlayWalkSound ();
			phobiaAI.GrabPhobia();
			playerMovement.isHolding = true;
		}
	}

	public void ThrowProjectile () {
		if (projectile) {
			int direction = playerMovement.IsWalkingLeft () ? -1 : 1;
			float ang = Mathf.Deg2Rad * scope.transform.rotation.eulerAngles.z;
			Vector2 vForce = new Vector2 (direction * force * Mathf.Cos (ang), direction * force * Mathf.Sin (ang));
			scope.SetActive (false);
			PhobiaAI phobiaAI = projectile.GetComponent<PhobiaAI> ();
			phobiaAI.PlayThrowSound ();
			phobiaAI.ThrowPhobia(vForce);
			hasReleaseProjectileCatcherButton = false;
			playerMovement.isHolding = false;
            projectile = null;
		}
	}

	public void DropProjectile () {
		if (projectile) {
            hasReleaseProjectileCatcherButton = false;
            scope.SetActive(false);
            PhobiaAI phobiaAI = projectile.GetComponent<PhobiaAI>();
			phobiaAI.DropPhobia();
            projectile = null;
		}
	}

	public void GrabPhobia () {
		projectile.transform.SetParent (projectileHolder.transform);
		projectile.transform.localPosition = Vector3.zero;
		projectile.transform.localScale = Vector3.one;
	}
}
