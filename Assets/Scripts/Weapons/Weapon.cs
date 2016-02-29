using UnityEngine;
using System.Collections;

public enum Rarity { Common, Uncommon, Rare };

public class Weapon : MonoBehaviour {

	protected int damageMin;			//Min damage a weapon can output without a critical hit
	protected int damageMax;			//Max damage a weapon can output without a critical hit
	protected float critChance;			//Percent chance from 0 - 1 of landing a critical hit
	protected Rarity weaponRarity;		//Rarity of the weapon (determines its power)
	protected int handsRequired;        //Number of hands needed to hold the weapon (1 or 2)
	protected float attackCoolDown;		//Time in seconds that the player has to wait before being able to attack again

	// Use this for initialization
	protected virtual void Start () {
		//I would like to have this dynamically determined, sometimes by random chance, and other times depending on the chest it comes out of
		//Having a "rare chest" that has a guaranteed rare item would be kind of cool.  For now, the rarity is preset in this function
		weaponRarity = Rarity.Rare;
	}

	protected void PrintWeaponStats(string weaponType) {
		Debug.Log("<b>Weapon Stats (" + weaponType + "):</b>\n<i>Rarity: </i>" + weaponRarity + "\n<i>Damage Min: </i>" + damageMin +
				  "\n<i>Damage Max: </i>" + damageMax + "\n<i>Crit Chance: </i>" + critChance + "\n<i>Attack Cooldown: </i>" + attackCoolDown +
				  "\n<i>Hands Required: </i>" + handsRequired);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
