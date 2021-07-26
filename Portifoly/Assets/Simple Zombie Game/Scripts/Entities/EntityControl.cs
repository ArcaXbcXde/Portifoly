using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityControl : MonoBehaviour {
	
	[Tooltip ("The basic movement speed")]
	[Range (1.0f, 100.0f)]
	public float walkSpeed = 3;

	[Tooltip("Fast movement speed")]
	[Range(1.0f, 100.0f)]
	public float runSpeed = 6;
}