using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraUpDownDirection : OptionsDropdowns {

	private void Start () {

		thisDropdown.value = GameManager.Instance.cameraUpDownDirection;
	}

	protected override void ValueChangeCheck () {

		GameManager.Instance.cameraUpDownDirection = thisDropdown.value;
	}

	public void BackToDefault () {

		thisDropdown.value = GameManager.Instance.defaultCameraUpDownDirection;
	}
}