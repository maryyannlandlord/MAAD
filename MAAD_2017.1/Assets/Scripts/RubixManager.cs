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

    AdamBehavior adam;
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
    public float floatLerpSpeed = .01f;
    public float FlySpeed = .3f; 


    bool startTracker; // allows Interactive phyiscal experience
    private int counter;

    public Transform playerTransform;
    

    // Use this for initialization
    void Start() {
        currentStage = RubixTargetState.Welcome; 

        Success += TrackerSuccess;
        Fail += TrackerFail;

        adam = GameObject.Find("Adam").GetComponent<AdamBehavior>(); // Find Adam in the scene
        adam.SetState(AdamState.Eating); // Set Adam initial state 


        targetPlayerLOS = AdamBehavior.AdamAnchor;

        Debug.Log("Rubix State:" + currentStage);

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
            adam.transform.rotation = Quaternion.LookRotation(playerTransform.position - adam.transform.position);
            if (Time.time >= adam.StartSurprise + SurpriseDur)
            {

                adam.SetState(AdamState.Fly);
            }

        }
        else if (adam.state == AdamState.Fly)
        {
            /*currentPosition = (Time.time - startTime)/tripTime; */

            adam.transform.position = Vector3.Lerp(adam.transform.position, adam.flyTarget.position, FlySpeed);
            adam.transform.rotation = Quaternion.LookRotation(playerTransform.position - adam.transform.position); // if player runs...adam will chase them....
            Debug.Log("Destination:" + adam.flyTarget);
            if (Vector3.Distance(adam.transform.position, adam.flyTarget.position) < .2f)
            {
                
                adam.SetState(AdamState.Idle);

            }
        }
        else if (adam.state == AdamState.Idle)
        {
            //adam.transform.position = Vector3.Lerp(adam.transform.position, targetPlayerLOS.position, floatLerpSpeed);
            adam.transform.rotation = Quaternion.LookRotation(playerTransform.position - adam.transform.position);

            if (currentStage == RubixTargetState.Welcome)
            {
                Debug.Log("Giving a pause");
                adam.transform.position = Vector3.Lerp(adam.transform.position, adam.flyTarget.position, floatLerpSpeed);
                if (Time.time >= adam.StartIdle + IdleDur)
                {
                    Success(currentStage);

                }
            }
            else if (currentStage == RubixTargetState.FirstTracker)
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

            adam.transform.rotation = Quaternion.LookRotation(playerTransform.position - adam.transform.position); //not sure if need 
            adam.transform.position = Vector3.Lerp(adam.transform.position, adam.TargetPoint1.position, floatLerpSpeed);
            if (Time.time >= adam.StartInvest + 2)
            {

                adam.SetState(AdamState.Welcome); // fix the transition 

            }

        }
        else if (adam.state == AdamState.Welcome) {

            if (( Time.time - adam.StartWelcome)/5 >= 1) {

                currentStage = RubixTargetState.FirstTracker;
                adam.SetState(AdamState.Fly);
            }

        }
        else if (adam.state == AdamState.FirstDemo) //Adam is demoing the first symbol 
        {
            adam.transform.rotation = Quaternion.LookRotation(playerTransform.position - adam.transform.position);
            if (Time.time >= adam.StartFirstDemo + FirstDemoDur)
            {

                adam.SetState(AdamState.FirstHold); // Adam now holds the 1st symbol
            }

        }
        else if (adam.state == AdamState.SecDemo) //Adam is demoing the 2nd symbol
        {
            adam.transform.rotation = Quaternion.LookRotation(playerTransform.position - adam.transform.position);
            if (Time.time >= adam.StartSecDemo + SecDemoDur)
            {
                adam.SetState(AdamState.SecHold);
            }
        }
        else if (adam.state == AdamState.Happy) // Ball not transitioning to next state when looking trackers 
        {
            if (Time.time >= adam.StartHappy + HappyDur)
            {

                if (currentStage == RubixTargetState.SecTracker)
                {
                    adam.SetState(AdamState.SecDemo);
                }
            }
        }
        else if (adam.state == AdamState.SecHold && currentStage == RubixTargetState.SecTracker)
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
                        Success(currentStage);
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
                adam.SetState(AdamState.Investigate);
                //adam.SetState(AdamState.Fly);
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
  