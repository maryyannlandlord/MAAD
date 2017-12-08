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
    //public AudioSource[] flowerPlayer;
    //public AudioClip[] flowerBloomsAudio;
    public GameObject flowerPlayer;
    public GameObject flowerPlayer2; 
    //public AudioClip flowerBloomAudio; 
    private int random;


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
            case FlowerState.Waiting:
                animator.SetBool("Waiting", false);
                break;

        }
        switch (state)
        {
            case FlowerState.Waiting:
                animator.SetBool("Waiting", true);
                break;
            case FlowerState.Bloom:
                animator.SetBool("Bloom", true);

                /*foreach(AudioSource sound in flowerPlayer)
                {
                    random = Random.Range(0, (flowerBloomsAudio.Length));
                    sound.clip = flowerBloomsAudio[random];
                }*/

                AudioController.PlayAudioSource(flowerPlayer);
                AudioController.PlayAudioSource(flowerPlayer2);

                //flowerPlayer.clip = flowerBloomAudio;
                //flowerPlayer.Play(); 

                break;
            
        }

    }


}





