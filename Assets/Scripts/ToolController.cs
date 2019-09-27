using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour {

	public Range[] ranges;
	private Transform tf;

	// Use this for initialization
	void Start () {
		tf = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		tf.SetPositionAndRotation (tf.position, CalculateRotation (tf.position.y));
	}

	Quaternion CalculateRotation(float height) {
		for (int i = 0; i < ranges.Length; i++) {
			Range range = ranges[i];
			if (range.contains(height)) {
				return Quaternion.Euler (0, range.getRotation (height), 0);
			}
		}
		return Quaternion.Euler (0, 0, 0);
	}

	[System.Serializable]
	public class Range {
		public float minHeight;
		public float maxHeight;
		public float minRotation;
		public float maxRotation;

		public bool contains(float height) {
			return height >= minHeight && height < maxHeight;
		}

		public float getRotation(float height) {
			float frac = Mathf.InverseLerp (minHeight, maxHeight, height);
			return Mathf.Lerp (minRotation, maxRotation, frac);
		}
	}
}
