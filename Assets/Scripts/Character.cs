﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Character : MonoBehaviour, DamageableObject {
	public List<Ability> abilities;
	public List<StatusEffect> statusEffects;
	public PlayerUI hud;				//HUD for the player

	public float maxHealth;				//The player's maximum health
	public float health;				//The player's current health

	public float maxMana;				//The player's maximum mana
	public float mana;                  //The player's remaining mana
	protected float manaRegen;			//The rate of mana regeneration for the player

	public float movespeed;				//The player's max speed
	float acceleration = 150f;			//How quickly a player gets up to max speed
	float decelerationRate = 0.15f;		//(0-1) How quickly a player returns to rest after releasing movement buttons

	Rigidbody thisRigidbody;			//Reference to the attached Rigidbody
	Collider thisCollider;              //Reference to the attached Collider

	public Weapon currentWeapon;        //Reference to the player's currently equipped weapon
	bool inMeleeAttackCoroutine = false;
	protected float attackCooldown = 0;

	public Armor currentArmor;			//Reference to the player's currently equipped armor

	// Use this for initialization
	void Start () {
		thisRigidbody = GetComponent<Rigidbody>();
		thisCollider = GetComponent<Collider>();

		//Debug characteristics and stats:
		abilities.Add(this.gameObject.AddComponent<Charge>());
		maxMana = 400;
		mana = maxMana;
		manaRegen = 4f;
	}
	
	// Update is called once per frame
	void Update () {
		CharacterMovement();

		CharacterAttack();

		//Regenerate mana over time
		if (mana < maxMana) {
			mana += Mathf.Min(maxMana - mana, manaRegen * Time.deltaTime);
		}

		//Cooldown attack to prepare for the next one
		if (attackCooldown > 0) {
			attackCooldown -= Time.deltaTime;
		}
	}

	void CharacterMovement() {
		/*~~~~~~~~~~~~~~~~~~~~~~~~DEBUG MOVESPEED CHANGE~~~~~~~~~~~~~~~~~~*/
		if (Input.GetKeyDown(KeyCode.PageUp))
			movespeed++;
		if (Input.GetKeyDown(KeyCode.PageDown))
			movespeed--;
		/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

		//Movement up
		if (Input.GetKey(KeyCode.UpArrow)) {
			Move(Vector3.forward);
		}
		//Movement down
		else if (Input.GetKey(KeyCode.DownArrow)) {
			Move(Vector3.back);
		}

		//Movement right
		if (Input.GetKey(KeyCode.RightArrow)) {
			Move(Vector3.right);
		}
		//Movement left
		else if (Input.GetKey(KeyCode.LeftArrow)) {
			Move(Vector3.left);
		}

		//If no direction is being pressed, decelerate the player
		if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) &&
			!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
			thisRigidbody.velocity = Vector3.Lerp(thisRigidbody.velocity, Vector3.zero, decelerationRate);
		}

		//Limit the player's movement speed to the maximum movespeed
		if (thisRigidbody.velocity.magnitude > movespeed) {
			thisRigidbody.velocity = thisRigidbody.velocity.normalized * movespeed;
		}
	}
	
	void CharacterAttack() {
		//JPS: If the player doesn't have a weapon equipped, have them attack with their fists instead (make a default weapon)
		if (currentWeapon != null && !inMeleeAttackCoroutine && attackCooldown <= 0 && Input.GetKeyDown(KeyCode.E)) {
			attackCooldown = currentWeapon.attackCoolDown;
			StartCoroutine(MeleeAttack());
		}
	}

	IEnumerator MeleeAttack() {
		inMeleeAttackCoroutine = true;

		//Attacks will have a slight delay, dependant on the weapon
		yield return new WaitForSeconds(currentWeapon.attackDelay);

		Quaternion startRot = Quaternion.Euler(-currentWeapon.swingAngle/2f * currentWeapon.swingAxis);
		Quaternion endRot = Quaternion.Euler(currentWeapon.swingAngle/2f * currentWeapon.swingAxis);
		
		//Enable the weapon's collider to execute an attack
		//Enables the mesh renderer as well for debug purposes
		currentWeapon.gameObject.GetComponent<Collider>().enabled = true;

		float timeElapsed = 0;
		while (timeElapsed < currentWeapon.attackTime) {
			timeElapsed += Time.deltaTime;
			float percent = timeElapsed/currentWeapon.attackTime;

			currentWeapon.gameObject.transform.localRotation = Quaternion.Lerp(startRot, endRot, percent);

			yield return 0;
		}

		//Keep the collider up until the next FixedUpdate in order to calculate damage done to afflicted enemies
		//Then disable it.
		yield return new WaitForFixedUpdate();
		currentWeapon.gameObject.GetComponent<Collider>().enabled = false;

		inMeleeAttackCoroutine = false;
	}
	
	void Move(Vector3 direction) {
		thisRigidbody.AddForce(direction * acceleration, ForceMode.Acceleration);
		//thisRigidbody.AddForce(new Vector3(0, 0, acceleration), ForceMode.Acceleration);
		//if (thisRigidbody.velocity.z > movespeed) {
		//	Vector3 cur_vel = thisRigidbody.velocity;
		//	cur_vel.z = movespeed;
		//	thisRigidbody.velocity = cur_vel;
		//}
	}

	public void TakeDamage(float damageIn) {
		print("<color=red>TakeDamage() not implemented yet.</color>");
	}

	public void EquipWeapon(Weapon weaponToBeEquipped) {
		if (currentWeapon != null) {
			DropWeapon(currentWeapon);
		}

		currentWeapon = weaponToBeEquipped;

		//Attach the weapon to the player, and fix its transform values
		weaponToBeEquipped.gameObject.transform.SetParent(transform);
		weaponToBeEquipped.gameObject.transform.localScale = weaponToBeEquipped.weaponDimensions;
		weaponToBeEquipped.gameObject.transform.localPosition = Vector3.zero;

		//Disable the weapon collider
		currentWeapon.gameObject.GetComponent<Collider>().enabled = false;
	}

	public void EquipArmor(Armor armorToBeEquipped) {
		if (currentArmor != null) {
			DropArmor(currentArmor);
		}

		currentArmor = armorToBeEquipped;

		//Attach the armor to the player, and turn off its mesh renderer.
		//And armor equip can be represented by a change in the player model.
		//Once the armor is "equipped," the object no longer needs to be rendered.
		armorToBeEquipped.gameObject.transform.SetParent(transform);
		armorToBeEquipped.gameObject.GetComponent<MeshRenderer>().enabled = false;

		//Disable the armor collider
		currentArmor.gameObject.GetComponent<Collider>().enabled = false;
	}

	void DropWeapon(Weapon weaponToBeDropped) {
		print("<color=red>DropWeapon() not implemented yet.</color>");
	}

	void DropArmor(Armor armorToBeDropped) {
		print("<color=red>DropArmor() not implemented yet.</color>");
	}
}
