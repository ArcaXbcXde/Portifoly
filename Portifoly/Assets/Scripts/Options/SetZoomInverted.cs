using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZoomInverted : OptionsToggleables {

	private void Start () {

		thisToggle.isOn = GameManager.Instance.invertCameraZoom;
	}

	protected override void ValueChangeCheck () {

		GameManager.Instance.invertCameraZoom = thisToggle.isOn;
	}

	public void BackToDefault () {

		thisToggle.isOn = GameManager.Instance.defaultInvertCameraZoom;
	}
}
