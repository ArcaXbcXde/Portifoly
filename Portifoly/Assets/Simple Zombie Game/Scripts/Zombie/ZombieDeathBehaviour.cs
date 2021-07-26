using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeathBehaviour : StateMachineBehaviour {

	private Rigidbody2D rigid;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		rigid = animator.GetComponent<Rigidbody2D>();
		rigid.velocity = Vector3.zero;
		rigid.gravityScale = 0;

	    animator.GetComponent<Collider2D>().enabled = false;
		animator.GetComponentInParent<ZombieSpawner>().zombieAmount--;

	}
}