using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(PlatformerController2D))]
public class Shield : MonoBehaviour {

	[Tooltip ("After how many seconds is the Shield destroyed")]
	public float lifeTime = 5;
    public AudioClip shieldSound;


	// Use this for initialization
	void Start () {
		StartCoroutine (KillAfterSeconds (lifeTime));
        AudioSource.PlayClipAtPoint(shieldSound, transform.position);
    }
	
	// Update is called once per frame
	void Update () {
		if (Player.instance != null) {
			transform.position = Player.instance.transform.position;

		}
			
	}


	IEnumerator KillAfterSeconds (float seconds)
	{
		yield return new WaitForSeconds (seconds);
		Destroy (gameObject);

	}
}
