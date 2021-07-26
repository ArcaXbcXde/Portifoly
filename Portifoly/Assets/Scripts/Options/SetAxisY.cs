using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAxisY : OptionsToggleables {
	
	private void Start () {
		
		thisToggle.isOn = GameManager.Instance.invertYAxis;
	}

	protected override void ValueChangeCheck () {

		GameManager.Instance.invertYAxis = thisToggle.isOn;
	}

	public void BackToDefault () {

		thisToggle.isOn = GameManager.Instance.defaultInvertYAxis;
	}
}