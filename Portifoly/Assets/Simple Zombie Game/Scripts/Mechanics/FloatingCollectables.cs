using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCollectables : Collectables {

	[Tooltip("Speed of the oscilation")]
	[Range(0, 10)]
	public float oscilationSpeed = 1;
	
	[Tooltip("Distance in X and/or Y the object oscilates")]
	public Vector2 oscilationAmount = new Vector2(0, 1);
	
	private Vector2 startingPos;

	private Vector2 actualPos;
	
	protected void Start () {

		startingPos = transform.position;
	}

	protected void Update () {

		Oscilate();
	}

	private void Oscilate () {

		actualPos = startingPos + Mathf.Sin(Time.fixedTime * Mathf.PI * oscilationSpeed) * oscilationAmount;

		transform.position = actualPos;
	}
}