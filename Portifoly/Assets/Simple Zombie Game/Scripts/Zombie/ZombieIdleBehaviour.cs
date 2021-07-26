using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleBehaviour : StateMachineBehaviour {
	
	[Tooltip ("Minimum time for the zombie to wander again")]
	[Range (0, 60)]
	public float minIdleTime;

	[Tooltip("Maximum time for the zombie to wander again")]
	[Range(0, 60)]
	public float maxIdleTime;

	public string animatorWalkParameter = "Walking";
	public string animatorPursueParameter = "Pursuing";
	public string animatorDeadParameter = "Death";

	[SerializeField]
	private float idleTime;

	private ZombieControl control;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		control = animator.GetComponent<ZombieControl>();

		if (control.wanderer == true) {

			idleTime = Random.Range(minIdleTime, maxIdleTime);
		}
	}
	
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		CheckDeath(animator);

		if (control.wanderer == true) {
			
			IdleTimeControl (animator);
		}

		VisionControl (animator);
	}

	private void CheckDeath (Animator animator) {

		if (control.myHealth.Dead == true) {

			animator.SetTrigger(animatorDeadParameter);
		}

	}

	private void IdleTimeControl (Animator animator) {

		idleTime -= Time.deltaTime;

		if (idleTime <= 0) {

			animator.SetBool(animatorWalkParameter, true);
		}
	}

	private void VisionControl (Animator animator) {
		
		control.rayDistance = control.viewDistance;

		if (control.render.flipX == false) {

			control.rayEnd = animator.transform.position + (Vector3.right * control.viewDistance);
		} else {

			control.rayEnd = animator.transform.position + (Vector3.right * -control.viewDistance);
		}

		if (control.hit.transform != null) {
			if (control.hit.transform.GetComponent<PlayerControl>() != null) {

				control.target = control.hit.transform.GetComponent<Transform>();

				animator.SetBool(animatorPursueParameter, true);
			}
		}
	}
}