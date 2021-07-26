using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHaltBehaviour : StateMachineBehaviour {

	public string animatorWalkParameter = "Walking";
	public string animatorAttackParameter = "Melee";
	public string animatorDeadParameter = "Death";
	
	private ZombieControl control;

	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		control = animator.GetComponent<ZombieControl>();
	}

	override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		CheckDeath(animator);

		CheckDistance(animator);
	}

	private void CheckDeath (Animator animator) {

		if (control.myHealth.Dead == true) {

			animator.SetTrigger(animatorDeadParameter);
		}
	}

	private void CheckDistance (Animator animator) {
		
		control.rayEnd = control.target.position;
		control.rayDistance = control.melee.range;
		
		if (control.hit.transform != null) {

			if (control.hit.transform.GetComponent<PlayerControl>() != null) {

				control.rigid.velocity = Vector2.zero;

				animator.SetTrigger(animatorAttackParameter);
			}
		}

		if (control.target.GetComponent<HealthControl>().Dead == false) {

			animator.SetBool(animatorWalkParameter, true);
		} else {

			animator.SetBool(animatorWalkParameter, false);
		}
	}
}