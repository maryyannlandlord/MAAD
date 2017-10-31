using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseTrigger : MonoBehaviour {

    public static bool _triggered;

    

	// Use this for initialization
	void Start () {
       _triggered  = false; 
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera" && !_triggered)
        {
            Debug.Log("Triggered!");
            _triggered = true;
            AudioController.PlayAudioSource(gameObject);
        }

    }



}
