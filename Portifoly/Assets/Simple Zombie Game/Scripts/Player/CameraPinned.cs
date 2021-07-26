using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPinned : MonoBehaviour {

	[Tooltip ("Where the camera will be pinned")]
	public Transform player;

	// The position difference between the player and the camera 
	private Vector3 difference;

	// Firms camera behind player
	private void Start () {

		difference = player.position - transform.position;
	}

	// Makes camera follow player wherever it goes
	private void LateUpdate () {

		transform.position = player.position - difference;
	}
}