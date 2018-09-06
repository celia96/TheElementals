using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAnimation : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	public float seconds;	


	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine(PlayAnimation(seconds));
	}


	IEnumerator PlayAnimation(float seconds)
	{
		while (true)
		{
			if (spriteRenderer.flipX == false) {
				spriteRenderer.flipX = true;
			} else {
				spriteRenderer.flipX = false;
			}
			yield return new WaitForSeconds(seconds);
		}

	}
}
