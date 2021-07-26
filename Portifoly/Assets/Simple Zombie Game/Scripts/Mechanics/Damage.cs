using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfDamage {
	
	Neutral,
	Perfurating,
	Cutting,
	Blunt,
	Explosive,
	Special,
}

public enum ElementOfDamage {

	Neutral,
	Fire,
	Electric,
	Poison,

}

public class Damage {
	
	public int damageAmount;

	public float critChance;

	public TypeOfDamage damageType;

	public ElementOfDamage damageElement;

	public Damage (int damageValue) {

		damageAmount = damageValue;
		critChance = 0;
		damageType = TypeOfDamage.Neutral;
		damageElement = ElementOfDamage.Neutral;
	}

	public Damage (int damageValue, int crit) {

		damageAmount = damageValue;
		critChance = crit;
		damageType = TypeOfDamage.Neutral;
		damageElement = ElementOfDamage.Neutral;
	}

	public Damage (int damageValue, TypeOfDamage type) {

		damageAmount = damageValue;
		critChance = 0;
		damageType = type;
		damageElement = ElementOfDamage.Neutral;
	}

	public Damage (int damageValue, ElementOfDamage element) {

		damageAmount = damageValue;
		critChance = 0;
		damageType = TypeOfDamage.Neutral;
		damageElement = element;
	}
	
	public Damage (int damageValue, int crit, TypeOfDamage type) {

		damageAmount = damageValue;
		critChance = crit;
		damageType = type;
		damageElement = ElementOfDamage.Neutral;
	}

	public Damage (int damageValue, int crit, ElementOfDamage element) {

		damageAmount = damageValue;
		critChance = crit;
		damageType = TypeOfDamage.Neutral;
		damageElement = element;
	}

	public Damage (int damageValue, TypeOfDamage type, ElementOfDamage element) {

		damageAmount = damageValue;
		critChance = 0;
		damageType = type;
		damageElement = element;
	}
	
	public Damage (int damageValue, int crit, TypeOfDamage type, ElementOfDamage element) {

		damageAmount = damageValue;
		critChance = crit;
		damageType = type;
		damageElement = element;
	}
}