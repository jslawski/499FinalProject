using UnityEngine;
using System.Collections;

//These gem names are arbitrary.  We can change them to whatever we want
//Gems used for enchanting a weapon:
//Aquamarine:	Ice
//Sunstone:		Fire
//Fluorite:		Additional Crit
//Onyx:			Additional Damage
//Spinel:		Wounding	
public enum Gems { Aquamarine, Sunstone, Fluorite, Onyx, Spinel, NumberOfTypes};

public class EnchantmentKeys : MonoBehaviour {
	//GLOBAL KEY VALUES FOR ENCHANTMENTS  
	//Whenever an enchanted object is interacted with, (ex: weapon, armor, enemy, etc.) 
	//access each enchantment in that object's dictionary, indexed by the following keys:

	//ICE ENCHANTMENT
	//Stone:  Aquamarine
	//Weapon:		   Deals ice-based damage.  Contains a percent-chance (0-1) to slow an enemy's movements temporarily
	//Armor/Enemies:   Resistance to ice-based damage.  Contains a percentage (0-1) of damage prevented from ice-based attacks.
	//				   Prevents any slow-down effect from ice-enchanted weapons.
	public static string ICE_KEY = "ice";

	//FIRE ENCHANTMENT
	//Stone:  Sunstone
	//Weapon:			Deals fire-based damage.  Contains a percent-chance (0-1) to ignite an enemy, dealing continuous damage over a short period of time
	//					(Potentially has the ability to ignite other enemies?)
	//Armor/Enemies:	Resistance to fire-based damage.  Contains a percentage (0-1) of damage prevented from fire-based attacks.
	//					Prevents any igniting effect from fire-enchanted weapons
	public static string FIRE_KEY = "fire";

	//ADDITIONAL CRIT ENCHANTMENT
	//Stone:  Fluorite
	//Weapon:			Contains a percentage (0-1) that is added to the weapon's critChance before damage is calculated every attack.
	//Armor/Enemies:	Contains a percentage (0-1) that is added to the character's critChance before damage is calculated every attack.
	//					NOTE: Do we want 2 different critChances? A base one for the character and another for the weapon?  They could both
	//						  be rolled individually every attack so they're not additive.  We'll have to discuss.
	public static string ADDITIONAL_CRIT_KEY = "additional_crit";

	//ADDITIONAL DAMAGE ENCHANTMENT
	//Stone:  Onyx
	//Weapon:			Contains a percentage (0-1) that is applied to the weapon's damageMin and damageMax, then added to it.
	//Armor/Enemies:	Contains a percentage (0-1) that is applied to the armor/enemy's damagePreventedMin and damagePreventedMax, then added to it.
	public static string ADDITIONAL_DAMAGE_KEY = "additional_damage";

	//WOUNDING ENCHANTMENT
	//Stone:  Spinel
	//Weapon:			Contains a percent-chance (0-1) to "wound" an enemy, causing them lose chunks of HP at regular intervals for a period of time.
	//					(Potentially allow this ability to stack with each successful trigger?)
	//Armor/Enemies:	Prevents any "wound" effect from wounding-enchanted weapons.  (Anything else?)
	public static string WOUNDING_KEY = "wounding";
}
