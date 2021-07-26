using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour {
	
	// The cursor is locked?
	private bool mouseLocked = false;

	// Used for easy acess to the manager
	private GameManager manager;

	/// <summary>
	/// A static instance of this script as object to be called when needed
	/// </summary>
	public static MouseLock Instance {

		get;
		private set;
	}

	private void Awake () {

		InitialSet ();
	}

	// Call initialization methods
	private void Start () {
		
		manager = GetComponent<GameManager>();
	}

	// Basic update
	private void Update () {

		ToggleCursorLock();
	}

	// Sets this as its own instance
	private void InitialSet () {

		if (Instance == null) {

			Instance = this;
		}
	}

	// Verifies if the cursor lock changed, then apply changes
	private void ToggleCursorLock () {
		
		if (Input.GetKeyDown(manager.keys.mouseToggle)) {
			
			if (mouseLocked == true) {

				UnlockMouse ();
			} else {

				LockMouse ();
			}
		}
	}

	/// <summary>
	/// Locks the cursor and makes it invisible
	/// </summary>
	public void LockMouse () {
		
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		if (mouseLocked == false) {
			mouseLocked = true;
		}
	}

	/// <summary>
	/// Unlocks the cursor and makes it visible
	/// </summary>
	public void UnlockMouse () {
		
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		if (mouseLocked == true) {
			mouseLocked = false;
		}
	}
}