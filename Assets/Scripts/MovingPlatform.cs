using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	
	/// <summary>The objects initial position.</summary>
	private Vector2 startPosition;
	/// <summary>The objects updated position for the next frame.</summary>
	private Vector2 newPosition;

	/// <summary>The speed at which the object moves.</summary>
	public float speed = 3;
	/// <summary>The maximum distance the object may move in either y direction.</summary>
	public float maxDistance = 1f;

	void Start()
	{
		startPosition = transform.position;
		newPosition = transform.position;
	}

	void Update()
	{
		newPosition.x = startPosition.x + (maxDistance * Mathf.Sin(Time.time * speed));
		transform.position = newPosition;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.tag == "Player") {
			other.transform.parent = transform;
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		if (other.transform.tag == "Player") {
			if (other.gameObject.activeSelf) { //Return if the object is active or not
				other.transform.parent = null;
			}

		}
	}
	
}


