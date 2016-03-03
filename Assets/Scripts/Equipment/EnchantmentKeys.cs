using UnityEngine;
using System.Collections;

/*~~~~~~~STEPS TO IMPLEMENTING A NEW ENCHANTMENT~~~~~~~
1.  Add the gem to the Gems enum, and the Enchantment key to the Enchantments enum in this file.
    Make sure the gem and the enchantment key correspond to the same numerical position!

2.  Add the necessary display text in EquipmentTextZone::GetEnchantmentStringValue() for the new enchantment

3.  Add an adjective and noun for both Weapons AND Armor in NameGenerator::GetAdjectiveFromKey() and NameGenerator::GetNounFromKey()  
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

//These gem names are arbitrary.  We can change them to whatever we want
//Gems used for enchanting a weapon:
public enum Gems {	Aquamarine,		//Ice
					Sunstone,		//Fire
					Fluorite,		//Additional Crit
					Onyx,			//Additional Damage
					Spinel,			//Wounding
					NumberOfTypes	//Number of different gems
};

//GLOBAL KEY VALUES FOR ENCHANTMENTS  
//Whenever an enchanted object is interacted with, (ex: weapon, armor, enemy, etc.) 
//access each enchantment in that object's dictionary, indexed by the following keys:
//(IMPORTANT!  ENCHANTMENTS ARE LISTED IN THE SAME ORDER AS THEIR CORRESPONDING GEMS ARE LISTED)
public enum Enchantments {
	//ICE ENCHANTMENT
	//Stone:  Aquamarine
	//Weapon:		   Deals ice-based damage.  Contains a percent-chance (0-1) to slow an enemy's movements temporarily
	//Armor/Enemies:   Resistance to ice-based damage.  Contains a percentage (0-1) of damage prevented from ice-based attacks.
	//				   Prevents any slow-down effect from ice-enchanted weapons.
	ICE_KEY,

	//FIRE ENCHANTMENT
	//Stone:  Sunstone
	//Weapon:			Deals fire-based damage.  Contains a percent-chance (0-1) to ignite an enemy, dealing continuous damage over a short period of time
	//					(Potentially has the ability to ignite other enemies?)
	//Armor/Enemies:	Resistance to fire-based damage.  Contains a percentage (0-1) of damage prevented from fire-based attacks.
	//					Prevents any igniting effect from fire-enchanted weapons
	FIRE_KEY,

	//ADDITIONAL CRIT ENCHANTMENT
	//Stone:  Fluorite
	//Weapon:			Contains a percentage (0-1) that is added to the weapon's critChance before damage is calculated every attack.
	//Armor/Enemies:	Contains a percentage (0-1) that is added to the character's critChance before damage is calculated every attack.
	//					JPS:  Do we want 2 different critChances? A base one for the character and another for the weapon?  They could both
	//						  be rolled individually every attack so they're not additive.  We'll have to discuss.
	ADDITIONAL_CRIT_KEY,

	//ADDITIONAL DAMAGE ENCHANTMENT
	//Stone:  Onyx
	//Weapon:			Contains a percentage (0-1) that is applied to the weapon's damageMin and damageMax, then added to it.
	//Armor/Enemies:	Contains a percentage (0-1) that is applied to the armor/enemy's damagePreventedMin and damagePreventedMax, then added to it.
	ADDITIONAL_DAMAGE_KEY,

	//WOUNDING ENCHANTMENT
	//Stone:  Spinel
	//Weapon:			Contains a percent-chance (0-1) to "wound" an enemy, causing them lose chunks of HP at regular intervals for a period of time.
	//					(Potentially allow this ability to stack with each successful trigger?)
	//Armor/Enemies:	Prevents any "wound" effect from wounding-enchanted weapons.  (Anything else?)
	WOUNDING_KEY,

	NUMBER_OF_ENCHANTMENTS
};

public class EnchantmentKeys : MonoBehaviour {

}
