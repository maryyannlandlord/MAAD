using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCam : MonoBehaviour {

    private Vector3 lastTrackerToUnityTranslation = Vector3.zero;
    private Quaternion lastTrackerToUnityRotation = Quaternion.identity;

    private Transform cameraTransform;

    // Use this for initialization
    void Start () {
        cameraTransform = GetComponent<Camera>().transform;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

	}
}
