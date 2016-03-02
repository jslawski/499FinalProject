using UnityEngine;
using System.Collections;

public class NameGenerator : MonoBehaviour {

	//Each enchantment key has a unique adjective.  Get that adjective from this function.
	private static string GetAdjectiveFromKey(string key) {
		//Cast the string to an enum for quicker calculating
		switch ((Enchantments)System.Enum.Parse(typeof(Enchantments), key)) {
			case Enchantments.ICE_KEY:
				return "Frigid";
			case Enchantments.FIRE_KEY:
				return "Scorched";
			case Enchantments.ADDITIONAL_CRIT_KEY:
				return "Warped";
			case Enchantments.ADDITIONAL_DAMAGE_KEY:
				return "Refined";
			case Enchantments.WOUNDING_KEY:
				return "Pained";
			default:
				return "<color=red>ERROR: Adjective could not be determined!</color>";
		}
	}

	private static string DetermineAdjective(Weapon thisWeapon) {
		//If the weapon has 3 or more unique enchantments, the adjective
		//becomes one of the following words:
		if (thisWeapon.weaponEnchantments.Count == 5) {
			return "Chromatic";
		}
		else if (thisWeapon.weaponEnchantments.Count == 4) {
			return "Tetrad"; //I'd like to change this...
		}
		else if (thisWeapon.weaponEnchantments.Count == 3) {
			return "Triad"; //I'd like to change this...
		}
		//Otherwise, find the enchantment that is the smaller of the two in terms of percent-chance
		else {
			string currentSmallestKey = "placeholder";
			float currentSmallestValue = 1000;
			foreach(string key in thisWeapon.weaponEnchantments.Keys) {
				if (thisWeapon.weaponEnchantments[key] < currentSmallestValue) {
					currentSmallestKey = key;
					currentSmallestValue = thisWeapon.weaponEnchantments[key];
				}
			}
			print("CURRENT SMALLEST KEY: " + currentSmallestKey);
			return GetAdjectiveFromKey(currentSmallestKey);
		}
	}

	private static string DetermineWeaponGrade(Weapon thisWeapon) {
		if (thisWeapon.weaponRarity == Rarity.Common) {
			return "Shoddy";
		}
		else if (thisWeapon.weaponRarity == Rarity.Uncommon) {
			return "Fine";
		}
		else if (thisWeapon.weaponRarity == Rarity.Rare) {
			return "Superior";
		}
		else {
			return "<color=red>ERROR: Could not determine weapon grade!</color>";
		}
	}

	//Each enchantment key has a unique noun.  Get that noun from this function.
	private static string GetNounFromKey(string key) {
		//Cast the string to an enum for quicker calculating
		switch ((Enchantments)System.Enum.Parse(typeof(Enchantments), key)) {
			case Enchantments.ICE_KEY:
				return "of Frost";
			case Enchantments.FIRE_KEY:
				return "of Blazing";
			case Enchantments.ADDITIONAL_CRIT_KEY:
				return "of Distortion";
			case Enchantments.ADDITIONAL_DAMAGE_KEY:
				return "of Power";
			case Enchantments.WOUNDING_KEY:
				return "of Wounding";
			default:
				return "<color=red>ERROR: Noun could not be determined!</color>";
		}
	}

	private static string DetermineNoun(Weapon thisWeapon) {
		//A weapon will have no distinguishing noun if:
		//1) The weapon has 3 or more gem slots, and each is filled with a unique enchantment
		//2) The weapon has only 1 unique enchantment filling all of its slots
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
		//Otherwise, find the enchantment that has the largest percent-chance value
		else {
			string currentLargestKey = "placeholder";
			float currentLargestValue = 0;
			foreach (string key in thisWeapon.weaponEnchantments.Keys) {
				if (thisWeapon.weaponEnchantments[key] >= currentLargestValue) {
					currentLargestKey = key;
					currentLargestValue = thisWeapon.weaponEnchantments[key];
				}
			}
			print("CURRENT LARGEST KEY: " + currentLargestKey);
			return GetNounFromKey(currentLargestKey);
		}
	}

	public static string GenerateName(Weapon thisWeapon, string weaponType) {
		string name = "";
		
		//The weapon name will be constructed out of the following components:
		string adjective = "";      //Determined by the 2nd highest enchantment influence, or number of enchantments
		string weaponGrade = "";    //Determined by weaponRarity
		//weaponType
		string noun = "";           //Determined by highest enchantment influence

		adjective = DetermineAdjective(thisWeapon);
		weaponGrade = DetermineWeaponGrade(thisWeapon);
		noun = DetermineNoun(thisWeapon);

		name = adjective + " " + weaponGrade + " " + weaponType + " " + noun;
		print("NAME: " + name);
		return name;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
