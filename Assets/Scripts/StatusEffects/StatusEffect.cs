using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusEffect : MonoBehaviour {
	public Character thisCharacter;     //The character this status effect is associate with
	public Image statusEffectIcon;		//The UI image showing the remaining time on the status effect

	protected float duration;
	protected float timeRemaining;

	// Use this for initialization
	protected virtual void Start () {
		thisCharacter = GetComponent<Character>();
		statusEffectIcon = GameObject.Find("BuffImage").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;

		statusEffectIcon.material.SetFloat("_Percent", 1 - (timeRemaining / duration));

		if (timeRemaining <= 0) {
			EffectExpire();
		}
	}

	virtual protected void EffectExpire() {
		thisCharacter.statusEffects.Remove(this);
		Destroy(this);
	}
}
