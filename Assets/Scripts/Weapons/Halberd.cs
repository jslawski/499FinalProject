using UnityEngine;
using System.Collections;

public class Halberd : Weapon {

	//Halberds are long-ranged melee weapons (like a spear with an axe on the end)
	//They swing relatively slowly, and output a WIDE range of damage.  While they are
	//inconsistent with damage output, they are very effective at damaging multiple enemies
	//at once, and have a respectable crit rate.  Halberds require 2 hands to use

	// Use this for initialization
	protected override void Start () {
		base.Start();

		//These are arbitrarly picked numbers.  They can change as we see fit.
		if (weaponRarity == Rarity.Common) {
			damageMin = Random.Range(1, 1);
			damageMax = Random.Range(7, 9);
		}
		else if (weaponRarity == Rarity.Uncommon) {
			damageMin = Random.Range(2, 3);
			damageMax = Random.Range(10, 15);
		}
		else if (weaponRarity == Rarity.Rare) {
			damageMin = Random.Range(5, 6);
			damageMax = Random.Range(17, 21);
		}

		handsRequired = 2;
		attackCoolDown = 0.75f;

		//Currently I was thinking we could have all weapons of the same type have the same crit chance
		//I'm up for having this dependent on rarity as well
		critChance = 0.1f;

		PrintWeaponStats("Halberd");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
