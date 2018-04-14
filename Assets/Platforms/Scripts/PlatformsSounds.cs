using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsSounds : MonoBehaviour {

	[SerializeField]private AudioClip[] sounds;
	[SerializeField]private float minStartTime;
	[SerializeField]private float maxStartTime;
	[SerializeField]private float minRepeatTime;
	[SerializeField]private float maxRepeatTime;

	private AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}

	void OnTriggerStay2D(Collider2D col){
		InvokeRepeating ("PlayRandomSound", Random.Range(minStartTime, maxStartTime), 
											Random.Range(minRepeatTime, maxRepeatTime));
	}

	void OnTriggerExit2D(Collider2D col){
		CancelInvoke ();
	}

	void PlayRandomSound(){
		if (!audio.isPlaying) {
			audio.PlayOneShot (sounds[Random.Range(0, sounds.Length)]);
		}
	}
}
