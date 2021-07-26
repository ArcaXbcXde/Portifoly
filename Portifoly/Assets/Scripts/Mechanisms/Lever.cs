using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Mechanicals {
	
	// Activates every mechanism in the "objectsToActivate" vector
	protected override void OnActivate () {

		for (int i = 0; i < objectsToActivate.Length; i++) {

			objectsToActivate[i].Activate();
		}
	}

	// Deactivates every mechanism in the "objectsToActivate" vector
	protected override void OnDeactivate () {

		for (int i = 0; i < objectsToActivate.Length; i++) {

			objectsToActivate[i].Deactivate();
		}
	}

	// When the player touches the lever's trigger, rotates it and triggers the activation properly
	private void OnTriggerEnter (Collider col) {

		if (col.tag == "Player") {

			transform.Rotate(Vector3.up, 180, Space.World);

			if (isActive == false) {

				Activate();
			} else if (isActive == true) {

				Deactivate();
			}
		}
	}
}