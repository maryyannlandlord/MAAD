using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

// Keeps track of Adam's State

public enum AdamState
{
    Hiding,
    Idle,
    Happy,
    Spinning,
    Hop, 
    FirstDemo, 
    FirstHold, 
    SecDemo, 
    SecHold
}


public class AdamBehavior : MonoBehaviour
{
    Animator animator;
    public Renderer[] renderers;

    public static AdamState state;

    [HideInInspector]
    public float StartFirstDemo;
    [HideInInspector]
    public float StartSecDemo;
    [HideInInspector]
    public float StartSecHold;
    [HideInInspector]
    public float StartHappy; 


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        
        StartFirstDemo = 0;
        StartSecDemo = 0;
        StartSecHold = 0;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetState(AdamState newState) {

        AdamState prevstate = state;
        state = newState;

        switch(prevstate)
        {

            case AdamState.Hiding:
                foreach (Renderer r in renderers)
                {
                    r.enabled = true; 
                    
                }
                break;
            case AdamState.Happy:
                animator.SetBool("Happy", false);
                break;
            case AdamState.Spinning:
                animator.SetBool("Spinning", false);
                break;
            case AdamState.Hop:
                animator.SetBool("Hop", false);
                break;
            case AdamState.FirstDemo:
                animator.SetBool("FirstDemo", false);
                break;
            case AdamState.FirstHold:
                animator.SetBool("FirstHold", false);
                break;
            case AdamState.SecDemo:
                animator.SetBool("SecDemo", false);
                break;
            case AdamState.SecHold:
                animator.SetBool("SecHold", false);
                break;

        }
        switch(state)
        {
            case AdamState.Hiding: 
                foreach (Renderer r in renderers)
                {
                    r.enabled = false; 
                }
                break;
            case AdamState.Happy:
                StartHappy = Time.time; 
                animator.SetBool("Happy", true);
                break;
            case AdamState.Spinning:
                animator.SetBool("Spinning", true);
                break;
            case AdamState.Hop:
                animator.SetBool("Hop", true);
                break;
            case AdamState.FirstDemo:
                StartFirstDemo = Time.time;
                animator.SetBool("FirstDemo", true);
                break;
            case AdamState.FirstHold:
                animator.SetBool("FirstHold", true);
                break;
            case AdamState.SecDemo:
                StartSecDemo = Time.time;
                animator.SetBool("SecDemo", true);
                break;
            case AdamState.SecHold:
                StartSecHold = Time.time;
                animator.SetBool("SecHold", true);
                break;


        }


    }

    public void Restart() {

        StartFirstDemo = 0;
        StartSecDemo = 0;
        StartSecHold = 0;

        // Need to destroy and remake adam to set to Hiding 


    }


}