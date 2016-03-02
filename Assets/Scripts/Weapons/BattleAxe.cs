using UnityEngine;
using System.Collections;

public class BattleAxe : Weapon {

	//Battle axes are slow and powerful.  They are held with two hands.
	//The range of their potential damage output is narrower than most weapons.
	//Given that they hit so hard, their crit chance is fairly low.  The hitbox
	//for a battle axe is wide and short.  Its attack delay is fairly long.

	// Use this for initialization
	protected override void Start () {
		base.Start();

		attackDelay = 0.3f;

		//These are arbitrarly picked numbers.  They can change as we see fit.
		if (weaponRarity == Rarity.Common) {
			damageMin = Random.Range(70, 100);
			damageMax = Random.Range(120, 150);
		}
		else if (weaponRarity == Rarity.Uncommon) {
			damageMin = Random.Range(130, 170);
			damageMax = Random.Range(190, 220);
		}
		else if (weaponRarity == Rarity.Rare) {
			damageMin = Random.Range(200, 240);
			damageMax = Random.Range(270, 320);
		}

		handsRequired = 2;
		attackCoolDown = 0.5f;

		//Currently I was thinking we could have all weapons of the same type have the same crit chance
		//I'm up for having this dependent on rarity as well
		critChance = 0.03f;

		PrintWeaponStats("Battle Axe");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
