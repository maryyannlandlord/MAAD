using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Vuforia
{
    public class SpawnSphere : MonoBehaviour
    {
        private bool spawned;
        //private bool prevState;
        //private bool currState; 


        // Use this for initialization
        void Start()
        {
            /*DefaultTrackableEventHandler tracker = GetComponent<DefaultTrackableEventHandler>();
            prevState = tracker.found;*/

            spawned = false; 
             

        }

        // Update is called once per frame
        void Update()
        {
            DefaultTrackableEventHandler tracker = GetComponent<DefaultTrackableEventHandler>();
            if (tracker.found && spawned == false) {

                //Vector3 screenPosition

            }

            /*currState = (GetComponent<DefaultTrackableEventHandler>()).found;

            if (currState != prevState) //if current State is different from Previous state
            {

                if (currState == true) // check if true
                {
                    Debug.Log("found");
                }
                else {
                    Debug.Log("Lost");
                }

            }

            prevState = currState; */

        }
    }

}

/*Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
                    Instantiate(ball, screenPosition, Quaternion.identity);*/