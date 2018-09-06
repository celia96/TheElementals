using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Coins : MonoBehaviour {


	[Tooltip("The individual sprites of the animation")]
	// frames of coins (Rotating)
	public Sprite[] frames;
	private SpriteRenderer spriteRenderer;
    public AudioClip collectSound;


	public static Coins instance;

	// Use this for initialization
	void Start () {
		instance = this;
		spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine (PlayAnimation (0.15f));


	}
		
	void OnTriggerEnter2D (Collider2D other) {
		// if the other object has the player tag...
		if (other.CompareTag ("Player")) {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
			CoinPanel.instance.CollectCoins ();
			Destroy (gameObject);
		}
	}
		


	/// <summary>
	/// This is a coroutine that cycles through the sprites of coin animation. It needs to be started using StartCoroutine().
	/// </summary>
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
