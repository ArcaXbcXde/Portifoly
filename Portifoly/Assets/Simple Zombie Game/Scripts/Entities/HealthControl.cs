using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControl : MonoBehaviour {

	[System.Serializable]
	public struct ElementResistance {

		public float electric;
		public float fire;
		public float neutral;
		public float poison;
	}

	[System.Serializable]
	public struct DamageResistance {

		public float neutral;
		public float blunt;
		public float cutting;
		public float perfurating;
		public float explosive;
		public float special;
	}

	[Tooltip("The maximum hp")]
	[Range(1, 100)]
	public int maxHP = 10;

	[HideInInspector]
	public int hp;

	public ElementResistance elementResistance;
	public DamageResistance damageResistance;

	// A boolean method that when called returns if the entity is dead comparing if its hp is equal or lower than 0
	public bool Dead {
		get {

			return (hp <= 0);
		}
	}

	private void Awake () {
		
		InitialHP();
	}

	public void TakeDamage (Damage damageTaken) {
		if (Dead == true) {

			return;
		}

		hp -= damageTaken.damageAmount;

		if (Dead == true) {

			Death();
		}

	}
	
	public void Death () {
		
		hp = 0;
		
		Invoke ("Disappear", 5.0f);
	}

	private void Disappear () {

		gameObject.SetActive(false);
	}

	private void InitialHP () {

		hp = maxHP;
	}
}