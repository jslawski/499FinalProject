using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour {
	//JPS: Implement different tiers of chests (common, uncommon, and rare) that have more likelihood
	//	   of producing equipment of different rarities.  Refactor equipment stat determination to be OUTSIDE
	//	   of Start(), and instead only happens when GenerateEquipmentStats() is called, so the equipment stats
	//	   can be manipulated in this script.

	private GameObject[] weapons;			//List of all of the possible weapons
											
	public GameObject armorPrefab;
	public GameObject daggerPrefab;
	public GameObject shortSwordPrefab;
	public GameObject battleAxePrefab;
	public GameObject halberdPrefab;

	private Equipment currentEquipment;

	void Start() {
		//JPS: Find a way to auto-populate this list...
		weapons = new GameObject[] { daggerPrefab, shortSwordPrefab, battleAxePrefab, halberdPrefab };
	}

	//Generate a piece of equipment (JPS: Or gemstones, in the future)
	//and destroy the chest
	public void GenerateTreasure() {
		//JPS: I'm thinking weapons should be more abundant than armor, because
		//	   a player is more likely to swap out new weapons than new armor...

		//Roll to see what the item is
		float equipmentTypeRoll = Random.Range(0, 1.0f);

		//Determine equipmentType
		//Weapons:	60%
		//Armor:	40%
		if ((1 - equipmentTypeRoll) <= 0.40f) {
			print("Generating Armor!");
			Instantiate(armorPrefab, transform.position, new Quaternion());
		}
		else {
			print("Generating Weapon!");
			Instantiate(weapons[Random.Range(0, weapons.Length)], transform.position, new Quaternion());
		}
		
		//Destroy the chest
		Destroy(gameObject);
	}
}
