using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System;
using System.Runtime.InteropServices;

public class Climber : MonoBehaviour {

	public SteamVR_Input_Sources leftHandType;
	public SteamVR_Input_Sources rightHandType;

	public SteamVR_Action_Boolean grabPinch;
	public SteamVR_Action_Boolean grabGrip;
	public SteamVR_Action_Vibration vibration;

	public Hand leftHand;
	public Hand rightHand;
	public GameObject headset;

	private Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (grabPinch.GetStateDown (leftHandType)) {
			AttemptGrab (leftHand);
		}
		if (grabPinch.GetStateDown (rightHandType)) {
			AttemptGrab (rightHand);
		}
		if (grabGrip.GetStateDown (leftHandType)) {
			// leftHand.ToggleType ();
			leftHand.AttemptGrabTool ();
		}
		if (grabGrip.GetStateDown (rightHandType)) {
			// rightHand.ToggleType ();
			rightHand.AttemptGrabTool ();
		}
	}

	void LateUpdate () {
		Hand grabbingHand = GetGrabbingHand ();
		if (grabbingHand != null) {
			transform.position = transform.position - (grabbingHand.GetOffset () - this.GetOffset ());
		}
		UpdateLastPosition ();
		UpdateLastHandPositions ();
	}

	void AttemptGrab(Hand hand) {
		if (hand.CanGrab()) {
			ReleaseGrabbingHand ();
			hand.Grab ();
			VibrateHand (hand);
		}
	}

	void ReleaseGrabbingHand () {
		Hand grabbingHand = GetGrabbingHand ();
		if (grabbingHand != null) {
			grabbingHand.Release ();
		}
	}

	void VibrateHand (Hand hand) {
		if (hand == leftHand) {
			vibration.Execute (0f, 0.2f, 40f, 1f, leftHandType);
		} else {
			vibration.Execute (0f, 0.2f, 40f, 1f, rightHandType);
		}
	}

	Hand GetGrabbingHand() {
		if (leftHand.IsGrabbing ()) {
			return leftHand;
		}
		if (rightHand.IsGrabbing ()) {
			return rightHand;
		}
		return null;
	}

	public Vector3 GetOffset () {
		return transform.position - lastPosition;
	}

	public void UpdateLastPosition () {
		lastPosition = transform.position;
	}

	void UpdateLastHandPositions() {
		leftHand.UpdateLastPosition ();
		rightHand.UpdateLastPosition ();
	}
}
