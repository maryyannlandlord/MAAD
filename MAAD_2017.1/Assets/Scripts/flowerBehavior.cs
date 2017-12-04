using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowerState
{
    Waiting, 
    Bloom, 
    Melt
}

public class flowerBehavior : MonoBehaviour {

    Animator animator;
    public FlowerState state;
    public GameObject[] flowerSounds; 


    // Use this for initialization
    void Start () {

        animator = GetComponent<Animator>(); 

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetState(FlowerState newState) {

        FlowerState prevstate = state;
        state = newState;

        switch(prevstate){

            case FlowerState.Bloom:
                animator.SetBool("Bloom", false);
                break; 

        }
        switch (state)
        {
            case FlowerState.Waiting:
                animator.SetBool("Waiting", true);
                break;
            case FlowerState.Bloom:
                animator.SetBool("Bloom", true);

                foreach(GameObject sound in flowerSounds)
                {
                    AudioController.PlayAudioSource(sound);
                }

                break;
            
        }

    } 
}
