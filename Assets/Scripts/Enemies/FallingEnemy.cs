using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemy : Killable {

	[Tooltip("The individual sprites of the animation")]
	public Sprite[] frames;
	SpriteRenderer spriteRenderer;
	private float speed;
	public float minSpeed = 2;
	public float maxSpeed = 5;
	[Tooltip("How fast does the animation play")]
	public float seconds;

	public GameObject deathEffect;
    public AudioClip deathSound;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine(PlayAnimation());
		speed = Random.Range (minSpeed, maxSpeed);
	}

	// Update is called once per frame
	void Update () {

		transform.position = transform.position + Vector3.down * speed * Time.deltaTime;
	}
		
	void OnCollisionStay2D(Collision2D col)
	{
		if (col.collider.CompareTag ("shield")) {
			if (gameObject.activeInHierarchy) {
				Die ();
			}
		}
		if (col.collider.CompareTag("Player"))
		{
			print ("Hurt");
			Player player = col.transform.root.GetComponentInChildren<Player>();
			if (player.gameObject.activeInHierarchy) {
				player.Die ();
			}
		}

	}

	IEnumerator PlayAnimation() {
		int currentFrameIndex = 0;
		while (true) {
			spriteRenderer.sprite = frames [currentFrameIndex];
			// yield return new WaitForSeconds(1f / framesPerSecond);
			yield return new WaitForSeconds(seconds);
			currentFrameIndex++;
			currentFrameIndex = currentFrameIndex%frames.Length;
		}

	}

	IEnumerator explosionEffect() {
		gameObject.SetActive(false);
		Instantiate(deathEffect, transform.position, transform.rotation);
		yield return new WaitForSeconds(1);
	}



	public override void Die()
	{
		StartCoroutine (explosionEffect ());
		AudioSource.PlayClipAtPoint (deathSound, transform.position);
		Invoke ("Remove" , 1);

	}
	public void Remove (){
		Destroy (gameObject);
	}

}
