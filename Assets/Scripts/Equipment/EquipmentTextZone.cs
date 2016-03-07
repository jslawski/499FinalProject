using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//JPS: Known bug.  This is still active when a piece of equipment is equipped.
//	   The text stops showing up when the weapon is picked up, which is GOOD,
//	   but given the code that's here, I don't know why the text doesn't stick around.
//	   May cause issues later.  Be on the lookout.
public class EquipmentTextZone : MonoBehaviour {

	Equipment thisEquipment;

	// Use this for initialization
	void Start() {
		thisEquipment = GetComponentInParent<Equipment>();
	}

	//Get customized text for each enchantment
	string GetEnchantmentStringValue(Enchantments key) {
		//Cast the string to an enum for quicker calculating
		//JPS: I would like to replace the text, such as Ice, Fire, Crit+, etc. with images, so that they
		//	   are more visually descriptive, and are easier to generalize for all equipment
		switch (key) {
			case Enchantments.ICE_KEY:
				return "<b><color=aqua>Ice: " + thisEquipment.enchantments[key] * 100 + "%</color></b>";
			case Enchantments.FIRE_KEY:
				return "<b><color=red>Fire: " + thisEquipment.enchantments[key] * 100 + "%</color></b>";
			case Enchantments.ADDITIONAL_CRIT_KEY:
				return "<b><color=purple>Crit+: " + thisEquipment.enchantments[key] * 100 + "%</color></b>";
			case Enchantments.ADDITIONAL_DAMAGE_KEY:
				return "<b><color=maroon>Dmg+: " + thisEquipment.enchantments[key] * 100 + "%</color></b>";
			case Enchantments.WOUNDING_KEY:
				return "<b><color=brown>Wound: " + thisEquipment.enchantments[key] * 100 + "%</color></b>";
			default:
				return "<color=red>ERROR: No enchantment found</color>";
		}
	}

	//Generate the text that is displayed above the weapon
	string GenerateText() {
		string textValue = "";

		//Weapons of different rarities are displayed in different colors
		if (thisEquipment.rarity == Rarity.Common) {
			textValue += "<b><color=black>" + thisEquipment.equipmentName + "</color></b>\n";
		}
		else if (thisEquipment.rarity == Rarity.Uncommon) {
			textValue += "<b><color=silver>" + thisEquipment.equipmentName + "</color></b>\n";
		}
		else if (thisEquipment.rarity == Rarity.Rare) {
			textValue += "<b><color=#FFD700>" + thisEquipment.equipmentName + "</color></b>\n";
		}

		//JPS: I'd like to replace this with icons in the future...
		if (thisEquipment.equipmentType == EquipmentType.Weapon) {
			textValue += "<b>Dmg:</b> " + thisEquipment.minValue + " - " + thisEquipment.maxValue + "\n";
		}
		else if (thisEquipment.equipmentType == EquipmentType.Armor) {
			textValue += "<b>Def:</b> " + thisEquipment.minValue + " - " + thisEquipment.maxValue + "\n";
		}

		//Print stats of all enchantments
		int enchantmentCount = 0; //Keep track of the number of enchantments processed to determine newlines
		foreach (Enchantments key in thisEquipment.enchantments.Keys) {
			//Only 2 enchantments per line
			enchantmentCount++;
			if (enchantmentCount % 3 == 0) {
				textValue += "\n";
			}
			//Otherwise separate by 5 spaces
			else if (enchantmentCount != 1) {
				textValue += "     ";
			}

			//Get text values for current enchantment
			textValue += GetEnchantmentStringValue(key);
		}

		return textValue;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			//JPS:   I really want to change this so that it only has to generate text once,
			//		 but putting GenerateText() in Start() might yield unreliable results in terms
			//		 of when certain values in thisEquipment are available.
			string displayText = GenerateText();
			Vector3 textPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5); //JPS: Arbitrary 5 offset for now
			GameManager.S.DisplayEquipmentText(textPosition, displayText);
		}
	}

	//JPS: Known bug.  Equipment can't be too close to each other.  If a player is able to 
	//walking into two triggers without exiting either of them, then both texts will display,
	//but only one will be destroyed when the player exists the triggers
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			GameManager.S.DestroyEquipmentText();
		}
	}
}
