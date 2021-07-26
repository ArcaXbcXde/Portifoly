using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderCameraMoveSpeed : OptionsSliders {

	private void Start () {

		thisSlider.value = GameManager.Instance.cameraMoveSpeed;
	}

	protected override void ValueChangeCheck () {

		GameManager.Instance.cameraMoveSpeed = thisSlider.value;
	}

	public void BackToDefault () {

		thisSlider.value = GameManager.Instance.defaultCameraMoveSpeed;
	}
}
