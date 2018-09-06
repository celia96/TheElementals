using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for general purpose 2D controls for any object that can move and jump when grounded.
/// </summary>
[RequireComponent (typeof(Rigidbody2D))]
public class PlatformerController2D : MonoBehaviour
{
	[Header ("Controls")]
	[HideInInspector] public Vector2 input;	// horizontal movement
	[HideInInspector] public bool inputJump;	// jumping (whether space is pressed or not)
	[HideInInspector] public bool inputItem;	// Use Item (whether X is pressed or not)
    [HideInInspector] public bool inputFire;    // Shoot (whether C is pressed or not)

    [Header ("Grounding")]
	[Tooltip ("Offset of the grounding raycasts (red lines)")]
	[SerializeField] Vector2 groundCheckOffset = new Vector2 (0, -0.6f); // set the location of the raycast // -1.3
	[Tooltip ("Width of the grounding raycasts.")]
	[SerializeField] float groundCheckWidth = 0.7f; // distances between each raycast
	[Tooltip ("Distance of the grounding raycasts.")]
	[SerializeField] float groundCheckDepth = 0.2f; // how long the raycast is
	[Tooltip ("Number of the grounding Raycsts. Will be evenly spread over the width")]
	[SerializeField] int groundCheckRayCount = 3;	// the number of raycast
	[Tooltip ("Layers to be considered ground.")]
	[SerializeField] LayerMask groundLayers = 0;

    [Header ("Shooting")]
    [Tooltip("How fast is the player shooting")]
    public float rateOfFire = 2;
    [Tooltip("Prefab to be instantiated when shooting (Projectile)")]
    public GameObject projectilePrefab;


    public GameObject shieldPrefab;
    public AudioClip jumpSound;
    public static PlatformerController2D instance;
	private float speed = 5f; 	// horizontal movement speed
	private bool grounded = false; 	// on ground or not
	private float gravity = 5f;
	private Rigidbody2D rb;
	private float jumpForce = 10f;
    private float lastTimeFired = 0;
    // private float lastGroundingTime = 0;

    private Animator playerAnim;
	SpriteRenderer spriteRenderer;

    void Start () {
		instance = this;
		inputItem = false;
        inputFire = false;
		rb = GetComponent <Rigidbody2D> ();
        playerAnim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	/// <summary>
	/// Controls the basic update of the controller. This uses fixed update, since the movement is physics driven and has to be synched with the physics step.
	/// </summary>
	void FixedUpdate () {
		UpdateGrounding ();

		Vector2 velocity = rb.velocity;
        velocity.x = input.x * speed;

		// horizontal right
        if(velocity.x > 0) {
            velocity = new Vector2(speed, velocity.y);
			spriteRenderer.flipX = false;
        }
        if (velocity.x < 0) {
            velocity = new Vector2(-speed, velocity.y);
			spriteRenderer.flipX = true;
        } else { // Reduce sliding movement
            rb.velocity = new Vector2(0, velocity.y);
        }
			
		if (inputJump && grounded) {
			velocity = ApplyJump (velocity);
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);

        }

		velocity.y += -gravity * Time.deltaTime;
		rb.velocity = velocity;



	}

	void Update () {
		if (CoinPanel.instance.canUseItem() && inputItem) {
			UseItem ();
		}

        // if the fire button is pressed and we waited long enough since the last shot was fired, FIRE!
        if (inputFire && (lastTimeFired + 1 / rateOfFire) < Time.time)
        {
            lastTimeFired = Time.time;
            Fire();
        }

        playerAnim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        playerAnim.SetBool("Grounded", grounded);

    }

	Vector2 ApplyJump (Vector2 velocity) {
		velocity.y = jumpForce; // amount of jump
		grounded = false;
		return velocity;
	}

	void UseItem () {
		Player player = GetComponent<Player> ();
		CoinPanel.instance.removeCoins ();
		Instantiate(shieldPrefab, player.transform.position, Quaternion.identity);

	}

    /// <summary>
    /// Helper function to include the shooting behavior.
    /// </summary>
    void Fire()
    {
        // Shooting up
        Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);
    }

	/// <summary>
	/// Updates bool grounded 
	/// </summary>
	void UpdateGrounding ()
	{
		
		Vector2 groudCheckCenter = new Vector2 (transform.position.x + groundCheckOffset.x, transform.position.y + groundCheckOffset.y);
		Vector2 groundCheckStart = groudCheckCenter + Vector2.left * groundCheckWidth * 0.5f;
		if (groundCheckRayCount > 1) {
			for (int i = 0; i < groundCheckRayCount; i++) {
				
				RaycastHit2D hit = Physics2D.Raycast (groundCheckStart, Vector2.down, groundCheckDepth, groundLayers);
				// print ("update grounding");
				if (hit.collider != null) {
					// print ("update grounding");
					grounded = true;
					return;
				}

				groundCheckStart += Vector2.right * (1.0f / (groundCheckRayCount - 1.0f)) * groundCheckWidth;
			}
		}
		grounded = false;

	}


	/// <summary>
	/// Used to draw the red lines for the grounding raycast. Only active in the editor and when the instance is selected.
	/// </summary>
	void OnDrawGizmosSelected(){
		Vector2 groudCheckCenter = new Vector2 (transform.position.x + groundCheckOffset.x, transform.position.y + groundCheckOffset.y);
		Vector2 groundCheckStart = groudCheckCenter + Vector2.left * groundCheckWidth * 0.5f;
		if (groundCheckRayCount > 1) {
			for (int i = 0; i < groundCheckRayCount; i++) {
				Debug.DrawLine (groundCheckStart, groundCheckStart + Vector2.down * groundCheckDepth, Color.red);
				groundCheckStart += Vector2.right * (1.0f / (groundCheckRayCount - 1.0f)) * groundCheckWidth;
			}
		}
	}


}