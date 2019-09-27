using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Security.Cryptography;
using System.ComponentModel;

public class Hand : MonoBehaviour {

	public GameObject circlePrefab;
	public GameObject squarePrefab;
	public GameObject trianglePrefab;

	private GameObject typeIndicator;
	private GrabbableType grabbableType = GrabbableType.Circle;
	private bool isGrabbing = false;
	private bool inGrabRange = false;
	private bool inToolRange = false;
	private Tool toolInRange;
	private Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		lastPosition = transform.position;
		UpdateTypeIndicator ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.CompareTag ("Grabbable") && other.gameObject.GetComponent<Grabbable> ().MatchesType (grabbableType)) {
			inGrabRange = true;
		} else if (other.gameObject.CompareTag ("Grabbable")) {
			inGrabRange = false;
		}

		if (other.gameObject.CompareTag ("Tool")) {
			inToolRange = true;
			toolInRange = other.gameObject.GetComponent<Tool> ();
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag ("Grabbable")) {
			inGrabRange = false;
		}
		if (other.gameObject.CompareTag ("Tool")) {
			inToolRange = false;
			toolInRange = null;
		}
	}

	void UpdateTypeIndicator () {
		if (typeIndicator != null) {
			Destroy (typeIndicator);
			typeIndicator = null;
		}
		GameObject prefab;
		switch (grabbableType) {
		case GrabbableType.Circle:
			prefab = circlePrefab;
			break;
		case GrabbableType.Square:
			prefab = squarePrefab;
			break;
		case GrabbableType.Triangle:
			prefab = trianglePrefab;
			break;
		default:
			prefab = circlePrefab;
			break;
		}
		typeIndicator = Instantiate (prefab, this.transform, false);
	}

	public void UpdateLastPosition () {
		lastPosition = transform.position;
	}

	public bool IsGrabbing () {
		return isGrabbing;
	}

	public bool InGrabRange () {
		return inGrabRange;
	}

	public bool InToolRange () {
		return inToolRange;
	}

	public bool CanGrab () {
		return !IsGrabbing () && InGrabRange ();
	}

	public void Grab () {
		isGrabbing = true;
	}

	public void Release () {
		isGrabbing = false;
	}

	public void AttemptGrabTool () {
		if (InToolRange () && toolInRange != null) {
			SwitchToType (toolInRange.GetType ());
		}
	}

	public void ToggleType () {
		switch (grabbableType) {
		case GrabbableType.Circle:
			SwitchToType (GrabbableType.Square);
			break;
		case GrabbableType.Square:
			SwitchToType (GrabbableType.Triangle);
			break;
		case GrabbableType.Triangle:
			SwitchToType (GrabbableType.Circle);
			break;
		default:
			break;
		}
	}

	public void SwitchToType (GrabbableType newType) {
		grabbableType = newType;
		UpdateTypeIndicator ();
	}

	public Vector3 GetOffset () {
		return transform.position - lastPosition;
	}
}
