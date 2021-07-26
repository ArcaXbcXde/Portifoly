using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

	private PlayerControl playerControl;

	private float myPosition;

	private void Awake () {
		
		myPosition = transform.position.x + 0.16f;
	}

	private void OnTriggerEnter2D (Collider2D col) {
		
		if (col.tag == "Player"){

			if (playerControl == null) {

				playerControl = col.GetComponent<PlayerControl>();
			}

			playerControl.ladderPosition = myPosition;
		}
	}

	private void OnTriggerExit2D (Collider2D col) {

		if (col.tag == "Player") {


			playerControl.ladderPosition = 0;
		}
	}
}
