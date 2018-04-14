using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	[SerializeField] private GameObject phobia;
	[SerializeField] private float width;
	[SerializeField] private float spawnRate;
	[SerializeField] private float variance;
	[SerializeField] private float spawnIncrease;
	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves () {
		while (true) {
			Spawn (phobia);
			yield return new WaitForSecondsRealtime (spawnRate + Random.value * variance);
		}
	}

	void Spawn (GameObject phobia) {
		Vector2 spawnPosition = new Vector2 (Random.Range (-width / 2, width / 2), this.transform.position.y);
		Instantiate (phobia, spawnPosition, Quaternion.identity);
        spawnRate = spawnRate * (1 - spawnIncrease);
    }
}
