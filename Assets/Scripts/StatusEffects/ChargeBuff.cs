using UnityEngine;
using System.Collections;

public class ChargeBuff : StatusEffect {
	public float movespeedIncrease = 10;
	public float duration_c = 3;

	// Use this for initialization
	protected override void Start () {
		base.Start();

		duration = duration_c;
		timeRemaining = duration;

		//Apply the movespeed granted by this buff
		thisCharacter.movespeed += movespeedIncrease;
	}

	//Take away the movespeed granted by the buff before deleting this script
	protected override void EffectExpire() {
		thisCharacter.movespeed -= movespeedIncrease;
		base.EffectExpire();
	}
}
