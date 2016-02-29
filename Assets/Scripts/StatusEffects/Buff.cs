using UnityEngine;
using System.Collections;

public class Buff : StatusEffect {

	// Use this for initialization
	protected override void Start () {
		base.Start();
		
		//Create and keep a reference to the effect icon
		statusEffectIcon = thisCharacter.hud.AddStatusEffectIcon(statusEffectIconPrefab, true);
	}
	
	protected override void EffectExpire() {
		thisCharacter.hud.RemoveStatusEffectIcon(statusEffectIcon, true);
		base.EffectExpire();
	}
}
