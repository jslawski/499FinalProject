using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour {

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

		//Roll to see what the item is, and how rare it is
		float rarityRoll = Random.Range(0, 1.0f);
		float equipmentTypeRoll = Random.Range(0, 1.0f);

		//Determine equipmentType
		//Weapons:	60%
		//Armor:	40%
		if (1 - equipmentTypeRoll <= 0.60f) {
			currentEquipment = new Weapon();
		}
		else if (1 - equipmentTypeRoll <= 0.40f) {
			currentEquipment = new Armor();
		}

		//Determine the equipment's rarity
		//Common:		50%
		//Uncommon:		30%
		//Rare:			20%				 
		if (1 - rarityRoll <= 0.50f) {
			currentEquipment.rarity = Rarity.Common;
		}
		else if (1 - rarityRoll <= 0.30f) {
			currentEquipment.rarity = Rarity.Uncommon;
		}
		else if (1 - rarityRoll <= 0.20f) {
			currentEquipment.rarity = Rarity.Rare;
		}
	}
}
