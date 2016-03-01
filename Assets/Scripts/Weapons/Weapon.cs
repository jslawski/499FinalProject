using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Rarity { Common, Uncommon, Rare };

public class Weapon : MonoBehaviour {

	public Vector3 hitboxDimensions;						//Dimensions of collider when the weapon is equipped.  Varies for different weapons.

	protected float damageMin;                                //Min damage a weapon can output without a critical hit
	protected float damageMax;                                //Max damage a weapon can output without a critical hit
	protected float critChance;                             //Percent chance from 0 - 1 of landing a critical hit
	protected Rarity weaponRarity;                          //Rarity of the weapon (determines its power)
	protected int handsRequired;                            //Number of hands needed to hold the weapon (1 or 2)

	public float attackCoolDown;                            //Time in seconds that the player has to wait before being able to attack again
	public float attackDelay;                               //Time in seconds that the player has to wait between pressing the attack button and the attack getting executed

	protected int numGemSlots;                              //Number of slots a weapon has for enchantment gems
	protected List<Gems> attachedGems;                      //List of all gems attached to the weapon.  Used for calculating and storing values in weaponEnchantments
	protected Dictionary<string, float> weaponEnchantments;	//Dict of all of the current enchantment abiltities on the weapon

	// Use this for initialization
	protected virtual void Start () {
		//I would like to have this dynamically determined, sometimes by random chance, and other times depending on the chest it comes out of
		//Having a "rare chest" that has a guaranteed rare item would be kind of cool.  For now, the rarity is preset in this function
		weaponRarity = Rarity.Rare;

		//For testing purposes, each weapon is given a random number of enchantment slots, with a random selection of gems.  
		//I'd like to explore the possibility of varying the number of slots based on weapon type, weapon rarity, or other factors in the future.
		//Gem slots will be populated only when the weapon is enchanted at a blacksmith.  For now, I'm just putting random ones in automatically
		//for testing.
		numGemSlots = Random.Range(1, 6);
		attachedGems = new List<Gems>();
		for (int i = 0; i < numGemSlots; i++) {
			attachedGems.Add((Gems)Random.Range(0, (int)Gems.NumberOfTypes));
		}

		//I plan to populate this field with enchantment calculations next.  Right now it does nothing.
		weaponEnchantments = new Dictionary<string, float>();
	}

	protected void PrintWeaponStats(string weaponType) {
		Debug.Log("<b>Weapon Stats (" + weaponType + "):</b>\n<i>Rarity: </i>" + weaponRarity + "\n<i>Damage Min: </i>" + damageMin +
				  "\n<i>Damage Max: </i>" + damageMax + "\n<i>Crit Chance: </i>" + critChance + "\n<i>Attack Cooldown: </i>" + attackCoolDown +
				  "\n<i>Hands Required: </i>" + handsRequired + "\n<i>Gems Equipped: </i>" + PrintGems());
    }

	string PrintGems() {
		string returnValue = "";

		foreach (Gems gem in attachedGems) {
			returnValue += "\n     " + gem.ToString();
		}
		return returnValue;
	}

	void OnTriggerEnter(Collider other) {
		DamageableObject objCanBeHit = other.gameObject.GetComponent<DamageableObject>();
		if (objCanBeHit != null) {
			DealDamage(objCanBeHit);
		} 
	}

	//Deal damage to every enemy that was inside the attack hitbox when it was enabled
	void DealDamage(DamageableObject other) {
		//Determine damage dealt for the attack
		float attackDamage = Random.Range(damageMin, damageMax + 1);

		//Determine if the attack was a critical hit
		//If so, double the damage dealt
		if (Random.Range(0, 1.0f) <= critChance) {
			attackDamage = attackDamage * 2;
		}

		//TODO: Apply calculations for enchantments, such as additional crit chance, freezing chance, etc.

		other.TakeDamage(attackDamage);
	}
}
