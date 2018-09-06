using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Killable {

	[Tooltip ("Layers to be considered obstacles for obstacle checks and collision checks when checking for change of direction.")]
	[SerializeField] LayerMask obstacleLayers = 0;
	[Tooltip("The individual sprites of the animation")]
	public Sprite[] frames;
	[Tooltip("How fast does the animation play")]
	public float seconds;
	public float speed;
	public Collider2D leftCheck ; // 
	public Collider2D rightCheck;
	public Collider2D body;
    public GameObject deathEffect;

	SpriteRenderer spriteRenderer;
	public int dir = 1;

	public AudioClip deathSound;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = true;
		StartCoroutine(PlayAnimation());

	}

	// Update is called once per frame
	void Update () {

		// Enemy Type: Birds
		if (leftCheck != null && rightCheck != null) {
			if (leftCheck.IsTouchingLayers (obstacleLayers)) {
				dir = 1;
				spriteRenderer.flipX = false;
			}

			if (rightCheck.IsTouchingLayers (obstacleLayers)) {
				dir = -1;
				spriteRenderer.flipX = true;
			}

		}
		transform.position += Vector3.right * dir * speed * Time.deltaTime;
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
			Player player = col.transform.root.GetComponentInChildren<Player>();
			if (player.gameObject.activeInHierarchy) {
				player.Die ();
			}
		}

	}


	public override void Die()
	{
		StartCoroutine(explosionEffect());
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Invoke ("Remove" , 1);
	}
	public void Remove (){
		Destroy (gameObject);
	}


	IEnumerator PlayAnimation() {
		int currentFrameIndex = 0;
		while (true) {
			spriteRenderer.sprite = frames [currentFrameIndex];
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


}
