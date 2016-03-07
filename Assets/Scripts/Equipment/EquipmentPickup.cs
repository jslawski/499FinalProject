using UnityEngine;
using System.Collections;

public class EquipmentPickup : MonoBehaviour {
	//This is a specific behavior that only happens when the equipment is on the map/not picked up
	//JPS:			 Consider making this an Event system if multiple things will be happening at pickup
	//				 Ex: Drop your current weapon, equip new weapon, etc.

	Equipment thisEquipment;

	void Start() {
		thisEquipment = GetComponent<Equipment>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			//JPS:  Unequip current equipment of the same type.  Drop it somewhere around the player so that they can pick it up again.
			//		Right now the current weapon gets replaced, even though it remains a child of the player game object.
			if (thisEquipment.equipmentType == EquipmentType.Weapon) {
				other.gameObject.GetComponent<Character>().EquipWeapon(GetComponent<Weapon>());
			}
			else if (thisEquipment.equipmentType == EquipmentType.Armor) {
				other.gameObject.GetComponent<Character>().EquipArmor(GetComponent<Armor>());
			}

			//Get rid of this script once the weapon is equipped
			Destroy(this);
		}
		
	}

}
