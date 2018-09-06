using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
	[Tooltip("The target transform to follow.")]
	public Transform target;


	// Update is called once per frame
	void LateUpdate () {
		if (target.position.y != transform.position.y) {
			transform.position = new Vector3 (transform.position.x, target.position.y, transform.position.z);

		}
	}
}
