using UnityEngine;
using System.Collections;

public class Dagger : Weapon {

	//Daggers are short-ranged weapons that only use 1 hand
	//Of all of the weapons, they have the fastest attack speed.
	//In terms of damage output, they are on the lower end, and their crit chance is about average

	// Use this for initialization
	protected override void Start () {
		base.Start();

		//These are arbitrarly picked numbers.  They can change as we see fit.
		if (weaponRarity == Rarity.Common) {
			damageMin = Random.Range(1, 2);
			damageMax = Random.Range(4, 6);
		}
		else if (weaponRarity == Rarity.Uncommon) {
			damageMin = Random.Range(4, 5);
			damageMax = Random.Range(7, 10);
		}
		else if (weaponRarity == Rarity.Rare) {
			damageMin = Random.Range(7, 9);
			damageMax = Random.Range(13, 16);
		}

		handsRequired = 1;
		attackCoolDown = 0.45f;

		//Currently I was thinking we could have all weapons of the same type have the same crit chance
		//I'm up for having this dependent on rarity as well
		critChance = 0.05f;

		PrintWeaponStats("Dagger");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
