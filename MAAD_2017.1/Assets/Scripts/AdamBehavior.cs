using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Audio; 

// Keeps track of Adam's State

public enum AdamState
{
    Hiding,
    Idle,
    Eating,
    Surprise,
    Fly,
    Investigate,
    Welcome,
    FirstDemo, 
    FirstHold,
    Happy,
    SecDemo, 
    SecHold,
    FreakOut,
    Melting,
    FadeOut,
    FadeIn, 
    Wave
}


public class AdamBehavior : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public AdamState state;
   
    public AudioClip eating;
    public AudioClip surprise;
    public AudioClip melt;
    public AudioClip investigate;
    public AudioClip fly;
    public AudioClip happy;
    public AudioClip wave;
    public AudioClip idle;
    public AudioClip tracker1;
    public AudioClip tracker2; 
    

    private bool meltTriggered = false; 

    public Renderer[] renderers;


    public Animator[] clocks;
    public AudioSource clockgear1;
    public AudioSource clockgear2;

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
    [HideInInspector]
    public float startFadeIn; 
    
    public Transform _adamAnchor; 


    public static Transform AdamAnchor;
    public Transform TargetPoint1;
    public Transform TargetPoint2;
    public Transform TargetPoint3; 
    [HideInInspector]
    public Transform flyTarget;

    RubixManager rubix;

    private void Awake()
    {
        AdamAnchor = _adamAnchor;
    }


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rubix = GameObject.Find("Rubix").GetComponent<RubixManager>();
        audioSource = GetComponent<AudioSource>(); 
       
        

        StartFirstDemo = 0;
        StartSecDemo = 0;
        StartSecHold = 0;
        StartHappy = 0; 
        StartSurprise = 0;
        StartInvest = 0;
        StartIdle = 0;
        StartWelcome = 0;

        foreach (Animator clock in clocks) {
            clock.enabled = false; 
        }

        
        //AudioController.PlayAudioSource(this.gameObject, eating);
        
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
            case AdamState.Idle:
                animator.SetBool("Idle", false);
                audioSource.Stop();
                break;
            case AdamState.Happy:
                animator.SetBool("Happy", false);
                audioSource.Stop();
                break;
            case AdamState.Fly:
                animator.SetFloat("Fly", 0f);
                audioSource.Stop();
                break; 
            case AdamState.FirstDemo:
                animator.SetBool("FirstDemo", false);
                audioSource.Stop();
                break;
            case AdamState.FirstHold:
                animator.SetBool("FirstHold", false);
                break;
            case AdamState.SecDemo:
               animator.SetBool("SecDemo", false);
                audioSource.Stop();
                break;
            case AdamState.SecHold:
                animator.SetBool("SecHold", false);
                break;
            case AdamState.Eating:
                animator.SetBool("Eating", false);
                audioSource.Stop();
                break; 
            case AdamState.Surprise:
                animator.SetBool("Surprise", false);
                audioSource.Stop();
                break;
            case AdamState.Investigate:
                animator.SetBool("Investigate", false);
                audioSource.Stop();
                break;
            case AdamState.Welcome:
                animator.SetBool("Welcome", false);
                audioSource.Stop();
                break;
            case AdamState.Melting:
                animator.SetBool("Melting", false);
                audioSource.Stop();
                break;
            case AdamState.FreakOut:
                animator.SetBool("FreakOut", false);
                break;
            case AdamState.FadeOut:
                animator.SetBool("Idle", false);
                break;
            case AdamState.FadeIn:
                animator.SetBool("Idle", false);
                break;
            case AdamState.Wave:
                animator.SetBool("Wave", false);
                audioSource.Stop();
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

                audioSource.clip = idle;
                audioSource.loop = true;
                audioSource.Play();



                break;
            case AdamState.Happy:
                StartHappy = Time.time; 
                animator.SetBool("Happy", true);

                audioSource.clip = happy;
                audioSource.loop = true;
                audioSource.Play();

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
                else if (rubix.currentStage == RubixTargetState.FirstTracker)
                {

                    flyTarget = TargetPoint2;
                }
                else if (rubix.currentStage == RubixTargetState.End)
                {
                    flyTarget = TargetPoint3; 
                }

                audioSource.clip = fly;
                audioSource.loop = true;
                audioSource.Play();


                break;
            case AdamState.FirstDemo:
               StartFirstDemo = Time.time;
               animator.SetBool("FirstDemo", true);

                audioSource.clip = tracker1;
                audioSource.loop = true;
                audioSource.Play();


                break;
            case AdamState.FirstHold:
                animator.SetBool("FirstHold", true);
                break;
            case AdamState.SecDemo:
                StartSecDemo = Time.time;
                animator.SetBool("SecDemo", true);

                audioSource.clip = tracker2;
                audioSource.loop = true;
                audioSource.Play();

                break;
            case AdamState.SecHold:
                StartSecHold = Time.time;
                animator.SetBool("SecHold", true);
                break;
            case AdamState.Eating:
                animator.SetBool("Eating", true);
                audioSource.clip = eating;
                audioSource.loop = true; 
                audioSource.Play();

                break;
            case AdamState.Surprise:
                StartSurprise = Time.time;
                animator.SetBool("Surprise", true);

                audioSource.clip = surprise;
                audioSource.loop = false; 
                audioSource.Play();

                break;
            case AdamState.Investigate:
                StartInvest = Time.time; 
                animator.SetBool("Investigate", true);

                audioSource.clip = investigate;
                audioSource.loop = true;
                audioSource.Play();

                break;
            case AdamState.Welcome:
                StartWelcome = Time.time; 
                animator.SetBool("Welcome", true);

                foreach (Animator clock in clocks)
                {
                    clock.enabled = true;

                }
                clockgear1.Play();
                clockgear2.Play(); 

                break;
            case AdamState.Melting:
                animator.SetBool("Melting", true);
         
                 audioSource.clip = melt;
                 audioSource.loop = true;
                 audioSource.Play();


                break;
            case AdamState.FreakOut:
                animator.SetBool("FreakOut", true);   
                break;
            case AdamState.FadeOut:

                animator.SetBool("Idle", true);

                break;
            case AdamState.FadeIn:
                animator.SetBool("Idle", true);
                startFadeIn = Time.time; 
                break;
            case AdamState.Wave:
                animator.SetBool("Wave", true);

                audioSource.clip = wave;
                audioSource.loop = true;
                audioSource.Play();



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