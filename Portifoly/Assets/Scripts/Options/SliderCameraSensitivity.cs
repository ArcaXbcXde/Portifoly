using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderCameraSensitivity : OptionsSliders {

	private void Start () {

		thisSlider.value = GameManager.Instance.cameraSensitivity;
	}

	protected override void ValueChangeCheck () {

		GameManager.Instance.cameraSensitivity = thisSlider.value;
	}

	public void BackToDefault () {

		thisSlider.value = GameManager.Instance.defaultCameraSensitivity;
	}
}
