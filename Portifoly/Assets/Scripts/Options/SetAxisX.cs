using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAxisX : OptionsToggleables {

	private void Start () {
		
		thisToggle.isOn = GameManager.Instance.invertXAxis;
	}

	protected override void ValueChangeCheck () {

		GameManager.Instance.invertXAxis = thisToggle.isOn;
	}

	public void BackToDefault () {

		thisToggle.isOn = GameManager.Instance.defaultInvertXAxis;
	}
}