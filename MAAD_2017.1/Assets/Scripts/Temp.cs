using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour {

    Fade fader;
    

    // Use this for initialization
    void Start () {

        fader = this.GetComponent<Fade>(); 
        
	}
	
	// Update is called once per frame
	void Update () {
        if (fader.Wait(3)) fader.Fading();

    }
}
