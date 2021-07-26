using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCollectableKey : FloatingCollectables {

	public GameObject arrow;

	public Mechanicals[] unlockables;
	
	private void OnTriggerEnter2D (Collider2D col) {
		
		if (col.gameObject.tag == "Player") {

			for (int i = 0; i < unlockables.Length; i++) {

				unlockables[i].isLocked = false;
			}
			

			Destroy(arrow); // must be the last command
		}
	}

}