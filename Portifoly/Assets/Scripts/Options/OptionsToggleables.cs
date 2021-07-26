using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class OptionsToggleables : Options {

	protected Toggle thisToggle;

	private void Awake () {

		thisToggle = GetComponent<Toggle>();
	}

	private void OnEnable () {

		thisToggle.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
	}

	private void OnDisable () {

		thisToggle.onValueChanged.RemoveListener(delegate { ValueChangeCheck(); });
	}
}