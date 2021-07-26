using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class OptionsSliders : Options {

	protected Slider thisSlider;

	private void Awake () {

		thisSlider = GetComponent<Slider>();
	}

	private void OnEnable () {

		thisSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
	}

	private void OnDisable () {

		thisSlider.onValueChanged.RemoveListener(delegate { ValueChangeCheck(); });
	}
}
