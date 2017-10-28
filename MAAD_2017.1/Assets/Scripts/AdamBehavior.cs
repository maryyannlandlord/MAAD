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
    Fly,
    FirstDemo, 
    FirstHold, 
    SecDemo, 
    SecHold, 
    Eating, 
    Surprise, 
    Investigate, 
    Welcome
}


public class AdamBehavior : MonoBehaviour
{
    Animator animator;
    RubixManager rubix;
    

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
    [HideInInspector]
    public float StartSurprise; 

    public Transform TargetPoint1;
    public Transform _adamAnchor; 


    public static Transform AdamAnchor;
    Transform flyTarget;

    private void Awake()
    {
        AdamAnchor = _adamAnchor;
    }


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rubix = GameObject.Find("Rubix").GetComponent<RubixManager>();


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
            case AdamState.Fly:
                animator.SetFloat("Fly", 0f);
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
            case AdamState.Eating:
                animator.SetBool("Eating", false);
                break; 
            case AdamState.Surprise:
                animator.SetBool("Surprise", false);
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
            case AdamState.Fly:
                 animator.SetFloat("Fly", .1f);
                 flyTarget = (RubixManager.currentStage == RubixTargetState.Welcome) ? AdamAnchor : TargetPoint1;
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
            case AdamState.Eating:
                animator.SetBool("Eating", true);
                break;
            case AdamState.Surprise:
                StartSurprise = Time.time;
                animator.SetBool("Surprise", true);
                break; 


        }


    }

    public void Restart() {

        StartFirstDemo = 0;
        StartSecDemo = 0;
        StartSecHold = 0;


    }


}