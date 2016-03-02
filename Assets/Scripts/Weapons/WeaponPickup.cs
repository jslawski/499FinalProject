using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {
//This is a specific behavior that only happens when the weapon is on the map/not picked up
//Note to self:  Consider making this an Event system if multiple things will be happening at pickup
//				 Ex: Drop your current weapon, equip new weapon, etc.

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			//TODO: Unequip current weapon.  Drop it somewhere around the player so that they can pick it up again.
			//		Right now the current weapon gets replaced, even though it remains a child of the player game object.
			other.gameObject.GetComponent<Character>().EquipWeapon(GetComponent<Weapon>());

			//Get rid of this script once the weapon is equipped
			Destroy(this);
		}
		
	}

}
