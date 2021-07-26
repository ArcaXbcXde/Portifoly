using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : EntityControl {

	[System.Serializable]
	public struct Weapon {

		[Tooltip("Amount of damage in a hit")]
		[Range(0.0f, 10.0f)]
		public int damage;

		[Tooltip("Reach of the zombie's melee attack")]
		[Range(0.0f, 2.0f)]
		public float range;

		[Tooltip("Type of damage the weapon deals")]
		public TypeOfDamage damageType;

		[Tooltip("Element of damage of the weapon")]
		public ElementOfDamage damageElement;

	}

	#region public variables

	[Tooltip ("If the zombie walks around")]
	public bool wanderer = true;

	[Tooltip ("Distance the zombie can see the player in front of him")]
	[Range (0.0f, 100.0f)]
	public float viewDistance = 10.0f;
	
	[Tooltip ("Distance for the zombie to be automatically despawned")]
	[Min (15.0f)]
	public float despawnDistance = 100.0f;

	public Weapon melee;

	#endregion

	// Most are public because are being used by the state machine behaviour, otherwise would best be private
	#region hidden public variables

	[HideInInspector]
	public float rayDistance;

	[HideInInspector]
	public Vector2 moveAmount;

	[HideInInspector]
	public Vector3 rayEnd;

	// Ray for zombie vision and reach
	public RaycastHit2D hit;

	// Layers to be ignored, first set this as what layer to be ignored
	[HideInInspector]
	public LayerMask rayLayer = (1 << 2) + (1 << 9);

	// Entity's health manager
	[HideInInspector]
	public HealthControl myHealth;

	// Entity's renderer
	[HideInInspector]
	public SpriteRenderer render;

	// Zombie target information
	[HideInInspector]
	public Transform target;
	
	// Entity's rigidbody
	[HideInInspector]
	public Rigidbody2D rigid;

	#endregion

	#region private variables

	// The damage method that controls damage properties
	private Damage meleeDamage;

	// Animator to change animation state
	private Animator anima;
	
	#endregion

	private void Awake () {

		// Then invert to get all layers except what was marked, so the ray can hit anything except what is marked
		rayLayer = ~rayLayer;

		meleeDamage = new Damage(melee.damage, melee.damageType, melee.damageElement);

		myHealth = GetComponent<HealthControl>();

		render = GetComponent<SpriteRenderer>();

		anima = GetComponent<Animator>();

		rigid = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate () {
		
		hit = Physics2D.Raycast (transform.position, (rayEnd - transform.position), rayDistance, rayLayer);
	}

	private void OnTriggerEnter2D (Collider2D col) {

		if (col.tag == "Sound") {
			
			target = col.GetComponentsInParent<Transform>()[1];
			
			anima.SetBool("Pursuing", true);
		}
	}

	public void CheckDamage () {

		RaycastHit2D meleeHit = Physics2D.Raycast(transform.position, target.position - transform.position, melee.range, rayLayer);
		
		if (meleeHit.rigidbody != null) {

			if (meleeHit.rigidbody.GetComponent<PlayerControl>() != null) {
				
				meleeHit.rigidbody.GetComponent<HealthControl>().TakeDamage(meleeDamage);
			}
		}
	}
}