﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;

// Keeps track of current Rubix/World state, next tracker to look for, and determines Adam's state
// Triggers things to change in the world 

public enum RubixTargetState
{
    Intro = 0,
    Welcome = 1,
    FirstTracker = 2,
    SecTracker = 3,
    End = 4
}

public class RubixManager : MonoBehaviour {

    AdamBehavior adam;
    public TransBetween[] faders;
    public TransBetween adamFade; 
    public flowerBehavior[] flower;
    Transform targetPlayerLOS;

    public Action<RubixTargetState> Success;
    public Action<RubixTargetState> Fail;

    public RubixTargetState currentStage;
    public DefaultTrackableEventHandler[] trackers;

    public float FirstDemoDur;
    public float SecDemoDur;
    public float SurpriseDur;
    public float HappyDur;
    public float IdleDur;
    public float InvestDur;



    public float[] MeltingTimeTriggers = new float[] { 20, 30, 45, 50 };
    public float flyTargetSpeed = .01f;
    public float FlyToPlayerSpeed = .3f;
    public float rotSpeed = 0.02f;


    public GameObject musicSphere;
    private Renderer musicRenderer;
    private VideoPlayer musicPlayer;



    bool startTracker; // allows Interactive phyiscal experience
    private int counter;
    private Quaternion newLookRot;

    public Transform playerTransform;


    // Use this for initialization
    void Start() {
        currentStage = RubixTargetState.Intro;

        Success += TrackerSuccess;
        Fail += TrackerFail;

        adam = GameObject.Find("Adam").GetComponent<AdamBehavior>(); // Find Adam in the scene
        adam.SetState(AdamState.Eating); // Set Adam initial state 

        musicRenderer = musicSphere.GetComponent<Renderer>();
        musicRenderer.enabled = false;
        musicPlayer = musicSphere.GetComponent<VideoPlayer>(); 

    }

    // Update is called once per frame
    void Update() {
        WorldState();
        TrackerStatus();
    }

    public void WorldState() {

        if (adam.state == AdamState.Eating)
        {
            Debug.Log("Eating");
            if (SurpriseTrigger._triggered == true)
            {
                adam.SetState(AdamState.Surprise);
            }

        }
        else if (adam.state == AdamState.Surprise)
        {
            Debug.Log("Surprise");
            newLookRot = Quaternion.LookRotation(playerTransform.position - adam.transform.position);
            adam.transform.rotation = Quaternion.Lerp(adam.transform.rotation, newLookRot, rotSpeed);
            if (Time.time >= adam.StartSurprise + SurpriseDur)
            {

                adam.SetState(AdamState.Fly);
            }

        }
        else if (adam.state == AdamState.Fly)
        {
            //Debug.Log("Destination:" + adam.flyTarget);

            adam.transform.position = Vector3.Lerp(adam.transform.position, adam.flyTarget.position, FlyToPlayerSpeed);

            if (currentStage == RubixTargetState.Intro || currentStage == RubixTargetState.Welcome) {
                newLookRot = Quaternion.LookRotation(playerTransform.position - adam.transform.position);
            }
            else {
                newLookRot = Quaternion.LookRotation(adam.flyTarget.position - adam.transform.position);
            }

            adam.transform.rotation = Quaternion.Lerp(adam.transform.rotation, newLookRot, rotSpeed);

            if (Vector3.Distance(adam.transform.position, adam.flyTarget.position) < .2f)
            {
                newLookRot = Quaternion.LookRotation(playerTransform.position - adam.transform.position);
                adam.transform.rotation = Quaternion.Lerp(adam.transform.rotation, newLookRot, rotSpeed);

                if (currentStage == RubixTargetState.Welcome) { adam.SetState(AdamState.Welcome); }
                else { adam.SetState(AdamState.Idle); }



            }
        }
        else if (adam.state == AdamState.Idle)
        {

            newLookRot = Quaternion.LookRotation(playerTransform.position - adam.transform.position);
            adam.transform.rotation = Quaternion.Lerp(adam.transform.rotation, newLookRot, rotSpeed);

            if (currentStage == RubixTargetState.Intro)

            {
                Debug.Log("Giving a pause");
                adam.transform.position = Vector3.Lerp(adam.transform.position, adam.flyTarget.position, flyTargetSpeed);
                if (Time.time >= adam.StartIdle + IdleDur)
                {
                    Success(currentStage);
                    currentStage = (RubixTargetState)((int)currentStage + 1);


                }
            }
            else if(currentStage == RubixTargetState.FirstTracker)
            {
                Debug.Log("Waiting for Player");
                if (Time.time >= adam.StartIdle + IdleDur)
                {
                    adam.SetState(AdamState.FirstDemo);
                }
            }

        }
        else if (adam.state == AdamState.Investigate)
        {
            newLookRot = Quaternion.LookRotation(playerTransform.position - adam.transform.position);
            adam.transform.rotation = Quaternion.Lerp(adam.transform.rotation, newLookRot, rotSpeed);

            if (Time.time >= adam.StartInvest + InvestDur)
            {
                adam.SetState(AdamState.Fly);

            }

        }
        else if (adam.state == AdamState.Welcome) {

            if (((Time.time - adam.StartWelcome) / 4.667) >= 1) {

                Debug.Log("How much time has passed:" + (Time.time - adam.StartWelcome));

                currentStage = (RubixTargetState)((int)currentStage + 1); //firstTracker
                adam.SetState(AdamState.Fly);

            }

        }
        else if (adam.state == AdamState.FirstDemo) //Adam is demoing the first symbol 
        {
            newLookRot = Quaternion.LookRotation(playerTransform.position - adam.transform.position);
            adam.transform.rotation = Quaternion.Lerp(adam.transform.rotation, newLookRot, rotSpeed);
            if (Time.time >= adam.StartFirstDemo + FirstDemoDur)
            {

                adam.SetState(AdamState.FirstHold); // Adam now holds the 1st symbol
            }

        }
        else if (adam.state == AdamState.SecDemo) //Adam is demoing the 2nd symbol
        {
            newLookRot = Quaternion.LookRotation(playerTransform.position - adam.transform.position);
            adam.transform.rotation = Quaternion.Lerp(adam.transform.rotation, newLookRot, rotSpeed);
            if (Time.time >= adam.StartSecDemo + SecDemoDur)
            {
                adam.SetState(AdamState.SecHold);
            }
        }
        else if (adam.state == AdamState.Happy)
        {
            if (Time.time >= adam.StartHappy + HappyDur)
            {

                if (currentStage == RubixTargetState.SecTracker)
                {
                    adam.SetState(AdamState.SecDemo);
                }
            }
        }
        else if (currentStage == RubixTargetState.SecTracker && ((adam.state == AdamState.SecHold) || (adam.state == AdamState.FreakOut) || (adam.state == AdamState.Melting)))
        {
            if (Time.time >= adam.StartSecHold + MeltingTimeTriggers[3]) // 60 seconds
            {
                Debug.Log("60 seconds!");
                // pedistal glows more 
                if (adamFade.Wait(adamFade.FadewaitTime)) adamFade.Fading();
            }
            else if (Time.time >= adam.StartSecHold + MeltingTimeTriggers[2]) // 45 seconds
            {
                Debug.Log("45 seconds!");
                
                adam.SetState(AdamState.Melting);
                if (adamFade.Wait(adamFade.FossilwaitTime)) adamFade.Fossilize(); 
                // pedistal glows
            }
            else if (Time.time >= adam.StartSecHold + MeltingTimeTriggers[1]) // 40 seconds
            {
                Debug.Log("40 seconds!");             

                foreach (TransBetween f in faders)
                {
                    if (f.Wait(f.FadewaitTime)) // fossilize after waiting few seconds 
                    {

                        f.Fading();
                    }

                }

            }
            else if (Time.time >= adam.StartSecHold + MeltingTimeTriggers[0]) // 30 seconds
            {
                Debug.Log("30 seconds!");

                adam.SetState(AdamState.FreakOut);

                foreach (TransBetween f in faders)
                {
                    Debug.Log("entered fossil forloop");
                    if (f.Wait(f.FossilwaitTime)) // fossilize after waiting few seconds 
                    {
                        Debug.Log("fossilize!");
                        f.Fossilize();
                    }
                        
                }
                // pedestal glow (10s 50%)
            }

        }
    }

    public void TrackerStatus() {

        foreach (DefaultTrackableEventHandler tracker in trackers)
        {
            if (tracker.found) {

                Debug.Log("You found a tracker!");

                if (tracker.mTrackableBehaviour.TrackableName == "MAAD_FirstTracker") {
                    if (currentStage == RubixTargetState.FirstTracker && (adam.state == AdamState.FirstHold))
                    {
                        Debug.Log("TrackerFound!");
                        Success(currentStage);
                        currentStage = (RubixTargetState)((int)currentStage + 1); // currentStage -> SecTracker
                        Debug.Log("After found 1st tracker stage: " + currentStage);

                    }
                }

                else if (tracker.mTrackableBehaviour.TrackableName == "MAAD_ThirdTracker_v1") {
                    Debug.Log("TrackerFound again!");

                    if (currentStage == RubixTargetState.SecTracker && ((adam.state == AdamState.FreakOut ) || (adam.state == AdamState.Melting))) {
                        
                        Success(currentStage);
                        currentStage = (RubixTargetState)((int)currentStage + 1);

                        
                        if (adamFade.Wait(2))
                        {
                            adamFade.Fadedur = 5.0f; 
                            adamFade.Fading();
                        } 

                        Debug.Log("LargetTracker3!");
                    }

                }

            }
        }
    }



    public void TrackerSuccess(RubixTargetState targetStage) {
        // Tracker created successfully

        switch (targetStage)
        {
            case RubixTargetState.Intro:
                adam.SetState(AdamState.Investigate);
                break;
            case RubixTargetState.Welcome:
                break;
            case RubixTargetState.FirstTracker:
                adam.SetState(AdamState.Happy);
                foreach (flowerBehavior plant in flower)
                {
                    plant.SetState(FlowerState.Bloom);
                }
                break;
            case RubixTargetState.SecTracker:
                Debug.Log("Second Success!");
                
                
                musicRenderer.enabled = true;
                //musicPlayer.Play();
                AudioController.PlayAudioSource(musicSphere);

                break;
            case RubixTargetState.End:
                //adam.SetState(AdamState.Rebirth); 
                //trigger flowers

                break;
        }
    }

    public void TrackerFail(RubixTargetState targetStage)
    {
        //AudioController.PlayAudioSource(kamidanaObject, failClips[Random.Range(0, failClips.Length - 1)]);
        switch (targetStage)
        {
            case RubixTargetState.Intro:
                break;
            case RubixTargetState.Welcome:
                break;
            case RubixTargetState.FirstTracker:
                break;
            case RubixTargetState.SecTracker:
                break;
            case RubixTargetState.End:
                break;
        }
    }



    public void TriggerSuccess()
    {
        if (Success != null) Success(currentStage);
        currentStage = (RubixTargetState)((int)currentStage + 1);
        Debug.Log("Rubix State:" + currentStage);
    }


} 

/*
public void KamidanaSuccess(ShrineTargetStage targetStage)
{
    AudioController.PlayAudioSource(kamidanaObject, successClips[Random.Range(0, successClips.Length - 1)]);
    switch (targetStage)
    {
        case ShrineTargetStage.Candy:
            SetState(ZashikiState.Idle);
            fallingObjectsSpawn.StopSpawn();
            scroll1.TriggerAction();
            break;
        case ShrineTargetStage.Coin:
            swarmController.enabled = true;
            //darumaObject.SetActive(true);
            scroll2.TriggerAction();
            break;
        case ShrineTargetStage.Daruma:
            swarmCollider.enabled = false;
            swarmFlySound.Play();
            swarmTargetTransform.position = swarmTargetToGo.position;
            swarmController.maxVelocity = swarmFlyMaxVelocity;
            wallCollider.enabled = true;
            //underworld.SetActive(true);
            //darumaObject.SetActive(false);
            break;
    }
}

void KamidanaFail(ShrineTargetStage targetStage)
{
    AudioController.PlayAudioSource(kamidanaObject, failClips[Random.Range(0, failClips.Length - 1)]);
    switch (targetStage)
    {
        case ShrineTargetStage.Candy:
            break;
        case ShrineTargetStage.Coin:
            fallingObjectsSpawn.SpawnFor(3);
            SetState(ZashikiState.Angry);
            break;
        case ShrineTargetStage.Daruma:
            fallingObjectsSpawn.SpawnFor(3);
            SetState(ZashikiState.Angry);
            break;
    }
}
*/
  