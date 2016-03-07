using UnityEngine;
using System.Collections;

//JPS:  Refactor this to be more generalizable, with less repeated code
public class NameGenerator : MonoBehaviour {
	private static string GetAdjectiveFromKey(Equipment thisEquipment, Enchantments key) {
		//Different equipment types have different adjectives
		if (thisEquipment.equipmentType == EquipmentType.Weapon) {
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
		else if (thisEquipment.equipmentType == EquipmentType.Armor) {
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
					return "<color=red>ERROR: Armor adjective could not be determined!</color>";
			}
		}
		else {
			return "<color=red>ERROR: Equipment type is not defined for adjective!</color>";
		}
	}

	private static string DetermineAdjective(Equipment thisEquipment) {
		//If the Armor has 3 or more unique enchantments, it has the following adjectives
		if (thisEquipment.enchantments.Count == 5) {
			return "Chromatic ";
		}
		else if (thisEquipment.enchantments.Count == 4) {
			return "Tetrad "; //JPS: I'd like to change this...
		}
		else if (thisEquipment.enchantments.Count == 3) {
			return "Triad "; //JPS: I'd like to change this...
		}
		//Armors with no enchantments have no adjectives
		else if (thisEquipment.enchantments.Count == 0) {
			return "";
		}
		//Otherwise, find the enchantment that is the smaller of the two in terms of percent-chance
		else {
			Enchantments currentSmallestKey = Enchantments.NUMBER_OF_ENCHANTMENTS;
			float currentSmallestValue = 1000;
			foreach (Enchantments key in thisEquipment.enchantments.Keys) {
				if (thisEquipment.enchantments[key] < currentSmallestValue) {
					currentSmallestKey = key;
					currentSmallestValue = thisEquipment.enchantments[key];
				}
			}

			//JPS: Known bug.  In the case of 5 gem slots, with 3 unique enchantments, the enchantment
			//	   tied for first should determine the adjective, NOT the smallest valued one.

			return GetAdjectiveFromKey(thisEquipment, currentSmallestKey);
		}
	}

	private static string DetermineEquipmentGrade(Equipment thisEquipment) {
		//Different types of equipment have different words for their grades
		if (thisEquipment.rarity == Rarity.Common) {
			switch (thisEquipment.equipmentType) {
				case EquipmentType.Weapon:
					return "Shoddy";
				case EquipmentType.Armor:
					return "Leather Mail";
				default:
					return "<color=red>ERROR: Equipment type for equipment grade could not be determined!</color>";
			}
		}
		else if (thisEquipment.rarity == Rarity.Uncommon) {
			switch (thisEquipment.equipmentType) {
				case EquipmentType.Weapon:
					return "Fine";
				case EquipmentType.Armor:
					return "Chain Mail";
				default:
					return "<color=red>ERROR: Equipment type for equipment grade could not be determined!</color>";
			}
		}
		else if (thisEquipment.rarity == Rarity.Rare) {
			switch (thisEquipment.equipmentType) {
				case EquipmentType.Weapon:
					return "Superior";
				case EquipmentType.Armor:
					return "Iron Mail";
				default:
					return "<color=red>ERROR: Equipment type for equipment grade could not be determined!</color>";
			}
		}
		else {
			return "<color=red>ERROR: Could not determine weapon grade!</color>";
		}
	}

	private static string GetNounFromKey(Equipment thisEquipment, Enchantments key) {
		//Different equipment types have different adjectives
		if (thisEquipment.equipmentType == EquipmentType.Weapon) {
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
					return "<color=red>ERROR: Weapon noun could not be determined!</color>";
			}
		}
		else if (thisEquipment.equipmentType == EquipmentType.Armor) {
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
					return "<color=red>ERROR: Armor noun could not be determined!</color>";
			}
		}
		else {
			return "<color=red>ERROR: Equipment type is not defined for noun!</color>";
		}
	}

	private static string DetermineNoun(Equipment thisEquipment) {
		//An equipment will have no distinguishing noun if:
		//1) The equipment has 3 or more gem slots, and each is filled with a unique enchantment
		//2) The equipment has only 1 unique enchantment filling all of its slots
		//3) The equipment has no enchantments
		if (thisEquipment.enchantments.Count == 5) {
			return "";
		}
		else if (thisEquipment.enchantments.Count == 4 && thisEquipment.numGemSlots == 4) {
			return "";
		}
		else if (thisEquipment.enchantments.Count == 3 && thisEquipment.numGemSlots == 3) {
			return "";
		}
		else if (thisEquipment.enchantments.Count == 1) {
			return "";
		}
		else if (thisEquipment.enchantments.Count == 0) {
			return "";
		}
		//Otherwise, find the enchantment that has the largest percent-chance value
		else {
			Enchantments currentLargestKey = Enchantments.NUMBER_OF_ENCHANTMENTS;
			float currentLargestValue = 0;
			foreach (Enchantments key in thisEquipment.enchantments.Keys) {
				if (thisEquipment.enchantments[key] >= currentLargestValue) {
					currentLargestKey = key;
					currentLargestValue = thisEquipment.enchantments[key];
				}
			}

			return GetNounFromKey(thisEquipment, currentLargestKey);
		}
	}

	//Generate name for equipment
	public static string GenerateName(Equipment thisEquipment) {
		string name = "";

		//All equipment will generate the following fields for its name
		string adjective = DetermineAdjective(thisEquipment);				//Determined by the 2nd highest enchantment influence, or number of enchantments
		string equipmentGrade = DetermineEquipmentGrade(thisEquipment);		//Determined by the equipment's rarity
		string noun = DetermineNoun(thisEquipment);                         //Determined by highest enchantment inluence

		name = adjective + equipmentGrade + thisEquipment.equipmentObject + noun;

		return name;
	}
}
