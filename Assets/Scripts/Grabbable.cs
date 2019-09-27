using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {
	
	public GrabbableType grabbableType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool MatchesType (GrabbableType otherType) {
		return grabbableType == otherType;
	}
}
