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
    Welcome, 
    Melting,
    Fading
}


public class AdamBehavior : MonoBehaviour
{
    Animator animator;
    RubixManager rubix;
    [HideInInspector]
    public Fade adamFade;
    

    public Renderer[] renderers;

    public AdamState state;

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
    [HideInInspector]
    public float StartInvest;
    [HideInInspector]
    public float StartIdle;
    [HideInInspector]
    public float StartWelcome; 
    
    public Transform _adamAnchor; 


    public static Transform AdamAnchor;
    public Transform TargetPoint1;
    public Transform TargetPoint2; 
    [HideInInspector]
    public Transform flyTarget;


    private void Awake()
    {
        AdamAnchor = _adamAnchor;
    }


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rubix = GameObject.Find("Rubix").GetComponent<RubixManager>();
        adamFade = this.GetComponent<Fade>(); 


        StartFirstDemo = 0;
        StartSecDemo = 0;
        StartSecHold = 0;
        StartHappy = 0; 
        StartSurprise = 0;
        StartInvest = 0;
        StartIdle = 0;
        StartWelcome = 0;



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
            case AdamState.Investigate:
                animator.SetBool("Investigate", false);
                break;
            case AdamState.Welcome:
                animator.SetBool("Welcome", false);
                break;
            case AdamState.Melting:
                animator.SetBool("Melting", false);
                break;
            case AdamState.Fading:
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
            case AdamState.Idle:
                StartIdle = Time.time;
                animator.SetBool("Idle", true);
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
                if (rubix.currentStage == RubixTargetState.Intro)
                {
                    flyTarget = AdamAnchor;
                }
                else if (rubix.currentStage == RubixTargetState.Welcome)
                {
                    flyTarget = TargetPoint1;
                }
                else if (rubix.currentStage == RubixTargetState.FirstTracker) {

                    flyTarget = TargetPoint2;
                }
                 //flyTarget = (rubix.currentStage == RubixTargetState.Welcome) ? AdamAnchor : TargetPoint2;
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
            case AdamState.Investigate:
                StartInvest = Time.time; 
                animator.SetBool("Investigate", true);
                break;
            case AdamState.Welcome:
                StartWelcome = Time.time; 
                animator.SetBool("Welcome", true);
                break;
            case AdamState.Melting:
                animator.SetBool("Melting", true);
                break;
            case AdamState.Fading:
                //if (!adamFade.Wait(adamFade.sec)) adamFade.Fading();
                break;


        }

    }

    public void WelcomeTransition()
    {
        rubix.currentStage = RubixTargetState.FirstTracker;
        state = AdamState.Fly;

        Debug.Log("changed rubix state: " + rubix.currentStage);
        Debug.Log("changed adam state: " + state);

    }



}