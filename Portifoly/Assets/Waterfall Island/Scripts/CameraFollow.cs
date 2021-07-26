using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : CameraControl {
	
	#region public variables
	
	[Header ("Movement")]

	[Tooltip ("The highest angle the camera can pan")]
	[Range (0.0f, 90.0f)]
	public float cameraUpperBound = 90.0f;

	[Tooltip ("The lowest angle the camera can pan")]
	[Range(-90.0f, 0.0f)]
	public float cameraLowerBound = -90.0f;

	[Tooltip ("The starting camera rotation of this scene")]
	public Vector3 StartingRotation = new Vector3 (-45.0f, 45.0f, 0.0f);

	[Header("Zoom")]
	
	[Tooltip ("The CLOSEST the camera can get from the holder")]
	[Range (0.0f, 100.0f)]
	public float zoomMax = 0.1f;

	[Tooltip ("The FURTHEST the camera can get from the holder")]
	[Range(0.0f, 100.0f)]
	public float zoomMin = 20.0f;

	#endregion

	#region private variables

	// Variables to store the respective axis value
	private float movX, movY, movZ;

	// Easy access for both mouse axis used on the camera rotation
	private float camX, camY;

	// Easy acess for Mouse ScrollWheel axis used on the camera zoom
	private float scroll;

	// Camera target zoom distance
	private float zoomValue;

	// Camera local rotation
	private Vector3 thisLocalRotation;

	// Amount moved in the actual frame
	private Vector3 frameMovement;

	// Amount rotacioned in the actual frame
	private Quaternion frameRotation;

	// A direct reference to the game manager
	private GameManager manager;
	
	// The transform of the camera holder
	private Transform holderTransform;
	
	#endregion

	#region basic methods

	private void Awake () {

		holderTransform = transform.parent;

		thisLocalRotation = -StartingRotation;

	}

	private void Start () {
		
		manager = GameManager.Instance;
		
		if (manager.cameraHasZoom == true) {

			zoomValue = transform.position.z;
		} else {
			zoomValue = 0;
		}
	}

	private void Update () {

		KeyboardHandle();

		MovementHandle();
	}

	private void LateUpdate () {
		
		MouseHandle();

		RotationHandle();

		if (manager.cameraHasZoom == true) {

			ZoomHandle();
		}
	}

	#endregion

	#region handlers methods

	// Just gets the axis movement and put it in variables
	private void KeyboardHandle () {

		movX = Input.GetAxis("Horizontal");
		movY = Input.GetAxis("Frontal");
		movZ = Input.GetAxis("Vertical");
	}
	
	// Picks all movement results from all axis and make the movement
	private void MovementHandle () {

		if (manager.cameraUpDownDirection == 0) {

			frameMovement = new Vector3(movX, movY, movZ) * (manager.cameraMoveSpeed / 10);
			
		} else if (manager.cameraUpDownDirection == 1) {

			frameMovement = new Vector3(movX, 0, movZ) * (manager.cameraMoveSpeed / 10);

			holderTransform.position += Vector3.up * movY * (manager.cameraMoveSpeed / 10);
		} else {

			Debug.Log ("Dropdown das opções da câmera com o valor inválido: " + manager.cameraUpDownDirection);
		}
			holderTransform.Translate(frameMovement);
	}

	/* Simple method to handle mouse movement
	 * and set respective variables,
	 * if mouse is locked
	 */
	private void MouseHandle () {

		if (Cursor.lockState == CursorLockMode.Locked) {
			
			camX = Input.GetAxisRaw("Mouse X");
			camY = Input.GetAxisRaw("Mouse Y");

			scroll = Input.GetAxis("Mouse ScrollWheel");

			if (manager.invertXAxis == true) {
				camX *= -1;
			}
			if (manager.invertYAxis == true) {

				camY *= -1;
			}
			if (manager.invertCameraZoom == true) {

				scroll *= -1;
			}

		} else {

			camX = camY = 0;
		}
	}
	
	/* Calculate and rotate the camera
	 * based on mouse movement and some parameters
	 * to limit how much the camera can pan
	 * and interpolate smoothly
	 */
	private void RotationHandle () {

		if (camX != 0 || camY != 0) {

			// Gets mouse movement
			thisLocalRotation.x += camX * manager.cameraSensitivity;

			thisLocalRotation.y -= camY * manager.cameraSensitivity;

			// Clamp camera pan at the y axis
			thisLocalRotation.y = Mathf.Clamp(thisLocalRotation.y, cameraLowerBound, cameraUpperBound);
		}

		// Get camera target rotation
		frameRotation = Quaternion.Euler(thisLocalRotation.y, thisLocalRotation.x, 0.0f);

		// Interpolation between the actual rotation and the target rotation using "smoothness" value to make the rotation smooth
		holderTransform.rotation = Quaternion.Lerp(holderTransform.rotation, frameRotation, manager.cameraSmoothness * Time.deltaTime);
	}
	
	/* Calculate and apply zoom in the camera
	 * based on mousewheel scroll movement and others parameters
	 * to limit up to where the camera can zoom
	 * and interpolate smoothly
	 */
	private void ZoomHandle () {

		if (scroll != 0.0f) {
			
			// Amount of zoom to apply
			if (manager.invertCameraZoom == false) {

				zoomValue += scroll * manager.cameraZoomStrength * zoomValue * manager.cameraZoomIntensity;
			} else {

				zoomValue -= scroll * manager.cameraZoomStrength * zoomValue * manager.cameraZoomIntensity;
			}

			// Clamp zoom
			zoomValue = Mathf.Clamp(zoomValue, -zoomMin, -zoomMax);
		}

		if (transform.localPosition.z != -zoomValue) {

			// Interpolation between the actual zoom position and the target zoom position using "zoomSmoothness" value to make zooming smooth
			transform.localPosition = Vector3.forward * Mathf.Lerp(transform.localPosition.z, zoomValue, manager.cameraZoomSmoothness * Time.deltaTime);
		}
	}

	#endregion
}