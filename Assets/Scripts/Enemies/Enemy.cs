using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, DamageableObject {
	protected float maxHealth;
	protected float health = 1000;

	//TODO: Take into account armor or resistance to magic
	public void TakeDamage(float damageIn) {
		print("Took " + damageIn + " damage!");
		GameManager.S.DisplayDamageText(transform.position, damageIn);
		health -= damageIn;
		
		if (health <= 0) {
			Die();
		}
	}

	void Die() {
		Destroy(this.gameObject);
	}
}
