using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	public GameObject deathEffect;

	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "shield") {
			if (gameObject.activeInHierarchy) {
				Die ();
			}

		}
		if (col.collider.CompareTag("Player"))
		{
			Player player = col.transform.root.GetComponentInChildren<Player>();
			if (player.gameObject.activeInHierarchy) {
				player.Die ();
			}
		}

	}

	IEnumerator explosionEffect() {
		gameObject.SetActive(false);
		Instantiate(deathEffect, transform.position, transform.rotation);
		yield return new WaitForSeconds(1);
	}

	void Die()
	{
		StartCoroutine (explosionEffect ());
		Invoke ("Remove" , 1);

	}

	void Remove (){
		Destroy (gameObject);
	}

}
