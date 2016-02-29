using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Rarity { Common, Uncommon, Rare };

public class Weapon : MonoBehaviour {

	protected int damageMin;								//Min damage a weapon can output without a critical hit
	protected int damageMax;								//Max damage a weapon can output without a critical hit
	protected float critChance;								//Percent chance from 0 - 1 of landing a critical hit
	protected Rarity weaponRarity;							//Rarity of the weapon (determines its power)
	protected int handsRequired;							//Number of hands needed to hold the weapon (1 or 2)
	protected float attackCoolDown;                         //Time in seconds that the player has to wait before being able to attack again

	protected int numGemSlots;								//Number of slots a weapon has for enchantment gems
	protected List<Gems> attachedGems;						//List of all gems attached to the weapon.  Used for calculating and storing values in weaponEnchantments
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
}
