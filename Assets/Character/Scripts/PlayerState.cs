using UnityEngine;

public partial class PlayerMovement : MonoBehaviour {
    public enum PlayerState {
		STAND,
		WALK,
		JUMP,
		GRAB,
		THROW,
        CROUCH,
		DIE,
		DEAD};
}
