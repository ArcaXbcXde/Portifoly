using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoor : Mechanicals {
	
	[Tooltip ("Speed which it moves")]
	[Range (0.1f, 50.0f)]
	public float moveSpeed = 5.0f;

	[Tooltip ("To where it is supposed to move")]
	public Vector3 movePosition;

	// The starting position of this object
	private Vector3 initialPosition;

	private void Awake () {
		
		initialPosition = transform.position;
	}

	// Starts moving this object to the set "movePosition" position
	protected override void OnActivate () {

		StartCoroutine(CorroutineMoveObject(true, movePosition));
	}

	// Starts moving this object to the starting position
	protected override void OnDeactivate () {

		StartCoroutine(CorroutineMoveObject(false, initialPosition));
	}

	// Corroutine to start moving the object to the set "moveToPosition" position
	private IEnumerator CorroutineMoveObject (bool actualActivation, Vector3 moveToPosition) {

		while ((Vector3.Distance(transform.position, moveToPosition) > moveSpeed * Time.deltaTime) && (isActive == actualActivation)) {

			transform.position += (moveToPosition - transform.position).normalized * moveSpeed * Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		transform.position -= (transform.position - moveToPosition);
	}
}