using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Vuforia;

// Keeps track of current Rubix/World state, next tracker to look for, and determines Adam's state
// Triggers things to change in the world 

public enum RubixTargetState
{
    Welcome = 0,
    FirstTracker = 1,
    SecTracker = 2,
    End = 3
}

public class RubixManager : MonoBehaviour {



    public Action<RubixTargetState> Success;
    public Action<RubixTargetState> Fail;

    public RubixTargetState currentStage;
    public DefaultTrackableEventHandler[] trackers;

    public float FirstDemoDur;
    public float SecDemoDur;
    public float HappyDur;
    public float[] MeltingTimeTriggers = new float[] { 20, 30, 45, 50 };

    bool startTracker; // allows Interactive phyiscal experience
    private int counter; 

    AdamBehavior adam;


    // Use this for initialization
    void Start() {
        currentStage = RubixTargetState.Welcome;  //after Welcome animation plays, stage -> FirstTracker 

        Success += TrackerSuccess;
        Fail += TrackerFail;

        adam = GameObject.Find("Adam").GetComponent<AdamBehavior>(); //Find Adam in the scene
        adam.SetState(AdamState.Hiding); // Set Adam initial state

        Debug.Log("Rubix State:" + currentStage);

    }

    // Update is called once per frame
    void Update() {
        WorldState();
        TrackerStatus();
    }

    public void WorldState() {

        if (AdamBehavior.state == AdamState.FirstDemo) //Adam is demoing the first symbol 
        {

            if (Time.time >= adam.StartFirstDemo + FirstDemoDur)
            {

                adam.SetState(AdamState.FirstHold); // Adam now holds the 1st symbol
            }

        }
        else if (AdamBehavior.state == AdamState.SecDemo) //Adam is demoing the 2nd symbol
        {

            if (Time.time >= adam.StartSecDemo + SecDemoDur)
            {
                adam.SetState(AdamState.SecHold);
            }
        }
        else if (AdamBehavior.state == AdamState.Happy) // Ball not transitioning to next state when looking trackers 
        {
            if (Time.time >= adam.StartHappy + HappyDur)
            {
                
                if (currentStage == RubixTargetState.SecTracker)
                {
                    Debug.Log("reached time");
                    adam.SetState(AdamState.SecDemo);
                }
            }
        }
        else if (AdamBehavior.state == AdamState.SecHold && currentStage == RubixTargetState.SecTracker)
        {
            if (Time.time >= adam.StartSecHold + MeltingTimeTriggers[3]) // 50seconds
            {
                Debug.Log("50 seconds!");
            }
            else if (Time.time >= adam.StartSecHold + MeltingTimeTriggers[2]) // 45 seconds
            {
                Debug.Log("45 seconds!");
            }
            else if (Time.time >= adam.StartSecHold + MeltingTimeTriggers[1]) // 30 seconds
            {
                Debug.Log("30 seconds!");
            }
            else if (Time.time >= adam.StartSecHold + MeltingTimeTriggers[0]) // 20 seconds
            {
                Debug.Log("20 seconds!");
            }
           

        }
    }

    public void TrackerStatus() {

        foreach (DefaultTrackableEventHandler tracker in trackers)
        {
            if (tracker.found) {

                if (tracker.mTrackableBehaviour.TrackableName == "Astronaut") {
                    if (currentStage == RubixTargetState.FirstTracker)
                    {
                        Debug.Log("Astronaut!");
                        Success(currentStage);
                        currentStage = RubixTargetState.SecTracker;

                    }
                }
                else if (tracker.mTrackableBehaviour.TrackableName == "Oxygen") {
                    if (currentStage == RubixTargetState.SecTracker) {
                        Debug.Log("Oxygen!");
                    }

                }

            }
        }
    }



    public void TrackerSuccess(RubixTargetState targetStage) {
         // Tracker created successfully

         switch (targetStage)
         {
            case RubixTargetState.Welcome:
                adam.SetState(AdamState.FirstDemo);
                break; 
            case RubixTargetState.FirstTracker:
                adam.SetState(AdamState.Happy);
                // bloom flower 
                break;
            case RubixTargetState.SecTracker:
                Debug.Log("Second Success!");
                //everything gets sucked into cube 
                //play visual music
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

    public void Restart()
    {
        currentStage = RubixTargetState.Welcome;

        Success += TrackerSuccess;
        Fail += TrackerFail;

        adam = GameObject.Find("Adam").GetComponent<AdamBehavior>();
        adam.SetState(AdamState.Hiding); // doesn't reset Adam's state; 
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