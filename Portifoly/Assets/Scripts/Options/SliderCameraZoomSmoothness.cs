using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderCameraZoomSmoothness : OptionsSliders {

	private void Start () {

		thisSlider.value = GameManager.Instance.cameraZoomSmoothness;
	}

	protected override void ValueChangeCheck () {

		GameManager.Instance.cameraZoomSmoothness = thisSlider.value;
	}

	public void BackToDefault () {

		thisSlider.value = GameManager.Instance.defaultCameraZoomSmoothness;
	}
}
