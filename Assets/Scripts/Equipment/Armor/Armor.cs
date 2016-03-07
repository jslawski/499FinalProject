using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//IDEA: If we want, we can have different types of armor, but we'd have to think of a way to vary them.
//Potentially, heavier armor would make the player move slower.  I'm not sure if that's enough variation to
//warrant completely different types of armor, though.  

public class Armor : MonoBehaviour, Equipment {
	//~~~~~~Armor Stats~~~~~~
	protected string armorName;									//Full name of the armor
	protected float defenseMin;									//Minimum amount of damage the piece of armor can prevent
	protected float defenseMax;									//Maximum amount of damage the piece of armor can prevent
	protected Rarity armorRarity;								//Rarity of the armor

	//~~~~~~Enchantments~~~~~~
	protected int armorGemSlots;								//Number of slots a armor has for enchantment gems
	protected List<Gems> attachedGems;							//List of all gems attached to the armor.  Used for calculating and storing values in armorEnchantments
	public Dictionary<Enchantments, float> armorEnchantments;	//Dict of all of the current enchantment abiltities on the armor
	protected float baseEnchantmentChance = 0.15f;				//Base scalar (0-1) that enchantment calculations use to determine percent-chance of enchantment triggering 
	static protected Gems targetGem;							//Used in the predicate FindGem() to find all gems of a specific type.  Used for percent-chance calculations 

	/*~~~~~~~~~~Properties~~~~~~~~~~*/
	public string equipmentName {
		get { return armorName; }
	}

	public EquipmentType equipmentType {
		get { return EquipmentType.Armor; }
	}

	//No different armor types (yet?)
	public string equipmentObject {
		get { return ""; }
	}

	public float minValue {
		get { return defenseMin; }
	}

	public float maxValue {
		get { return defenseMax; }
	}

	public Rarity rarity {
		get { return armorRarity; }
	}

	public Dictionary<Enchantments, float> enchantments {
		get { return armorEnchantments; }
	}

	public int numGemSlots {
		get { return armorGemSlots; }
	}

	public List<Gems> gems {
		get { return attachedGems; }
	}

	// Use this for initialization
	void Start () {
		attachedGems = new List<Gems>();
		armorEnchantments = new Dictionary<Enchantments, float>();

		//Uncomment the Debug function and comment the other one to generate an unweighted random armor
		GenerateEquipmentStatsDebug();
		//GenerateEquipmentStats();

		PrintArmorStats();
	}

	//Determine the min and max defense values for this piece of armor
	void DetermineDefenseStats() {
		//These are arbitrarly picked numbers.  They can change as we see fit.
		if (armorRarity == Rarity.Common) {
			defenseMin = Random.Range(20, 30);
			defenseMax = Random.Range(40, 50);
		}
		else if (armorRarity == Rarity.Uncommon) {
			defenseMin = Random.Range(70, 80);
			defenseMax = Random.Range(90, 100);
		}
		else if (armorRarity == Rarity.Rare) {
			defenseMin = Random.Range(130, 140);
			defenseMax = Random.Range(160, 180);
		}
	}

	//Calculate percent-chances for each enchantment on the armor
	//Right now they are all calculated the same way.
	public void CalculateEnchantmentPercents() {
		//Iterate through all of the gem types
		for (int i = 0; i < (int)Gems.NumberOfTypes; i++) {
			//Specify a target gem that is currently being searched for
			targetGem = (Gems)i;

			//Find all instances of the target gem on the armor
			List<Gems> foundGems = attachedGems.FindAll(FindGem);

			//Add a key to the dictionary initialized to 0 if an instance
			//of the gem was found
			if (foundGems.Count > 0) {
				armorEnchantments.Add((Enchantments)i, 0);
			}

			//Calculate percent-chance for triggering that enchantment based on
			//number of gems found.  Implement diminishing returns for each stacked gem.
			//1 Gem	 = 1/2 * baseEnchantmentChance
			//2 Gems = 1GemChance + (1/3 * baseEnchantmentChance)
			//3 Gems = 2GemChance + (1/4 * baseEnchantmentChance)
			//...etc.
			for (float j = 1; j <= foundGems.Count; j++) {
				//Update dictionary value
				armorEnchantments[(Enchantments)i] += baseEnchantmentChance / (j + 1);
			}
		}
	}

	//Generate a completely random weapon, regardless of the rarities of anything
	void GenerateEquipmentStatsDebug() {
		//Like weapons, in the future rarity will be determined as soon as the armor is taken out of a chest
		//Just set it here for debugging purposes for now
		armorRarity = (Rarity)Random.Range(0, 3);

		//For debugging purposes, populate with random gem slots and gems.  
		//This will later vary based on rarity and potentially type of armor (if we decide to have different types)
		armorGemSlots = Random.Range(0, 6);

		for (int i = 0; i < numGemSlots; i++) {
			attachedGems.Add((Gems)Random.Range(0, (int)Gems.NumberOfTypes));
		}

		//Populate dictionary with all of the armor's enchantments
		CalculateEnchantmentPercents();

		DetermineDefenseStats();

		//Generate the name of the armor given the randomly generated values above
		armorName = NameGenerator.GenerateName(this);
	}

	public void GenerateEquipmentStats() {
		//Roll to see how rare the equipment is
		float rarityRoll = Random.Range(0, 1.0f);

		//Determine the equipment's rarity
		//Common:		50%
		//Uncommon:		30%
		//Rare:			20%				 
		if ((1 - rarityRoll) <= 0.20f) {
			armorRarity = Rarity.Rare;
		}
		else if ((1 - rarityRoll) <= 0.30f) {
			armorRarity = Rarity.Uncommon;
		}
		else {
			armorRarity = Rarity.Common;
		}
		
		//Determine number of gem slots
		//Common:		0
		//Uncommon:		1-3
		//Rare:			3-5	
		if (armorRarity == Rarity.Common) {
			armorGemSlots = 0;
		}
		else if (armorRarity == Rarity.Uncommon) {
			armorGemSlots = Random.Range(1, 4);
		}
		else if (armorRarity == Rarity.Rare) {
			armorGemSlots = Random.Range(3, 6);
		}

		//Determine if any gem slots will have a gem in it
		//For each gem slot, there is a 10% chance to have a gem in it
		//JPS: Scale this to factor in rarity of the gem, which isn't implemented yet.
		//	   Currently any gem has an equal chance of taking up a gem slot.
		for (int i = 0; i < armorGemSlots; i++) {
			float gemRoll = Random.Range(0, 1.0f);
			if ((1 - gemRoll) <= 0.10f) {
				attachedGems.Add((Gems)Random.Range(0, (int)Gems.NumberOfTypes));
			}
		}

		//Calculate enchantment percents for each gem equipped
		CalculateEnchantmentPercents();

		DetermineDefenseStats();

		//Generate the name of the armor given the randomly generated values above
		armorName = NameGenerator.GenerateName(this);
	}

	protected void PrintArmorStats() {
		Debug.Log("<b>Armor Stats:</b>\n<i>Rarity: </i>" + armorRarity + "\n<i>Defense Min: </i>" + defenseMin +
				  "\n<i>Defense Max: </i>" + defenseMax + "\n<i>Gems Equipped: </i>" + PrintGems() + "\n<i>Enchantment Percents: </i>" + PrintDictionary());
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

		foreach (Enchantments key in armorEnchantments.Keys) {
			returnValue += "\n     " + key + ": " + armorEnchantments[key];
		}
		return returnValue;
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
}
