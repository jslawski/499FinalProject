using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Character : MonoBehaviour {
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
	Collider thisCollider;				//Reference to the attached Collider

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

		//Regenerate mana over time
		if (mana < maxMana) {
			mana += Mathf.Min(maxMana - mana, manaRegen * Time.deltaTime);
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

	void Move(Vector3 direction) {
		thisRigidbody.AddForce(direction * acceleration, ForceMode.Acceleration);
		//thisRigidbody.AddForce(new Vector3(0, 0, acceleration), ForceMode.Acceleration);
		//if (thisRigidbody.velocity.z > movespeed) {
		//	Vector3 cur_vel = thisRigidbody.velocity;
		//	cur_vel.z = movespeed;
		//	thisRigidbody.velocity = cur_vel;
		//}
	}
}
