using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAudio : MonoBehaviour {

    public AudioClip[] audioClips;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        AudioController.PlayAudioSource(gameObject, audioClips[Random.Range(0, audioClips.Length)]);
    }
}
