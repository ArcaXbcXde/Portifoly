using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate2D : Mechanicals {

	// How many objects are triggering this pressure plate
	private int objectsTriggering;

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

	// If the player or any object marked as a prop collides with this trigger, triggers it if it had no objects colliding with it and add one to "objectsTriggering" variable
	private void OnTriggerEnter2D (Collider2D col) {

		if ((col.tag == "Player") || (col.tag == "Prop")) {

			if (objectsTriggering == 0) {

				Activate();
			}
			objectsTriggering++;
		}
	}

	// If the player or any object marked as a prop stops colliding with this trigger, substract one to "objectsTriggering" variable and if there is none deactivates it
	private void OnTriggerExit2D (Collider2D col) {

		if (col.tag == "Player" || col.tag == "Prop") {

			objectsTriggering--;
			if (objectsTriggering == 0) {

				Deactivate();
			}
		}
	}
}