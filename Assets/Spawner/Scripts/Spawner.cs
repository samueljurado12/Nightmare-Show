using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] private GameObject phobia;
    [SerializeField] private float width;
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] private float variance = 0.5f;
    [SerializeField] private float spawnIncrease = 0.5f;
	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnWaves());

    }

    // Update is called once per frame
    void Update () {
	    
	}

    IEnumerator SpawnWaves() {
        while (true) {
            Spawn(phobia);
            yield return new WaitForSeconds(spawnRate + Random.value * variance);
            spawnRate = spawnRate * (1 - spawnIncrease);
        }
    }

    void Spawn(GameObject phobia) {
        Vector2 spawnPosition = new Vector2(Random.Range(-width/2, width/2) ,this.transform.position.y);
        Instantiate(phobia, spawnPosition, Quaternion.identity);  
    }
}
