using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackBehaviour : StateMachineBehaviour {

	public string animatorDeadParameter = "Death";

	private ZombieControl control;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
		control = animator.GetComponent<ZombieControl>();
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		CheckDeath(animator);
	}

	private void CheckDeath (Animator animator) {

		if (control.myHealth.Dead == true) {

			animator.SetTrigger(animatorDeadParameter);
		}
	}
}