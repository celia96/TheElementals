using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Player script. Manages the health and interaction with enemies of the player.
/// </summary>
[RequireComponent (typeof(PlatformerController2D))]
public class Player : MonoBehaviour
{

	Renderer rend;
	public static Player instance;
    public GameObject deathEffect;
    public AudioClip deathSound;

	void Awake ()
	{
		instance = this;
		rend = GetComponent<Renderer>();
		rend.enabled = true;

	}

    IEnumerator explosionEffect() {
        instance.gameObject.SetActive(false);
        Instantiate(deathEffect, instance.transform.position, instance.transform.rotation);
        yield return new WaitForSeconds(1);
    }

	/// <summary>
	/// Destroy the player and spawn the death animation.
	/// </summary>
	public void Die ()
	{
		print (gameObject.activeSelf);
		StartCoroutine(explosionEffect());
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Invoke ("Remove" , 1);
        // restart level here
        GameManager.instance.RestartTheGameAfterSeconds(2);
    }

    /// <summary>
    /// Restart the level when the player exits the screen - results in a faster restart, since death animation doesn't play.
    /// </summary>
    public void ExitScreen()
    {
        Invoke("Remove", 0.5f);
        GameManager.instance.RestartTheGameAfterSeconds(1.5f);
    }

	/// <summary>
	/// Remove the player.
	/// </summary>
	public void Remove (){
		Destroy (gameObject);
	}




}
