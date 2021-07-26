using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

	[System.Serializable]
	public struct Weapon {

		[Tooltip("Amount of damage in a hit")]
		[Range(0.0f, 10.0f)]
		public int damage;

		[Tooltip("Range of the weapon")]
		[Range(0.0f, 50.0f)]
		public float range;

		[Tooltip("Time between weapon attacks")]
		[Range(0.0f, 10.0f)]
		public float delay;

		[Tooltip("Type of damage the weapon deals")]
		public TypeOfDamage damageType;

		[Tooltip("Element of damage of the weapon")]
		public ElementOfDamage damageElement;
	}

public class PlayerControl : EntityControl {

	protected enum PlayerState {

		Idling,
		Crouching,
		Walking,
		Running,
		Climbing,
		Hitting,
		Dying,
		None,
	}

	protected enum PlayerAction {

		Idle,
		Crouch,
		Walk,
		Run,
		Climb,
		Hit,
		Die,
		None,
	}

	[System.Serializable]
	public struct Precision {

		[Tooltip("Weapon precision while idling")]
		[Range(0.0f, 5.0f)]
		public float idling;

		[Tooltip("Weapon precision while walking")]
		[Range(0.0f, 5.0f)]
		public float walking;

		[Tooltip("Weapon precision while crouching")]
		[Range(0.0f, 5.0f)]
		public float crouching;

	}

	[System.Serializable]
	public struct Hearable {

		public float crouch;

		public float idle;

		public float walk;

		public float run;

		public float climb;

		public float shoot;

	}

	public delegate void Activate ();

	#region public variables

	public Activate interact;

	[Tooltip("Duration in seconds of the melee attack")]
	[Range(0.0f, 2.0f)]
	public float meleeDuration = 0.5f;

	[Tooltip("Distance the melee attack pushes the zombie")]
	[Range(0.0f, 10.0f)]
	public float meleePushDistance = 0.5f;

	[Tooltip("Duration of the shooting line")]
	[Range(0.0f, 1.0f)]
	public float lineDuration;

	[Tooltip("The line prefab used when weapon is used")]
	public GameObject lineUsed;
	
	[Tooltip("Properties of the shooting rifle")]
	public Weapon rifle;

	[Tooltip("Properties of the melee attack")]
	public Weapon melee;

	[Tooltip("Soldier' weapon precision while doing certain actions")]
	public Precision precision;

	[Tooltip("Sounds audible by the zombie and its range")]
	public Hearable zombieHearDistance;

	// The X of the ladder being climbed, if no ladder is found it is equal 0
	[HideInInspector]
	public float ladderPosition;

	#endregion

	#region private variables

	// A flag to tell if the player is shooting or not
	private bool shooting;

	// A flag to tell if the player is meleeing or not
	private bool meleeing;

	// Weapon precision inside the Y axis, varies depending on the player's actual state
	private float actualPrecision = 0.0f;

	// Time left untill player is able to shoot again
	private float shootCD;

	// Starting gravity scale for when it get changed at any moment (for example climb ladders), be able to go back to the original
	private float startingGravityScale;

	private Vector2 moveAmount;

	// Action state in which the player actually is
	private PlayerState actualState = PlayerState.Idling;

	// Action that if changed will lead to a different player's state
	private PlayerAction actualAction = PlayerAction.Idle;

	// Layers to be ignored, first set this as what layer to be ignored
	private LayerMask rayLayer = (1 << 2) + (1 << 8);

	// Reference to the method that controls damage properties for the rifle bullet
	private Damage rifleDamage;

	// Reference to the method that controls damage properties for the melee attack
	private Damage meleeDamage;

	// Entity's health manager
	private HealthControl myHealth;

	// The manager, here to easies calls
	private GameManager manager;

	// Radius of the sound made by the player
	private CircleCollider2D soundRadius;

	// Player's rigidbody
	private Rigidbody2D rigid;

	// Player's renderer
	private SpriteRenderer render;

	// Player's animator
	private Animator anima;

	// All shooting trajectories the player has, 4 because its one for each side for each state (standing up looking left and right and crouching looking left and right)
	private Transform[] shootingLines = new Transform[4];

	#endregion

	#region basic methods

	// Basic awake, mainly to initialize variables an set up the entity
	private void Awake () {

		// Then invert to get all layers except what was marked, so the ray can hit anything except what is marked
		rayLayer = ~rayLayer;

		rifleDamage = new Damage(rifle.damage, rifle.damageType, rifle.damageElement);
		meleeDamage = new Damage(melee.damage, melee.damageType, melee.damageElement);

		myHealth = GetComponent<HealthControl>();

		rigid = GetComponent<Rigidbody2D>();
		startingGravityScale = rigid.gravityScale;

		render = GetComponent<SpriteRenderer>();

		anima = GetComponent<Animator>();

		soundRadius = GetComponent<CircleCollider2D>();

		actualPrecision = precision.idling;
	}

	// Basic start, mainly to align the entity with the scene
	private void Start () {

		manager = GameManager.Instance;

		soundRadius = GetComponentInChildren<CircleCollider2D>();

		shootingLines = GetComponentsInChildren<Transform>(false);

		actualState = PlayerState.Idling;
		
		StartCoroutine(Countdowns());
	}

	// Fixed update, called a fixed amount of times each second
	private void FixedUpdate () {

		rigid.velocity = moveAmount;
	}

	// Basic update, called every frame
	private void Update () {

		if (myHealth.Dead == true) {

			actualAction = PlayerAction.Die;
		}

		switch (actualAction) {
			case (PlayerAction.Idle):

				if (actualState != PlayerState.Idling) {

					rigid.velocity = new Vector2(0, rigid.velocity.y);

					actualPrecision = precision.idling;
					actualState = PlayerState.Idling;

					PlayerSoundControl();

				}

				PlayerIdleControl();

				break;
			case (PlayerAction.Walk):

				if (actualState != PlayerState.Walking) {

					actualPrecision = precision.walking;
					actualState = PlayerState.Walking;

					PlayerSoundControl();

				}

				PlayerWalkControl();

				break;
			case (PlayerAction.Run):

				if (actualState != PlayerState.Running) {

					actualPrecision = precision.walking;
					actualState = PlayerState.Running;

					PlayerSoundControl();
				}

				PlayerRunControl();

				break;
			case (PlayerAction.Crouch):

				if (actualState != PlayerState.Crouching) {

					actualPrecision = precision.crouching;
					actualState = PlayerState.Crouching;

					PlayerSoundControl();

				}

				PlayerCrouchControl();

				break;
			case (PlayerAction.Climb):

				if (actualState != PlayerState.Climbing) {

					actualState = PlayerState.Climbing;

					PlayerSoundControl();

				}

				PlayerClimbControl();

				break;
			case (PlayerAction.Die):

				if (actualState != PlayerState.Dying) {

					actualState = PlayerState.Dying;
					PlayerDeathControl();

					PlayerSoundControl();
				}

				break;
		}
	}

	#endregion

	// Updates all cooldowns every 0.1 seconds
	private IEnumerator Countdowns () {

		while (true) {

			yield return new WaitForSecondsRealtime(0.1f);

			if (shootCD > 0) {

				shootCD -= 0.1f;
			} else if (shootCD != 0) {

				shootCD = 0;
			}
		}
	}

	#region player control methods

	// During idle the player does nothing, but can start walk, attack, crouch or climb
	private void PlayerIdleControl () {

		moveAmount = new Vector2(0, rigid.velocity.y);

		if (meleeing == false) {

			if (Input.GetKey(manager.keys.right) || Input.GetKey(manager.keys.left)) {

				anima.SetBool("Walking", true);
				actualAction = PlayerAction.Walk;

			}
			CheckAttacks();

			CheckClimbNCrouch();
		}

		CheckInteracting();
	}

	// During walk the player moves at "walkSpeed" and can start run, idle, attack, crouch or climb
	private void PlayerWalkControl () {

		if (Input.GetKey(manager.keys.right)) {

			moveAmount = new Vector2(walkSpeed, rigid.velocity.y);
			if (render.flipX == true) {

				render.flipX = false;
			}
		} else if (Input.GetKey(manager.keys.left)) {

			moveAmount = new Vector2(-walkSpeed, rigid.velocity.y);
			if (render.flipX == false) {

				render.flipX = true;
			}
		} else {

			moveAmount = Vector2.zero;

			anima.SetBool("Walking", false);
			actualAction = PlayerAction.Idle;
		}

		if (Input.GetKey(manager.keys.run) && shooting == false) {

			anima.SetBool("Running", true);
			actualAction = PlayerAction.Run;
		}

		CheckAttacks();

		CheckClimbNCrouch();

		CheckInteracting();
	}

	// During run the player moves at "runSpeed" and can start walk, idle, attack, crouch or climb
	private void PlayerRunControl () {

		if (Input.GetKey(manager.keys.run)) {

			if (Input.GetKey(manager.keys.right)) {

				moveAmount = new Vector2(runSpeed, rigid.velocity.y);
				if (render.flipX == true) {

					render.flipX = false;
				}
			} else if (Input.GetKey(manager.keys.left)) {

				moveAmount = new Vector2(-runSpeed, rigid.velocity.y);
				if (render.flipX == false) {

					render.flipX = true;
				}
			} else {

				moveAmount = Vector2.zero;

				anima.SetBool("Running", false);
				anima.SetBool("Walking", false);
				actualAction = PlayerAction.Idle;
			}
		} else {

			anima.SetBool("Running", false);
			actualAction = PlayerAction.Walk;

		}

		CheckAttacks();

		CheckClimbNCrouch();

		CheckInteracting();
	}

	// During crouch the player will be able to just look left or right, but can start shoot or idle
	private void PlayerCrouchControl () {

		moveAmount = new Vector2(0, rigid.velocity.y);

		if (Input.GetKeyDown(manager.keys.left)) {

			render.flipX = true;

		} else if (Input.GetKeyDown(manager.keys.right)) {

			render.flipX = false;
		}

		if (Input.GetKeyUp(manager.keys.down)) {

			anima.SetBool("Crouching", false);
			actualAction = PlayerAction.Idle;
		}

		CheckAttacks();

		CheckInteracting();
	}

	// During climb the player can just go up or down, and can start just walk
	private void PlayerClimbControl () {

		if (Input.GetKey(manager.keys.up)) {

			moveAmount = new Vector2(0.0f, walkSpeed);

			anima.speed = 1.0f;
		} else if (Input.GetKey(manager.keys.down)) {

			moveAmount = new Vector2(0.0f, -walkSpeed);

			anima.speed = 1.0f;
		} else if (Input.GetKey(manager.keys.right) || Input.GetKey(manager.keys.left) || ladderPosition == 0) {


			rigid.gravityScale = startingGravityScale;
			moveAmount = Vector2.zero;

			anima.speed = 1.0f;

			anima.SetBool("Climbing", false);
			anima.SetBool("Walking", true);
			actualAction = PlayerAction.Walk;
		} else {

			moveAmount = Vector2.zero;

			anima.speed = 0.0f;
		}


		CheckInteracting();
	}

	// During death the player can do nothing nor go to any other state, but the scene will restart after 5 seconds
	private void PlayerDeathControl () {

		soundRadius.radius = 0.0f;

		rigid.gravityScale = 0.0f;

		moveAmount = Vector2.zero;

		GetComponent<BoxCollider2D>().enabled = false;

		anima.SetTrigger("Death");

		ChangeScene.Instance.RestartScene(5.0f);
	}

	// Controls the radius of the sound that can trigger zombies to pursue the player
	private void PlayerSoundControl () {

		if (shooting == true) {

			soundRadius.radius = zombieHearDistance.shoot;
		} else {
			switch (actualAction) {

				case PlayerAction.Idle:

					soundRadius.radius = zombieHearDistance.idle;
					break;
				case PlayerAction.Crouch:

					soundRadius.radius = zombieHearDistance.crouch;
					break;
				case PlayerAction.Walk:

					soundRadius.radius = zombieHearDistance.walk;
					break;
				case PlayerAction.Run:

					soundRadius.radius = zombieHearDistance.run;
					break;
				case PlayerAction.Climb:

					soundRadius.radius = zombieHearDistance.climb;
					break;
			}
		}
	}

	#endregion

	#region check methods

	// Checks if the player wants to climb ladders or crouch, then calls the propper instructions
	private void CheckClimbNCrouch () {

		if ((Input.GetKey(manager.keys.up) || Input.GetKey(manager.keys.down)) && ladderPosition != 0) {

			rigid.gravityScale = 0.0f;
			rigid.velocity = Vector2.zero;
			moveAmount = Vector2.zero;

			transform.position = new Vector2(ladderPosition, transform.position.y);

			anima.SetBool ("Shooting", false);
			anima.SetBool ("Climbing", true);
			actualAction = PlayerAction.Climb;

		} else if (Input.GetKeyDown(manager.keys.down)) {

			rigid.velocity = new Vector2(0, rigid.velocity.y);

			anima.SetBool ("Running", false);
			anima.SetBool ("Walking", false);
			anima.SetBool ("Crouching", true);
			actualAction = PlayerAction.Crouch;
		}

	}

	// Checks if the player wants to attack and which attack, then calls the propper instructions
	private void CheckAttacks () {

		if (Input.GetKeyDown(manager.keys.action1)) {

			meleeing = true;
			Meleeing();

			Invoke("EndMelee", meleeDuration);

			anima.SetBool("Walking", false);
			actualAction = PlayerAction.Idle;

			anima.SetTrigger("Melee");
		} else if (Input.GetKey(manager.keys.action2) && meleeing == false) {

			if (shootCD <= 0) {

				shootCD = rifle.delay;
				Shooting();
			}

			if (shooting == false) {

				shooting = true;

				if (actualAction == PlayerAction.Run) {

					anima.SetBool("Running", false);
					actualAction = PlayerAction.Walk;
				}
				anima.SetBool("Shooting", true);
			}

		} else if (Input.GetKeyUp(manager.keys.action2)) {

			shooting = false;
			anima.SetBool("Shooting", false);

		}

		PlayerSoundControl();
	}

	// Checks if the player pressed the interact button, and call everything that is interactable
	private void CheckInteracting () {

		if (Input.GetKeyDown(manager.keys.action3)) {

			if (interact != null) {

				interact();
			}
		}
	}

	#endregion

	#region attack-related methods

	// Controls everything about shooting
	private void Shooting () {

		Vector3 rayStart = CalculateRayStart();

		Vector3 rayEnd = CalculateRayEnd(rifle.range);

		RaycastHit2D hit = Physics2D.Raycast(rayStart, rayEnd, rifle.range, rayLayer);

		// Push or creates a line from the pool
		LineRenderer pooledLine = Multipooling.MultiPool(lineUsed, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();

		pooledLine.SetPosition(0, rayStart);

		// if the "hit" raycast hits something that has health, makes that take damage from the rifle, and makes the line render propperly 
		if (hit.transform != null) {
			if (hit.transform.GetComponent<HealthControl>() != null) {

				hit.transform.GetComponent<HealthControl>().TakeDamage(rifleDamage);

				pooledLine.SetPosition(1, hit.point);
			} else {

				pooledLine.SetPosition(1, hit.point);
			}
		} else {

			pooledLine.SetPosition(1, rayStart + rayEnd);
		}

		// Then after being used, deactivates the line again
		StartCoroutine(DeactivateLine(pooledLine));
	}

	// Controls everything about melee attacks
	private void Meleeing () {

		Vector3 rayEnd = CalculateRayEnd(melee.range);

		RaycastHit2D hit = Physics2D.Raycast(transform.position, rayEnd, melee.range, rayLayer);

		// if the "hit" raycast hits something that has health, makes that take damage from the melee attack, and pushes back watever was hit based of where the player was facing
		if (hit.transform != null) {
			if (hit.transform.GetComponent<HealthControl>() != null) {

				hit.transform.GetComponent<HealthControl>().TakeDamage(meleeDamage);

				if (render.flipX == false) {
					
					hit.transform.position += Vector3.right * meleePushDistance;
				} else {

					hit.transform.position -= Vector3.right * meleePushDistance;
				}
			}
		}
	}
	
	// Defines where is the starting position of a raycast when called
	private Vector3 CalculateRayStart () {

		if ((actualState == PlayerState.Idling || actualState == PlayerState.Walking)) {

			if (render.flipX == false) {

				return shootingLines[1].position;
			} else {

				return shootingLines[2].position;
			}

		} else if ((actualState == PlayerState.Crouching)) {

			if (render.flipX == false) {

				return shootingLines[3].position;
			} else {

				return shootingLines[4].position;
			}
		} else {
			return shootingLines[0].position;
		}
	}

	// Defines where is the ending position of a raycast when called based on the "rayDistance" value
	private Vector3 CalculateRayEnd (float rayDistance) {

		if (render.flipX == false) {

			return (Vector3.right * rayDistance) + (Vector3.up * Random.Range(-actualPrecision, actualPrecision));
		} else {

			return (Vector3.right * -rayDistance) + (Vector3.up * Random.Range(-actualPrecision, actualPrecision));
		}
	}

	// Deactivates the shooting line after used, so it can be reused by the pooling
	private IEnumerator DeactivateLine (LineRenderer lineToDeactivate) {

		yield return new WaitForSecondsRealtime(lineDuration);

		lineToDeactivate.gameObject.SetActive(false);
	}

	// Called after "meleeDuration" when the player melee
	private void EndMelee () {
		meleeing = false;
	}

	#endregion
}