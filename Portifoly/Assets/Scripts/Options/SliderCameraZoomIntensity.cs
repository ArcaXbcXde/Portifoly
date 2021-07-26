using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderCameraZoomIntensity : OptionsSliders {

	private void Start () {

		thisSlider.value = GameManager.Instance.cameraZoomIntensity;
	}

	protected override void ValueChangeCheck () {

		GameManager.Instance.cameraZoomIntensity = thisSlider.value;
	}

	public void BackToDefault () {

		thisSlider.value = GameManager.Instance.defaultCameraZoomIntensity;
	}
}
