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
        AmbientAudio.clip = B_Light;
        AmbientAudio.Play(); 
		
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log("ambience clip: " + AmbientAudio.clip);
        if (adam.state == AdamState.Welcome)
        { AmbientAudio.clip = A_Light;
          AmbientAudio.Play();
        } 
        else if (adam.state == AdamState.FreakOut)
        {
            AmbientAudio.Stop();
        }
        else if (adam.state == AdamState.FadeIn) {
            AmbientAudio.clip = A_Light;
            AmbientAudio.Play(); 
        }
		
	}
}
