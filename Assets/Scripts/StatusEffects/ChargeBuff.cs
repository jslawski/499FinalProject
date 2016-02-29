using UnityEngine;
using System.Collections;

public class ChargeBuff : Buff {
	public float movespeedIncrease = 10;
	public float duration_c = 3;

	// Use this for initialization
	protected override void Start () {
		statusEffectIconPrefab = Resources.Load<GameObject>("StatusEffectIconPrefabs/ChargeBuffIcon");

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
