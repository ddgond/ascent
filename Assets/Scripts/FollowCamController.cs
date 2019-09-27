using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamController : MonoBehaviour {

	public Transform target;
	public float followFactor = 0.25f;
	public float verticalOffset = 2.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 matchedHeightPosition = transform.position;
		matchedHeightPosition.y = target.position.y + verticalOffset;
		transform.position = Vector3.Lerp (transform.position, matchedHeightPosition, followFactor);

		transform.LookAt (target);
	}
}
