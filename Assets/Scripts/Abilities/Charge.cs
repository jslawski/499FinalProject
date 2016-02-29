using UnityEngine;
using System.Collections;

public class Charge : Ability {
	public float cooldown_c = 7;
	public float manaCost_c = 120;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		print("Charge.Start()");

		cooldown = cooldown_c;
		//Debug activation key
		activateKey = KeyCode.Q;
		manaCost = manaCost_c;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}

	public override void Activate() {
		base.Activate();

		//Apply the charge buff to the player
		thisPlayer.statusEffects.Add(thisPlayer.gameObject.AddComponent<ChargeBuff>());
	}
}
