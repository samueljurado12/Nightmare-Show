using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

    private bool hasAPlayerDied = false;

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Phobia")) {
            Destroy(col.gameObject);
        } else if (col.gameObject.CompareTag("Player")) {
            Debug.Log("A player has died due to fall");
            PlayerMovement playerMovement = col.GetComponent<PlayerMovement>();
            if (!hasAPlayerDied) {
                if (playerMovement.playerNumber == 1) {
                    ScoreManager.player2Score++;
                } else {
                    ScoreManager.player1Score++;
                }
                playerMovement.SetCurrentState(PlayerMovement.PlayerState.DIE);
            }
            hasAPlayerDied = true;
        }
	}
}
