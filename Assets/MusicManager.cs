using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    [SerializeField] private AudioClip[] clips;
    private AudioSource myAudioSource;

	void Start () {
        myAudioSource = GetComponent<AudioSource>();
        int index = Random.Range(0, clips.Length);
		Debug.Log (index);
        myAudioSource.clip = clips[index];
        myAudioSource.Play();
    }
}
