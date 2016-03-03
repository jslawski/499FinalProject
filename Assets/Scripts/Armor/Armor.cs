using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//IDEA: If we want, we can have different types of armor, but we'd have to think of a way to vary them.
//Potentially, heavier armor would make the player move slower.  I'm not sure if that's enough variation to
//warrant completely different types of armor, though.  

public class Armor : MonoBehaviour, EnchantableObject {
	//~~~~~~Armor Stats~~~~~~
	public string armorName;									//Full name of the armor
	public float defenseMin;									//Minimum amount of damage the piece of armor can prevent
	public float defenseMax;									//Maximum amount of damage the piece of armor can prevent
	public Rarity armorRarity;									//Rarity of the armor

	//~~~~~~Enchantments~~~~~~
	//JPS:  Refactor this stuff to be in the EnchantableObject Interface
	public int numGemSlots;										//Number of slots a armor has for enchantment gems
	protected List<Gems> attachedGems;							//List of all gems attached to the armor.  Used for calculating and storing values in armorEnchantments
	public Dictionary<Enchantments, float> armorEnchantments;	//Dict of all of the current enchantment abiltities on the armor
	protected float baseEnchantmentChance = 0.10f;				//Base scalar (0-1) that enchantment calculations use to determine percent-chance of enchantment triggering 
	static protected Gems targetGem;							//Used in the predicate FindGem() to find all gems of a specific type.  Used for percent-chance calculations 

	// Use this for initialization
	void Start () {
		//Like weapons, in the future rarity will be determined as soon as the armor is taken out of a chest
		//Just set it here for debugging purposes for now
		armorRarity = Rarity.Rare;

		//For debugging purposes, populate with random gem slots and gems.  
		//This will later vary based on rarity and potentially type of armor (if we decide to have different types)
		numGemSlots = Random.Range(0, 6);
		attachedGems = new List<Gems>();
		for (int i = 0; i < numGemSlots; i++) {
			attachedGems.Add((Gems)Random.Range(0, (int)Gems.NumberOfTypes));
		}

		//Populate dictionary with all of the armor's enchantments
		armorEnchantments = new Dictionary<Enchantments, float>();
		CalculateEnchantmentPercents();

		//Generate the name of the armor given the randomly generated values above
		armorName = NameGenerator.GenerateName(this);
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

	//Predicate for finding all instances of a target gem
	private static bool FindGem(Gems currentGem) {
		if (currentGem == targetGem) {
			return true;
		}
		else {
			return false;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
