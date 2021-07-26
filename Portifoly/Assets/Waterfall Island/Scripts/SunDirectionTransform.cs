using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDirectionTransform : MonoBehaviour {
	
	// ! need to be substituted from the sky shader
	public float timeMultiplier = 0.5f;


	private void Update() {

		Shader.SetGlobalVector ("Sun_Direction", transform.forward);
		
		SetLightIntensity();
	}
	/// <summary>
	/// Changes the light intensity real-time with the sky in the scene, following every step on the "DayNight Cycle" inside the sky shader.
	/// if there was possible to get the value directly it wouldn't be necessary.
	/// but its still possible to optimize the shader by substituting the "DayNight Cycle" resulting values from here. 
	/// </summary>
	private void SetLightIntensity () {

		float step1 = Time.time * timeMultiplier;

		float step2 = Mathf.Sin(step1);

		float step3 = 0.2f + (step2 + 1) * (3.0f - 0.2f) / (1 + 1);

		bool comparison = step3 < 0.5 ? true : false;

		float step4 = 1 - step3;

		float step5 = comparison ? step3 : step4;

		float step6 = 0.2f + (step5 + 1) * (3.0f - 0.2f) / (1 + 1);

		if (step6 > 0.2f) {

			GetComponent<Light>().intensity = step6;
		} else {

			GetComponent<Light>().intensity = 0.2f;
		}
	}
}