using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZoomPossible : OptionsToggleables {
	
	private void Start () {

		thisToggle.isOn = GameManager.Instance.cameraHasZoom;
	}

	protected override void ValueChangeCheck () {

		GameManager.Instance.cameraHasZoom = thisToggle.isOn;
	}

	public void BackToDefault () {

		thisToggle.isOn = GameManager.Instance.defaultCameraHasZoom;
	}
}