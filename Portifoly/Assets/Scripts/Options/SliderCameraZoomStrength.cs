using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderCameraZoomStrength : OptionsSliders {

	private void Start () {

		thisSlider.value = GameManager.Instance.cameraZoomStrength;
	}

	protected override void ValueChangeCheck () {

		GameManager.Instance.cameraZoomStrength = thisSlider.value;
	}

	public void BackToDefault () {

		thisSlider.value = GameManager.Instance.defaultCameraZoomStrength;
	}
}
