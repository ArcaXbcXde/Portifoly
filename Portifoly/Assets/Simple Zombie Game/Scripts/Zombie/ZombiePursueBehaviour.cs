using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePursueBehaviour : StateMachineBehaviour {

	[Tooltip("Time to zombie stop pursuing the player")]
	public float giveUpTime;

	public string animatorWalkParameter = "Walking";
	public string animatorPursueParameter = "Pursuing";
	public string animatorAttackParameter = "Melee";
	public string animatorDeadParameter = "Death";
	
	private float giveUpCd = 3.0f;

	private ZombieControl control;
	
	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		control = animator.GetComponent<ZombieControl>();

		giveUpCd = giveUpTime;

		animator.SetBool(animatorWalkParameter, true);
	}

	override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		CheckDeath(animator);

		GiveUpTimeControl(animator);

		PursueControl(animator);
	}

	private void CheckDeath (Animator animator) {

		if (control.myHealth.Dead == true) {

			animator.SetTrigger(animatorDeadParameter);
		}
	}

	private void GiveUpTimeControl (Animator animator) {

		if (CheckLineOfSight(animator) == false) {

			giveUpCd -= Time.deltaTime;

			if (giveUpCd <= 0) {

				animator.SetBool(animatorPursueParameter, false);
			}
		} else {

			giveUpCd = giveUpTime;
		}
	}

	private bool CheckLineOfSight (Animator animator) {
		
		control.rayEnd = control.target.position;
		control.rayDistance = control.viewDistance;

		if (control.hit.transform != null) {

			if (control.hit.transform.GetComponent<PlayerControl>() != null) {

				if (control.hit.distance < control.melee.range) {
					
					control.rigid.velocity = Vector2.zero;
					
					animator.SetTrigger(animatorAttackParameter);
				}
				return true;
			}
		}
		return false;
	}

	private void PursueControl (Animator animator) {

		if (control.target.position.x > animator.transform.position.x) {

			control.render.flipX = false;

			control.rigid.velocity = (Vector2.right * control.runSpeed + (Vector2.up * control.rigid.velocity.y));
		} else {

			control.render.flipX = true;

			control.rigid.velocity = (Vector2.right * -control.runSpeed + (Vector2.up * control.rigid.velocity.y));
		}
	}
}