using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedDoor : Mechanicals {

	private bool interactable;

	private PlayerControl player;

	private SpriteRenderer[] playerSprites;

	private SpriteRenderer imageInteractable;
	private SpriteRenderer imageNotInteractable;

	private Collider2D theActualCollider;

	private Animator anima;

	private void Awake () {
		 
		anima = GetComponent<Animator>();
		anima.SetBool("isActive", isActive);

		Collider2D[] colliders = gameObject.GetComponents<BoxCollider2D>();

		for (int i = 0; i < colliders.Length; i++) {

			if (colliders[i].isTrigger == false) {

				theActualCollider = colliders[i];
				break;
			}
		}
	}

	// Called if the player is interacting with the door to either open or close it.
	public void OnInteracted () {

		if (interactable == true) {

			if (isActive == false) {
				
				for (int i = 0; i < objectsToActivate.Length; i++) {
					
					OnActivate();
				}
			} else if (isActive == true && isLocked == false) {

				for (int i = 0; i < objectsToActivate.Length; i++) {
					
					OnDeactivate();
				}
			}
		}
	}

	// Called if the door is now "active" (closed)
	protected override void OnActivate () {

		isActive = true;
		anima.SetBool("isActive", isActive);
		theActualCollider.enabled = true;
	}

	// Called if the door is now "deactive" (open)
	protected override void OnDeactivate () {

		isActive = false;
		anima.SetBool("isActive", isActive);
		theActualCollider.enabled = false;
	}

	// Detects what entered its trigger, if its the player then act propperly.
	private void OnTriggerEnter2D (Collider2D col) {
		
		if (col.tag == "Player") {

			if (player == null) {

				player = col.GetComponent<PlayerControl>();
				
				playerSprites = col.transform.GetComponentsInChildren<SpriteRenderer>(true);
				imageInteractable = playerSprites[1];
				imageNotInteractable = playerSprites[2];

			}

			player.interact += OnInteracted;

			interactable = true;

			if (isLocked == true) {

				imageNotInteractable.gameObject.SetActive(true);
			} else if (isLocked == false) {

				imageInteractable.gameObject.SetActive(true);
			}
		}
	}

	// Detects what exitted its trigger, if its the player then act propperly.
	private void OnTriggerExit2D (Collider2D col) {

		if (col.tag == "Player") {

			player.interact -= OnInteracted;

			interactable = false;

			imageInteractable.gameObject.SetActive(false);
			imageNotInteractable.gameObject.SetActive(false);
		}
	}
}