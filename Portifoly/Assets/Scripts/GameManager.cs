using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	#region keys variables
	
	public Keys defaultKeys;

	public Keys keys;

	#endregion

	#region camera variables

	[Header("Camera Movement")]

	[Tooltip ("If moving the mouse left and right pans the camera counter-clockwise and clockwise or the opposite")]
	[InspectorName ("inverted X axis")]
	public bool defaultInvertXAxis = false;
	[HideInInspector]
	public bool invertXAxis = false;

	[Tooltip ("If moving the mouse front and back pans the camera up and down or the opposite")]
	[InspectorName("inverted Y axis")]
	public bool defaultInvertYAxis = false;
	[HideInInspector]
	public bool invertYAxis = false;

	[Tooltip ("If the camera Y axis follows local position (0) or world position(1)")]
	[InspectorName("camera up down direction")]
	public int defaultCameraUpDownDirection = 0;
	[HideInInspector]
	public int cameraUpDownDirection = 0;

	[Tooltip("Speed which the camera moves throught the scene")]
	[Range(0.1f, 10.0f)]
	[InspectorName("Move speed")]
	public float defaultCameraMoveSpeed = 0.5f;
	[HideInInspector]
	public float cameraMoveSpeed = 0.5f;

	[Tooltip("Speed which the mouse travels through the screen")]
	[Range(1.0f, 20.0f)]
	[InspectorName("Sensitivity")]
	public float defaultCameraSensitivity = 5.0f;
	[HideInInspector]
	public float cameraSensitivity = 5.0f;

	[Tooltip("Makes mouse movimentation smoother")]
	[Range(1.0f, 50.0f)]
	[InspectorName("Smoothness")]
	public float defaultCameraSmoothness = 10.0f;
	[HideInInspector]
	public float cameraSmoothness = 10.0f;

	[Header("Camera Zoom")]

	[Tooltip ("If the camera is able to zoom")]
	[InspectorName("Camera has zoom")]
	public bool defaultCameraHasZoom = false;
	[HideInInspector]
	public bool cameraHasZoom = false;

	[Tooltip ("If scrolling the mousewheel forward and backward zooms in and out or the opposite")]
	[InspectorName("Invert zoom direction")]
	public bool defaultInvertCameraZoom = true;
	[HideInInspector]
	public bool invertCameraZoom = true;

	[Tooltip("Amount of zoom applied each mousewheel spin")]
	[Range(1.0f, 20.0f)]
	[InspectorName("Zoom strength")]
	public float defaultCameraZoomStrength = 2.0f;
	[HideInInspector]
	public float cameraZoomStrength = 2.0f;

	[Tooltip("Makes the zoom smoother")]
	[Range(1.0f, 50.0f)]
	[InspectorName("Zoom smoothness")]
	public float defaultCameraZoomSmoothness = 5.0f;
	[HideInInspector]
	public float cameraZoomSmoothness = 5.0f;

	[Tooltip("Makes the zoom stronger while further and weaker while nearer")]
	[Range(0.0f, 1.0f)]
	[InspectorName("Zoom intensity")]
	public float defaultCameraZoomIntensity = 0.333333f;
	[HideInInspector]
	public float cameraZoomIntensity = 0.333333f;
	#endregion

	/// <summary>
	/// A static instance of this script as object to be called when needed
	/// </summary>
	public static GameManager Instance {

		get;
		private set;
	}

	// Call initialization methods
	private void Awake () {

		InitialSet();
	}

	// Sets this as its own singleton instance, and then guarantee that there is one and only one instance of this
	private void InitialSet () {

		if (Instance == null) {

			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {

			Destroy(gameObject);
		}
	}

	public void SetCameraDefaults () {

		invertXAxis = defaultInvertXAxis;
		invertYAxis = defaultInvertYAxis;
		cameraUpDownDirection = defaultCameraUpDownDirection;
		cameraMoveSpeed = defaultCameraMoveSpeed;
		cameraSensitivity = defaultCameraSensitivity;
		cameraSmoothness = defaultCameraSmoothness;
		cameraHasZoom = defaultCameraHasZoom;
		invertCameraZoom = defaultInvertCameraZoom;
		cameraZoomStrength = defaultCameraZoomStrength;
		cameraZoomSmoothness = defaultCameraZoomSmoothness;
		cameraZoomIntensity = defaultCameraZoomIntensity;
	}
}