using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour {


	public Sprite[] frames;
	private SpriteRenderer spriteRenderer;


	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine (PlayAnimation (0.15f));


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col.collider.CompareTag ("Player")) {
			Player player = col.transform.root.GetComponentInChildren<Player> ();
			if (player.gameObject.activeInHierarchy) {
				player.Die ();
			}
		}

	}

	IEnumerator PlayAnimation(float seconds) {
		int currentFrameIndex = 0;
		while (true) {
			spriteRenderer.sprite = frames [currentFrameIndex];
			yield return new WaitForSeconds(seconds);
			currentFrameIndex++;
			currentFrameIndex = currentFrameIndex%frames.Length;
		}

	}


}
