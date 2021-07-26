using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour {

	[Tooltip("The cursor will start locked?")]
	public bool mouseStartLocked = true;
	
	public static SceneSetup Instance {

		get;
		private set;
	}

	private void Awake () {
		
		InitialSet();
	}

	private void Start () {

		if (mouseStartLocked == true) {

			MouseLock.Instance.LockMouse();
		}
	}

	private void InitialSet () {

		Instance = this;
	}
}