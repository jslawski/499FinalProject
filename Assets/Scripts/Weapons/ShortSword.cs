using UnityEngine;
using System.Collections;

public class ShortSword : Weapon {

	//Short Swords will be able to swing fast, and are held with one hand
	//Their damage output is relatively low, but their critical chance is 
	//slightly above average.  They will potentially be able to be dual wielded.
	//The hitbox for a short sword is narrow and slightly short.  It's attack delay
	//is relatively short.

	// Use this for initialization
	protected override void Start () {
		base.Start();

		hitboxDimensions = new Vector3(1, 1, 1.5f);

		attackDelay = 0.1f;

		//These are arbitrarly picked numbers.  They can change as we see fit.
		if (weaponRarity == Rarity.Common) {
			damageMin = Random.Range(1, 3);
			damageMax = Random.Range(5, 7);
		}
		else if (weaponRarity == Rarity.Uncommon) {
			damageMin = Random.Range(4, 6);
			damageMax = Random.Range(9, 12);
		}
		else if (weaponRarity == Rarity.Rare) {
			damageMin = Random.Range(10, 12);
			damageMax = Random.Range(15, 17);
		}

		handsRequired = 1;
		attackCoolDown = 0.2f;

		//Currently I was thinking we could have all weapons of the same type have the same crit chance
		//I'm up for having this dependent on rarity as well
		critChance = 0.085f;

		PrintWeaponStats("Short Sword");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
