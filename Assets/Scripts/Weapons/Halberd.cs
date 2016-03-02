using UnityEngine;
using System.Collections;

public class Halberd : Weapon {

	//Halberds are long-ranged melee weapons (like a spear with an axe on the end)
	//They swing relatively slowly, and output a WIDE range of damage.  While they are
	//inconsistent with damage output, they are very effective at damaging multiple enemies
	//at once, and have a respectable crit rate.  Halberds require 2 hands to use.
	//The hitbox for the halberd is narrow and long.  Its attack delay is fairly long.

	// Use this for initialization
	protected override void Start () {
		base.Start();

		weaponDimensions = Vector3.one * 1.5f;
		attackDelay = 0.25f;
		attackTime = 0.75f;
		swingAngle = 150;
		swingAxis = Vector3.up;

		//These are arbitrarly picked numbers.  They can change as we see fit.
		if (weaponRarity == Rarity.Common) {
			damageMin = Random.Range(10, 10);
			damageMax = Random.Range(70, 90);
		}
		else if (weaponRarity == Rarity.Uncommon) {
			damageMin = Random.Range(20, 30);
			damageMax = Random.Range(100, 150);
		}
		else if (weaponRarity == Rarity.Rare) {
			damageMin = Random.Range(50, 60);
			damageMax = Random.Range(170, 210);
		}

		handsRequired = 2;
		attackCoolDown = 0.4f;

		//Currently I was thinking we could have all weapons of the same type have the same crit chance
		//I'm up for having this dependent on rarity as well
		critChance = 0.1f;

		PrintWeaponStats("Halberd");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
