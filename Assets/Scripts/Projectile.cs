using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Projectile handles the movement of the fired lasers. The script needs any 2D Collider (set to trigger) and a Rigidbody2D (set to kinematic) on the same GameObject.
/// The script moves the GameObject constantly upwards using speed. After the lifeTime the projectile is destroyed.
/// </summary>
public class Projectile : MonoBehaviour {

    [Tooltip("How fast is the projectile moving upwards")]
    public float speed = 13;
    [Tooltip("After how many seconds is the projectile destroyed")]
    public float lifeTime = 0.5f;
    [Tooltip("The direction the projectile travels")]
    public Vector2 direction = new Vector2(0, 1);
    [Tooltip("Sound played when projectile is shot.")]
    public AudioClip shootSound;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(KillAfterSeconds(lifeTime));
        // normalize direction so it does not impact the travel speed
        direction.Normalize();
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }

    // Update is called once per frame. This moves the object upwards at speed.
    void Update () {
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
    }

    // Will destroy an object if it is an enemy.
    void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.CompareTag("enemy"))
		{
			if (collision.gameObject.activeInHierarchy) {
				collision.GetComponent<Killable> ().Die (); // causing problem
				Destroy (gameObject);
			}

		}
    }

    /// <summary>
    /// Destroys the projectile after seconds. This is a coroutine that needs be started using StartCoroutine().
    /// </summary>
    IEnumerator KillAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }




}
