
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningFire : MonoBehaviour
{
    public Sprite[] frames;
    public GameObject deathEffect;
    private SpriteRenderer spriteRenderer;


    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayAnimation(0.15f));


    }
		
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            Player player = col.transform.root.GetComponentInChildren<Player>();
            player.Die();
        }
        if (col.gameObject.tag == "shield")
        {
            Destroy(gameObject);
            StartCoroutine(explosionEffect());
        }

    }

    IEnumerator explosionEffect()
    {
        gameObject.SetActive(false);
        Instantiate(deathEffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(1);
    }

    IEnumerator PlayAnimation(float seconds)
    {
        int currentFrameIndex = 0;
        while (true)
        {
            spriteRenderer.sprite = frames[currentFrameIndex];
            // yield return new WaitForSeconds(1f / framesPerSecond);
            yield return new WaitForSeconds(seconds);
            currentFrameIndex++;
            currentFrameIndex = currentFrameIndex % frames.Length;
        }

    }

}