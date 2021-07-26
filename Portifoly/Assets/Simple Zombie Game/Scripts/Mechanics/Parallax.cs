using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	[Tooltip ("Amount of movement the parallax moves in comparison with the camera's movement, in the X and Y axis")]
	public Vector2 parallaxAmount;

	[Tooltip ("The camera to be the reference")]
	public Transform cameraRef;

	private Vector2 startingPosition;

    void Awake() {

		startingPosition = transform.position;
    }
	
    void Update() {

        transform.position = (cameraRef.position * parallaxAmount) + startingPosition;
    }
}