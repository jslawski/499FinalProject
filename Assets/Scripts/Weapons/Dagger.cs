using UnityEngine;
using System.Collections;

public class Dagger : Weapon {

	//Daggers are short-ranged weapons that only use 1 hand
	//Of all of the weapons, they have the fastest attack speed.
	//In terms of damage output, they are on the lower end, and their crit chance is about average
	//The hitbox for the dagger is short and narrow.  It's attack delay is almost non-existant.

	// Use this for initialization
	protected override void Start () {
		base.Start();

		weaponType = "Dagger";

		attackDelay = 0.05f;

		//These are arbitrarly picked numbers.  They can change as we see fit.
		if (weaponRarity == Rarity.Common) {
			damageMin = Random.Range(10, 20);
			damageMax = Random.Range(40, 60);
		}
		else if (weaponRarity == Rarity.Uncommon) {
			damageMin = Random.Range(40, 50);
			damageMax = Random.Range(70, 100);
		}
		else if (weaponRarity == Rarity.Rare) {
			damageMin = Random.Range(70, 90);
			damageMax = Random.Range(130, 160);
		}

		handsRequired = 1;
		attackCoolDown = 0.1f;

		//Currently I was thinking we could have all weapons of the same type have the same crit chance
		//I'm up for having this dependent on rarity as well
		critChance = 0.05f;

		weaponName = NameGenerator.GenerateName(this);

		PrintWeaponStats("Dagger");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
