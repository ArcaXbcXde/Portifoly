using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderCameraSmoothness : OptionsSliders {

	private void Start () {

		thisSlider.value = GameManager.Instance.cameraSmoothness;
	}

	protected override void ValueChangeCheck () {

		GameManager.Instance.cameraSmoothness = thisSlider.value;
	}

	public void BackToDefault () {

		thisSlider.value = GameManager.Instance.defaultCameraSmoothness;
	}
}
