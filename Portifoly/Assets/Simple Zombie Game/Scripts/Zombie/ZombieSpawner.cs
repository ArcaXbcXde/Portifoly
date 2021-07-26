using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {
	
	[Tooltip ("If lower than this value then continue spawning zombies, 0 means it will never spawn")]
	[Range (0, 100)]
	public int maxZombies = 10;

	// Amount of zombies active in the scene at the moment
	[HideInInspector]
	public int zombieAmount;

	[Tooltip ("Default height the zombie will be spawned")]
	public float spawnHeight = 3.83f;

	[Tooltip ("Time between zombie spawns")]
	[Min (0.1f)]
	public float spawnDelay = 10.0f;

	[Tooltip ("Minimum spawning distance from the player")]
	[Range (3.0f, 100.0f)]
	public float minDistance = 15.0f;

	[Tooltip ("Maximum spawning distance from the player")]
	[Range (3.0f, 100.0f)]
	public float maxDistance = 50.0f;

	[Tooltip ("The zombie prefab being used")]
	public GameObject zombiePrefab;

	[Tooltip ("The player object")]
	public Transform player;

	// Time left untill another zombie spawns
	private float spawnCD;


	private void Awake () {
		
		StartCoroutine(Countdowns());
	}

	private void Start() {

		ZombieControl[] zombiesPresent = GetComponentsInChildren<ZombieControl>();

		for (int i = 0; i < zombiesPresent.Length; i++) {

			Multipooling.AddOnPool (zombiesPresent[i].GetComponent<GameObject>());

			zombieAmount++;
		}


	}

	private void Update() {
		
		if (spawnCD <= 0 && zombieAmount < maxZombies) {

			spawnCD = spawnDelay;
			
			float leftOrRight = Random.Range (-1.0f, 1.0f);

			Vector3 spawnPosition;

			if (leftOrRight > 0.0f) {

				spawnPosition = new Vector3 (Random.Range(player.position.x + minDistance, player.position.x + maxDistance), spawnHeight);
			} else {

				spawnPosition = new Vector3 (Random.Range(player.position.x - minDistance, player.position.x - maxDistance), spawnHeight);
			}
			
			ZombieControl pooledZombie = Multipooling.MultiPool(zombiePrefab, spawnPosition, Quaternion.identity).GetComponent<ZombieControl>();

			pooledZombie.GetComponent<Transform>().SetParent(transform);
			
			pooledZombie.GetComponent<Rigidbody2D>().gravityScale = 1;

			pooledZombie.GetComponent<Collider2D>().enabled = true;

			zombieAmount++;

		}

    }
	
	private IEnumerator Countdowns () {

		while (true) {

			yield return new WaitForSecondsRealtime(0.1f);

			if (spawnCD > 0) {

				spawnCD -= 0.1f;
			} else if (spawnCD != 0) {

				spawnCD = 0;
			}
		}
	}
}