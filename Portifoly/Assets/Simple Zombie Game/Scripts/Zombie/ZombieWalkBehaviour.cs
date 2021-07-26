using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWalkBehaviour : StateMachineBehaviour {

	[Tooltip ("Minimum time untill the zombie stop walking")]
	[Range(0.0f, 60.0f)]
	public float minWalkTime;

	[Tooltip("Maximum time untill the zombie stop walking")]
	[Range (0.0f, 60.0f)]
	public float maxWalkTime;

	public string animatorWalkParameter = "Walking";
	public string animatorPursueParameter = "Pursuing";
	public string animatorDeadParameter = "Death";

	private float walkTime;

	private float walkDirection;
	
	private ZombieControl control;
	
	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		control = animator.GetComponent<ZombieControl>();

		walkDirection = Random.Range (-1.0f, 1.0f);

		if (walkDirection < 0.0f) {

			control.render.flipX = false;
		} else {

			control.render.flipX = true;
		}

		walkTime = Random.Range (minWalkTime, maxWalkTime);
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		CheckDeath(animator);

		VisionControl(animator);

		WalkTimeControl(animator);

		WalkControl();
	}
	
	private void CheckDeath (Animator animator) {

		if (control.myHealth.Dead == true) {

			animator.SetTrigger(animatorDeadParameter);
		}
	}
	
	private void WalkTimeControl (Animator animator) {

		walkTime -= Time.deltaTime;

		if (walkTime <= 0) {

			animator.SetBool(animatorWalkParameter, false);
		}
	}

	private void WalkControl () {

		if (walkDirection < 0.0f) {

			control.rigid.velocity = (Vector2.right * control.walkSpeed + (Vector2.up * control.rigid.velocity.y));
		} else {

			control.rigid.velocity = (Vector2.right * -control.walkSpeed + (Vector2.up * control.rigid.velocity.y));
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
			} else if (control.hit.distance < control.melee.range) {

				walkDirection *= -1;
				control.render.flipX = !control.render.flipX;
			}
		}
	}
}