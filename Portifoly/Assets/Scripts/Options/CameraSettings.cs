using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour {

	[Header("Movement")]

	[Tooltip("Speed which the camera moves throught the scene")]
	[Range(0.1f, 10.0f)]
	public float moveSpeed = 0.5f;

	[Tooltip("Speed which the mouse travels through the screen")]
	[Range(1.0f, 20.0f)]
	public float sensitivity = 5.0f;

	[Tooltip("Makes mouse movimentation smoother")]
	[Range(1.0f, 50.0f)]
	public float smoothness = 10.0f;

	[Header("Zoom")]

	[Tooltip("Amount of zoom applied each mousewheel spin")]
	[Range(1.0f, 20.0f)]
	public float zoomStrength = 2.0f;

	[Tooltip("Makes the zoom smoother")]
	[Range(1.0f, 50.0f)]
	public float zoomSmoothness = 5.0f;

	[Tooltip("Makes the zoom stronger while further and weaker while nearer")]
	[Range(0.0f, 1.0f)]
	public float zoomIntensity = 0.333333f;

}