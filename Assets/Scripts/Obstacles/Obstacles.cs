using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripts for the sides 
public class Obstacles : MonoBehaviour {

	void Start () {

	}

	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.CompareTag("Player"))
		{
			Player player = col.transform.root.GetComponentInChildren<Player>();
			if (player.gameObject.activeInHierarchy) {
				player.Die ();
			}
		}

	}

}
