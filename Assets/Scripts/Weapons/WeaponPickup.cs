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
			EquipWeapon(other.gameObject);
        }
		
	}

	//TODO: Make sure this still works after player rotation is implemented
	void EquipWeapon(GameObject player) {
		Collider weaponCollider = gameObject.GetComponent<Collider>();
		Weapon weaponStats = gameObject.GetComponent<Weapon>();

		//Equip the weapon for the Character script to access
		player.GetComponent<Character>().currentWeapon = weaponStats;

		//Disable the collider, as it will only be enabled when attacking once equipped
		//Disable mesh renderer too for debugging purposes.  In the future it won't even be here.
		weaponCollider.enabled = false;
		gameObject.GetComponent<MeshRenderer>().enabled = false;

		//Transform the weapon to its proper dimensions when equipped
		weaponCollider.transform.localScale = weaponStats.hitboxDimensions;
		gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + weaponCollider.transform.localScale.z);
		gameObject.transform.eulerAngles = player.transform.eulerAngles;

		//Set the weapon as a child of the player that picked it up
		gameObject.transform.SetParent(player.transform);
		
		//Get rid of this script once the weapon is equipped
		Destroy(this);
	}

}
