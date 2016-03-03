using UnityEngine;
using System.Collections;

//JPS:  Refactor this to be more generalizable, with less repeated code
public class NameGenerator : MonoBehaviour {

	//Each enchantment key has a unique adjective.  Get that adjective from this function.
	private static string GetWeaponAdjectiveFromKey(Enchantments key) {
		switch (key) {
			case Enchantments.ICE_KEY:
				return "Frigid ";
			case Enchantments.FIRE_KEY:
				return "Scorched ";
			case Enchantments.ADDITIONAL_CRIT_KEY:
				return "Warped ";
			case Enchantments.ADDITIONAL_DAMAGE_KEY:
				return "Refined ";
			case Enchantments.WOUNDING_KEY:
				return "Pained ";
			default:
				return "<color=red>ERROR: Adjective could not be determined!</color>";
		}
	}

	private static string DetermineWeaponAdjective(Weapon thisWeapon) {
		//If the weapon has 3 or more unique enchantments, it has the following adjectives
		if (thisWeapon.weaponEnchantments.Count == 5) {
			return "Chromatic ";
		}
		else if (thisWeapon.weaponEnchantments.Count == 4) {
			return "Tetrad "; //JPS: I'd like to change this...
		}
		else if (thisWeapon.weaponEnchantments.Count == 3) {
			return "Triad "; //JPS I'd like to change this...
		}
		//Weapons with no enchantments have no adjectives
		else if (thisWeapon.weaponEnchantments.Count == 0) {
			return "";
		}
		//Otherwise, find the enchantment that is the smaller of the two in terms of percent-chance
		else {
			Enchantments currentSmallestKey = Enchantments.NUMBER_OF_ENCHANTMENTS;
			float currentSmallestValue = 1000;
			foreach(Enchantments key in thisWeapon.weaponEnchantments.Keys) {
				if (thisWeapon.weaponEnchantments[key] < currentSmallestValue) {
					currentSmallestKey = key;
					currentSmallestValue = thisWeapon.weaponEnchantments[key];
				}
			}

			return GetWeaponAdjectiveFromKey(currentSmallestKey);
		}
	}

	private static string DetermineWeaponGrade(Weapon thisWeapon) {
		if (thisWeapon.weaponRarity == Rarity.Common) {
			return "Shoddy ";
		}
		else if (thisWeapon.weaponRarity == Rarity.Uncommon) {
			return "Fine ";
		}
		else if (thisWeapon.weaponRarity == Rarity.Rare) {
			return "Superior ";
		}
		else {
			return "<color=red>ERROR: Could not determine weapon grade!</color>";
		}
	}

	//Each enchantment key has a unique noun.  Get that noun from this function.
	private static string GetWeaponNounFromKey(Enchantments key) {
		switch (key) {
			case Enchantments.ICE_KEY:
				return " of Frost";
			case Enchantments.FIRE_KEY:
				return " of Blazing";
			case Enchantments.ADDITIONAL_CRIT_KEY:
				return " of Distortion";
			case Enchantments.ADDITIONAL_DAMAGE_KEY:
				return " of Power";
			case Enchantments.WOUNDING_KEY:
				return " of Wounding";
			default:
				return "<color=red>ERROR: Noun could not be determined!</color>";
		}
	}

	private static string DetermineWeaponNoun(Weapon thisWeapon) {
		//A weapon will have no distinguishing noun if:
		//1) The weapon has 3 or more gem slots, and each is filled with a unique enchantment
		//2) The weapon has only 1 unique enchantment filling all of its slots
		//3) The weapon has no enchantments
		if (thisWeapon.weaponEnchantments.Count == 5) {
			return "";
		}
		else if (thisWeapon.weaponEnchantments.Count == 4 && thisWeapon.numGemSlots == 4) {
			return "";
		}
		else if (thisWeapon.weaponEnchantments.Count == 3 && thisWeapon.numGemSlots == 3) {
			return "";
		}
		else if (thisWeapon.weaponEnchantments.Count == 1) {
			return "";
		}
		else if (thisWeapon.weaponEnchantments.Count == 0) {
			return "";
		}
		//Otherwise, find the enchantment that has the largest percent-chance value
		else {
			Enchantments currentLargestKey = Enchantments.NUMBER_OF_ENCHANTMENTS;
			float currentLargestValue = 0;
			foreach (Enchantments key in thisWeapon.weaponEnchantments.Keys) {
				if (thisWeapon.weaponEnchantments[key] >= currentLargestValue) {
					currentLargestKey = key;
					currentLargestValue = thisWeapon.weaponEnchantments[key];
				}
			}

			return GetWeaponNounFromKey(currentLargestKey);
		}
	}

	private static string GetArmorAdjectiveFromKey(Enchantments key) {
		switch (key) {
			case Enchantments.ICE_KEY:
				return "Frostbitten ";
			case Enchantments.FIRE_KEY:
				return "Charred ";
			case Enchantments.ADDITIONAL_CRIT_KEY:
				return "Twisted ";
			case Enchantments.ADDITIONAL_DAMAGE_KEY:
				return "Reinforced ";
			case Enchantments.WOUNDING_KEY:
				return "Bloodied ";
			default:
				return "<color=red>ERROR: Adjective could not be determined!</color>";
		}
	}

	private static string DetermineArmorAdjective(Armor thisArmor) {
		//If the Armor has 3 or more unique enchantments, it has the following adjectives
		if (thisArmor.armorEnchantments.Count == 5) {
			return "Chromatic ";
		}
		else if (thisArmor.armorEnchantments.Count == 4) {
			return "Tetrad "; //JPS: I'd like to change this...
		}
		else if (thisArmor.armorEnchantments.Count == 3) {
			return "Triad "; //JPS: I'd like to change this...
		}
		//Armors with no enchantments have no adjectives
		else if (thisArmor.armorEnchantments.Count == 0) {
			return "";
		}
		//Otherwise, find the enchantment that is the smaller of the two in terms of percent-chance
		else {
			Enchantments currentSmallestKey = Enchantments.NUMBER_OF_ENCHANTMENTS;
			float currentSmallestValue = 1000;
			foreach (Enchantments key in thisArmor.armorEnchantments.Keys) {
				if (thisArmor.armorEnchantments[key] < currentSmallestValue) {
					currentSmallestKey = key;
					currentSmallestValue = thisArmor.armorEnchantments[key];
				}
			}

			return GetArmorAdjectiveFromKey(currentSmallestKey);
		}
	}

	private static string DetermineArmorGrade(Armor thisArmor) {
		if (thisArmor.armorRarity == Rarity.Common) {
			return "Leather Mail ";
		}
		else if (thisArmor.armorRarity == Rarity.Uncommon) {
			return "Chain Mail ";
		}
		else if (thisArmor.armorRarity == Rarity.Rare) {
			return "Iron Mail ";
		}
		else {
			return "<color=red>ERROR: Could not determine Armor grade!</color>";
		}
	}

	//Each enchantment key has a unique noun.  Get that noun from this function.
	private static string GetArmorNounFromKey(Enchantments key) {
		switch (key) {
			case Enchantments.ICE_KEY:
				return " of Crystalization";  //JPS: Not a fan of this one
			case Enchantments.FIRE_KEY:
				return " of Inferno";
			case Enchantments.ADDITIONAL_CRIT_KEY:
				return " of Willbending";
			case Enchantments.ADDITIONAL_DAMAGE_KEY:
				return " of Fortification";
			case Enchantments.WOUNDING_KEY:
				return " of Bloodthirst";
			default:
				return "<color=red>ERROR: Noun could not be determined!</color>";
		}
	}

	private static string DetermineArmorNoun(Armor thisArmor) {
		//A Armor will have no distinguishing noun if:
		//1) The Armor has 3 or more gem slots, and each is filled with a unique enchantment
		//2) The Armor has only 1 unique enchantment filling all of its slots
		//3) The Armor has no enchantments
		if (thisArmor.armorEnchantments.Count == 5) {
			return "";
		}
		else if (thisArmor.armorEnchantments.Count == 4 && thisArmor.numGemSlots == 4) {
			return "";
		}
		else if (thisArmor.armorEnchantments.Count == 3 && thisArmor.numGemSlots == 3) {
			return "";
		}
		else if (thisArmor.armorEnchantments.Count == 1) {
			return "";
		}
		else if (thisArmor.armorEnchantments.Count == 0) {
			return "";
		}
		//Otherwise, find the enchantment that has the largest percent-chance value
		else {
			Enchantments currentLargestKey = Enchantments.NUMBER_OF_ENCHANTMENTS;
			float currentLargestValue = 0;
			foreach (Enchantments key in thisArmor.armorEnchantments.Keys) {
				if (thisArmor.armorEnchantments[key] >= currentLargestValue) {
					currentLargestKey = key;
					currentLargestValue = thisArmor.armorEnchantments[key];
				}
			}

			return GetArmorNounFromKey(currentLargestKey);
		}
	}

	//Generate name for weapon
	public static string GenerateName(Weapon thisWeapon) {
		string name = "";
		
		//The weapon name will be constructed out of the following components:
		string adjective = "";      //Determined by the 2nd highest enchantment influence, or number of enchantments
		string weaponGrade = "";    //Determined by weaponRarity
		//thisWeapon.weaponType
		string noun = "";           //Determined by highest enchantment influence

		adjective = DetermineWeaponAdjective(thisWeapon);
		weaponGrade = DetermineWeaponGrade(thisWeapon);
		noun = DetermineWeaponNoun(thisWeapon);

		name = adjective + weaponGrade + thisWeapon.weaponType + noun;

		return name;
	}

	//Generate name for armor
	public static string GenerateName(Armor thisArmor) {
		string name = "";

		//The armor name will be constructed out of the following components
		string adjective = "";
		string armorGrade = "";
		string noun = "";

		adjective = DetermineArmorAdjective(thisArmor);
		armorGrade = DetermineArmorGrade(thisArmor);
		noun = DetermineArmorNoun(thisArmor);

		name = adjective + armorGrade + noun;

		return name;
	}
}
