using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class OptionsDropdowns : Options {

	protected Dropdown thisDropdown;

	private void Awake () {

		thisDropdown = GetComponent<Dropdown>();
	}

	private void OnEnable () {

		thisDropdown.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
	}

	private void OnDisable () {

		thisDropdown.onValueChanged.RemoveListener(delegate { ValueChangeCheck(); });
	}
}
