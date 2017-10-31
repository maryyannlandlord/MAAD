using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void PlayAudioSource(GameObject go, AudioClip clip = null)
    {
        AudioSource audioSource = go.GetComponent<AudioSource>();
        AudioClip clipToPlay = (clip == null) ? audioSource.clip : clip;
        audioSource.PlayOneShot(clipToPlay);
    }

}
