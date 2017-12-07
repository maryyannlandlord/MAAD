using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Ambience : MonoBehaviour {

    public AudioSource AmbientAudio;
    public AudioClip B_Light;
    public AudioClip A_Light;
    public AdamBehavior adam; 

	// Use this for initialization
	void Start () {

        AmbientAudio.loop = true; 
		
	}
	
	// Update is called once per frame
	void Update () {

        if (adam.state == AdamState.Welcome)
        {
            AmbientAudio.clip = A_Light;
        }
        else {
            AmbientAudio.clip = B_Light;
        }

        AmbientAudio.Play(); 

		
	}
}
