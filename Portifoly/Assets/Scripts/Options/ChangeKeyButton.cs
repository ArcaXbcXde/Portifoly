using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Keys {

	public KeyCode mouseToggle;
	public KeyCode pause;
	public KeyCode action1;
	public KeyCode action2;
	public KeyCode action3;
	public KeyCode run;
	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;
	public KeyCode nil;
}

public class ChangeKeyButton : MonoBehaviour {

	/// <summary>
	/// Keys for every non-axis related action of the player
	/// </summary>
	public enum KeyAction {

		MouseToggle,
		Pause,
		Run,
		Action1,
		Action2,
		Action3,
		Up,
		Down,
		Left,
		Right,
		Nil,
	}

	#region variables

	[Tooltip("What key this button changes?")]
	public KeyAction thisButtonKey;
	
	// If the button is waiting any keypress
	private bool waitingKey = false;

	// The child text of the button
	private Text buttonText;

	// A direct reference to the game manager's instance for easy acess
	private GameManager manager;
	
	// The temporary value of the key to substitute the old key
	private KeyCode keyPressed;

	#endregion

	// When enabled adds listeners to the button to call a method that automatically makes it work without further setup
	private void OnEnable () {

		if (thisButtonKey != KeyAction.Nil) {

			GetComponent<Button>().onClick.AddListener(delegate { Clicked(); });
		} else {

			GetComponent<Button>().onClick.AddListener(delegate { BackToDefault(); });
		}
	}

	// When disabled removes listeners from the memory.
	private void OnDisable () {
		
		GetComponent<Button>().onClick.RemoveAllListeners();
	}

	// Basic start setting up the button
	private void Start () {
		
		manager = GameManager.Instance;
		
		buttonText = transform.GetChild(0).GetComponent<Text>();

		// Switch to initialize the text of this button depending on what key this button changes
		switch (thisButtonKey) {
			case KeyAction.MouseToggle:

				buttonText.text = manager.defaultKeys.mouseToggle.ToString();
				keyPressed = manager.keys.mouseToggle;
				break;
			case KeyAction.Pause:

				buttonText.text = manager.keys.pause.ToString();
				keyPressed = manager.keys.pause;
				break;
			case KeyAction.Run:

				buttonText.text = manager.keys.run.ToString();
				keyPressed = manager.keys.run;
				break;
			case KeyAction.Action1:

				buttonText.text = manager.keys.action1.ToString();
				keyPressed = manager.keys.action1;
				break;
			case KeyAction.Action2:

				buttonText.text = manager.keys.action2.ToString();
				keyPressed = manager.keys.action2;
				break;
			case KeyAction.Action3:

				buttonText.text = manager.keys.action3.ToString();
				keyPressed = manager.keys.action3;
				break;
			case KeyAction.Up:

				buttonText.text = manager.keys.up.ToString();
				keyPressed = manager.keys.up;
				break;
			case KeyAction.Down:

				buttonText.text = manager.keys.down.ToString();
				keyPressed = manager.keys.down;
				break;
			case KeyAction.Left:

				buttonText.text = manager.keys.left.ToString();
				keyPressed = manager.keys.left;
				break;
			case KeyAction.Right:

				buttonText.text = manager.keys.right.ToString();
				keyPressed = manager.keys.right;
				break;
		}

	}

	// Update to hear what is being pressed
	private void Update () {

		HearKey();
	}

	// A method that hears what key is pressed after this button click, then changes the button key to the new key pressed
	private void HearKey () {

		if ((waitingKey == true) && Input.anyKeyDown) {

			keyPressed = WhatButtonWasPressed();

			// Switch to use the key pressed depending on what key this button changes
			switch (thisButtonKey) {
				case KeyAction.MouseToggle:

					manager.keys.mouseToggle = keyPressed;
					break;
				case KeyAction.Pause:

					manager.keys.pause = keyPressed;
					break;
				case KeyAction.Run:

					manager.keys.run = keyPressed;
					break;
				case KeyAction.Action1:

					manager.keys.action1 = keyPressed;
					break;
				case KeyAction.Action2:

					manager.keys.action2 = keyPressed;
					break;
				case KeyAction.Action3:

					manager.keys.action3 = keyPressed;
					break;
				case KeyAction.Up:

					manager.keys.up = keyPressed;
					break;
				case KeyAction.Down:

					manager.keys.down = keyPressed;
					break;
				case KeyAction.Left:

					manager.keys.left = keyPressed;
					break;
				case KeyAction.Right:

					manager.keys.right = keyPressed;
					break;
				default:

					Debug.Log("nil is being used");
					break;
			}

			buttonText.text = keyPressed.ToString();

			waitingKey = false;
		}
	}

	// Goes through every possible key in the KeyCode enum to identify what was pressed
	private KeyCode WhatButtonWasPressed () {

		foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))) {
			if (Input.GetKeyDown(key)) {

				return key;
			}
		}
		return keyPressed;
	}

	/// <summary>
	/// Button should call this method when clicked to start hearing what key is pressed next to change it
	/// </summary>
	public void Clicked () {

		buttonText.text = "Press key";
		waitingKey = true;
	}

	/// <summary>
	/// Return this button back to its default
	/// </summary>
	public void BackToDefault () {

		// Switch to make the button go back to the default depending on what key this button changes
		switch (thisButtonKey) {
			case KeyAction.MouseToggle:

				manager.keys.mouseToggle = manager.defaultKeys.mouseToggle;
				buttonText.text = manager.keys.mouseToggle.ToString();
				break;
			case KeyAction.Pause:

				manager.keys.pause = manager.defaultKeys.pause;
				buttonText.text = manager.defaultKeys.pause.ToString();
				break;
			case KeyAction.Run:

				manager.keys.run = manager.defaultKeys.run;
				buttonText.text = manager.defaultKeys.run.ToString();
				break;
			case KeyAction.Action1:

				manager.keys.action1 = manager.defaultKeys.action1;
				buttonText.text = manager.defaultKeys.action1.ToString();
				break;
			case KeyAction.Action2:

				manager.keys.action2 = manager.defaultKeys.action2;
				buttonText.text = manager.defaultKeys.action2.ToString();
				break;
			case KeyAction.Action3:

				manager.keys.action3 = manager.defaultKeys.action3;
				buttonText.text = manager.defaultKeys.action3.ToString();
				break;
			case KeyAction.Up:

				manager.keys.up = manager.defaultKeys.up;
				buttonText.text = manager.defaultKeys.up.ToString();
				break;
			case KeyAction.Down:

				manager.keys.down = manager.defaultKeys.down;
				buttonText.text = manager.defaultKeys.down.ToString();
				break;
			case KeyAction.Left:

				manager.keys.left = manager.defaultKeys.left;
				buttonText.text = manager.defaultKeys.left.ToString();
				break;
			case KeyAction.Right:

				manager.keys.right = manager.defaultKeys.right;
				buttonText.text = manager.defaultKeys.right.ToString();
				break;
			default:

				Debug.Log("nil is being used");
				break;
		}
	}
}