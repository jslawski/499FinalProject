using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponTextZone : MonoBehaviour {

	Weapon thisWeapon;
	TextMesh thisText;

	// Use this for initialization
	void Start () {
		thisWeapon = GetComponentInParent<Weapon>();
		thisText = GetComponent<TextMesh>();
	}
	
	//Get customized text for each enchantment
	string GetEnchantmentStringValue(Enchantments key) {
		//Cast the string to an enum for quicker calculating
		switch (key) {
			case Enchantments.ICE_KEY:
				return "<b><color=aqua>Ice: " + thisWeapon.weaponEnchantments[key] * 100 + "%</color></b>";
			case Enchantments.FIRE_KEY:
				return "<b><color=red>Fire: " + thisWeapon.weaponEnchantments[key] * 100 + "%</color></b>";
			case Enchantments.ADDITIONAL_CRIT_KEY:
				return "<b><color=purple>Crit+: " + thisWeapon.weaponEnchantments[key] * 100 + "%</color></b>";
			case Enchantments.ADDITIONAL_DAMAGE_KEY:
				return "<b><color=maroon>Dmg+: " + thisWeapon.weaponEnchantments[key] * 100 + "%</color></b>";
			case Enchantments.WOUNDING_KEY:
				return "<b><color=brown>Wound: " + thisWeapon.weaponEnchantments[key] * 100 + "%</color></b>";
			default:
				return "<color=red>ERROR: No enchantment found</color>";
		}
	}

	//Generate the text that is displayed above the weapon
	string GenerateText() {
		string textValue = "";

		//Weapons of different rarities are displayed in different colors
		if (thisWeapon.weaponRarity == Rarity.Common) {
			textValue += "<b><color=black>" + thisWeapon.weaponName + "</color></b>\n";
		}
		else if (thisWeapon.weaponRarity == Rarity.Uncommon) {
			textValue += "<b><color=silver>" + thisWeapon.weaponName + "</color></b>\n";
		}
		else if (thisWeapon.weaponRarity == Rarity.Rare) {
			textValue += "<b><color=orange>" + thisWeapon.weaponName + "</color></b>\n";
		}

		textValue += "<b>Dmg:</b> " + thisWeapon.damageMin + " - " + thisWeapon.damageMax + "\n";

		//Print stats of all enchantments
		int enchantmentCount = 0; //Keep track of the number of enchantments processed to determine newlines
		foreach (Enchantments key in thisWeapon.weaponEnchantments.Keys) {
			//Only 2 enchantments per line
			enchantmentCount++;
			if (enchantmentCount % 3 == 0) {
				textValue += "\n";
			}
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
			//TODO:  I really want to change this so that it only has to generate text once,
			//		 but putting GenerateText() in Start() might yield unreliable results in terms
			//		 of when certain values in thisWeapon are available.
			string displayText = GenerateText();
			Vector3 textPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5); //Arbitrary 5 offset for now
			GameManager.S.DisplayWeaponText(textPosition, displayText);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			GameManager.S.DestroyWeaponText();
		}
	}
}
