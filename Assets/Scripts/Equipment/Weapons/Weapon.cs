using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Rarity { Common, Uncommon, Rare };

public class Weapon : MonoBehaviour, Equipment {
	//~~~~~~Weapon Stats~~~~~~
	protected string weaponName;								//Full name of the weapon
	public string weaponType;									//Type of weapon (battle axe, halberd, dagger, etc.).  Used for name generation. Always preceded with a space
	protected float damageMin;									//Min damage a weapon can output without a critical hit
	protected float damageMax;									//Max damage a weapon can output without a critical hit
	public Vector3 weaponDimensions;							//Size and shape of the weapon
	protected float critChance;									//Percent chance from 0 - 1 of landing a critical hit
	protected Rarity weaponRarity;								//Rarity of the weapon (determines its power)
	protected int handsRequired;								//Number of hands needed to hold the weapon (1 or 2)

	//~~~~~~Attacking~~~~~~
	public float attackCoolDown;                            //Time in seconds that the player has to wait before being able to attack again
	public float attackDelay;                               //Time in seconds that the player has to wait between pressing the attack button and the attack getting executed
	public float attackTime;								//Time in seconds that the weapon takes to complete its swing (and how long its hitbox will be active)
	public float swingAngle;                                //Total degree change from start to finish for the swinging animation
	public Vector3 swingAxis;                               //Euler axis to rotate about while swinging

	//~~~~~~Enchantments~~~~~~
	protected int weaponGemSlots;								//Number of slots a weapon has for enchantment gems
	protected List<Gems> attachedGems;							//List of all gems attached to the weapon.  Used for calculating and storing values in weaponEnchantments
	public Dictionary<Enchantments, float> weaponEnchantments;	//Dict of all of the current enchantment abiltities on the weapon
	protected float baseEnchantmentChance = 0.15f;				//Base scalar (0-1) that enchantment calculations use to determine percent-chance of enchantment triggering 
	static protected Gems targetGem;                            //Used in the predicate FindGem() to find all gems of a specific type.  Used for percent-chance calculations 

	/*~~~~~~~~~~Properties~~~~~~~~~~*/
	public string equipmentName {
		get { return weaponName; }
	}

	public EquipmentType equipmentType {
		get { return EquipmentType.Weapon; }
	}

	public float minValue {
		get { return damageMin; }
	}

	public float maxValue {
		get { return damageMax; }
	}

	public Rarity rarity {
		get { return weaponRarity; }
		set { weaponRarity = value; }
	}

	public Dictionary<Enchantments, float> enchantments {
		get { return weaponEnchantments; }
	}

	public string equipmentObject {
		get { return weaponType; }
	}

	public int numGemSlots {
		get { return weaponGemSlots; }
	}

	// Use this for initialization
	protected virtual void Start () {
		//I would like to have this dynamically determined, sometimes by random chance, and other times depending on the chest it comes out of
		//Having a "rare chest" that has a guaranteed rare item would be kind of cool.  For now, the rarity is preset in this function
		weaponRarity = (Rarity)Random.Range(0, 3);

		//For testing purposes, each weapon is given a random number of enchantment slots, with a random selection of gems.  
		//I'd like to explore the possibility of varying the number of slots based on weapon type, weapon rarity, or other factors in the future.
		//Gem slots will be populated only when the weapon is enchanted at a blacksmith.  For now, I'm just putting random ones in automatically
		//for testing.
		weaponGemSlots = Random.Range(0, 6);
		attachedGems = new List<Gems>();
		for (int i = 0; i < numGemSlots; i++) {
			attachedGems.Add((Gems)Random.Range(0, (int)Gems.NumberOfTypes));
		}

		//Populate dictionary with all of the weapon's enchantments
		weaponEnchantments = new Dictionary<Enchantments, float>();
		CalculateEnchantmentPercents();
	}

	//Calculate percent-chances for each enchantment on the weapon
	//Right now they are all calculated the same way.
	public void CalculateEnchantmentPercents() {
		//Iterate through all of the gem types
		for (int i = 0; i < (int)Gems.NumberOfTypes; i++) {
			//Specify a target gem that is currently being searched for
			targetGem = (Gems)i;
			
			//Find all instances of the target gem on the weapon
			List<Gems> foundGems = attachedGems.FindAll(FindGem);

			//Add a key to the dictionary initialized to 0 if an instance
			//of the gem was found
			if (foundGems.Count > 0) {
				weaponEnchantments.Add((Enchantments)i, 0);
			}

			//Calculate percent-chance for triggering that enchantment based on
			//number of gems found.  Implement diminishing returns for each stacked gem.
			//1 Gem	 = 1/2 * baseEnchantmentChance
			//2 Gems = 1GemChance + (1/3 * baseEnchantmentChance)
			//3 Gems = 2GemChance + (1/4 * baseEnchantmentChance)
			//...etc.
			for (float j = 1; j <= foundGems.Count; j++) {
				//Update dictionary value
				weaponEnchantments[(Enchantments)i] += baseEnchantmentChance / (j + 1);
			}
		}
	}

	//Predicate for finding all instances of a target gem
	private static bool FindGem(Gems currentGem) {
		if (currentGem == targetGem) {
			return true;
		}
		else {
			return false;
		}
	}

	protected void PrintWeaponStats(string weaponType) {
		Debug.Log("<b>Weapon Stats (" + weaponType + "):</b>\n<i>Rarity: </i>" + weaponRarity + "\n<i>Damage Min: </i>" + damageMin +
				  "\n<i>Damage Max: </i>" + damageMax + "\n<i>Crit Chance: </i>" + critChance + "\n<i>Attack Cooldown: </i>" + attackCoolDown +
				  "\n<i>Hands Required: </i>" + handsRequired + "\n<i>Gems Equipped: </i>" + PrintGems() + "\n<i>Enchantment Percents: </i>" + PrintDictionary());
    }

	string PrintGems() {
		string returnValue = "";

		foreach (Gems gem in attachedGems) {
			returnValue += "\n     " + gem.ToString();
		}
		return returnValue;
	}

	string PrintDictionary() {
		string returnValue = "";

		foreach (Enchantments key in weaponEnchantments.Keys) {
			returnValue += "\n     " + key + ": " + weaponEnchantments[key];
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

		//JPS: Apply calculations for enchantments, such as additional crit chance, freezing chance, etc.

		other.TakeDamage(attackDamage);
	}
}
