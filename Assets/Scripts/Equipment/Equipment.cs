using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EquipmentType { Weapon, Armor};

public interface Equipment {
	string equipmentName { get;}							//Full name of the weapon
	EquipmentType equipmentType { get; }					//Type of equipment: Armor, Weapon, Accessory
	string equipmentObject { get; }							//What object the equipment is: weaponType, armorType (if we implement that), etc.			
	float minValue { get; }									//Min damage a weapon can output without a critical hit
	float maxValue { get; }									//Max damage a weapon can output without a critical hit
	Rarity rarity { get; }									//Rarity of the equipment
	Dictionary<Enchantments, float> enchantments { get; }	//Dictionary of enchantments on equipment
	int numGemSlots { get; }								//Number of slots an equipment has for enchantment gems
	List<Gems> gems { get; }								//List of gems currently attached to the equipment

	void CalculateEnchantmentPercents();                    //Calculate the values for all of the enchantments on the equipment

	void GenerateEquipmentStats();							//Determine the rarity, numGemSlots, enchantments, and weapon/armor type upon instantiation
}
