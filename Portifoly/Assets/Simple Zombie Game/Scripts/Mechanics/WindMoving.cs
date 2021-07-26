using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMoving : MonoBehaviour {

	[Tooltip ("Maximum distance between the camera and the back object to be repositioned forward")]
	public float maxDistance;

	[Tooltip ("Units travelled each second")]
	public float speed;

	[Tooltip ("The exact distance each object must be from each other")]
	public float distanceBetweenObjects;
	
	[Tooltip ("The camera to be the reference")]
	public Transform cameraRef;

	[SerializeField]
	private Transform[] carriedsByWind;

    private void Start () {

	    // Picks all children from this component's object and adds to the vector, also puts itself on 0 so the index 0 should never be used
		carriedsByWind = GetComponentsInChildren<Transform>();
    }
	
    private void Update () {

		for (int i = 1; i < carriedsByWind.Length; i++) {

			carriedsByWind[i].position += Vector3.right * speed * Time.deltaTime; // moves everything accordingly

			if (carriedsByWind[i].position.x < (cameraRef.position.x - (maxDistance * (carriedsByWind.Length - 2)))) { // if the clouds is too away to the left
				if (i == 1) { // if its the first object in the vector (not counting 0)

					carriedsByWind[i].position = (Vector3.right * (carriedsByWind[carriedsByWind.Length - 1].position.x + distanceBetweenObjects)) + (Vector3.up * carriedsByWind[i].position.y);
				} else {

					carriedsByWind[i].position = Vector3.right * ((carriedsByWind[i - 1].position.x + distanceBetweenObjects)) + (Vector3.up * carriedsByWind[i].position.y);
				}
			} else if (carriedsByWind[i].position.x > (cameraRef.position.x + (maxDistance * (carriedsByWind.Length - 2)))) { // if the clouds is too away to the right
				if (i == 1) { // if its the first object in the vector (not counting 0)

					carriedsByWind[i].position = (Vector3.right * (carriedsByWind[carriedsByWind.Length - 1].position.x - distanceBetweenObjects)) + (Vector3.up * carriedsByWind[i].position.y);
				} else {

					carriedsByWind[i].position = Vector3.right * ((carriedsByWind[i - 1].position.x - distanceBetweenObjects)) + (Vector3.up * carriedsByWind[i].position.y);
				}

			}
		}
    }
}