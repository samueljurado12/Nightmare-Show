using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

	[SerializeField]private AudioClip[] deathClips;

	private AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}
	
	public void playRandomSound(){
		audio.PlayOneShot (deathClips[Random.Range (0, deathClips.Length)]);
	}
}
